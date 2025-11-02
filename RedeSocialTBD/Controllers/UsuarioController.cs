using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RedeSocialTBD.Data;
using Microsoft.AspNetCore.Http;
using RedeSocialTBD.Models;
using RedeSocialTBD.Models.Relatorios;

namespace RedeSocialTBD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IMongoCollection<UsuarioModel> _usuarios;
        public UsuarioController(MongoDbService mongoDbService)
        {
            _usuarios = mongoDbService.Database?.GetCollection<UsuarioModel>("usuarios");
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioModel>> Get()
        {
            return await _usuarios.Find(FilterDefinition<UsuarioModel>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel?>> GetbyId(string id)
        {
            var filtro = Builders<UsuarioModel>.Filter.Eq(x => x.Id, id);
            UsuarioModel usuario = await _usuarios.Find(filtro).FirstOrDefaultAsync();

            return usuario is not null ? Ok(usuario) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioModel usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
            return CreatedAtAction(nameof(GetbyId), new { id = usuario.Id }, usuario);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UsuarioModel usuario)
        {
            var filtro = Builders<UsuarioModel>.Filter.Eq(x => x.Id, usuario.Id);
            var update = Builders<UsuarioModel>.Update
                .Set(x => x.Nome, usuario.Nome)
                .Set(x => x.UserName, usuario.UserName)
                .Set(x => x.Senha, usuario.Senha)
                .Set(x => x.DataNascimento, usuario.DataNascimento);
            await _usuarios.UpdateOneAsync(filtro, update);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filtro = Builders<UsuarioModel>.Filter.Eq(x => x.Id, id);
            await _usuarios.DeleteOneAsync(filtro);
            return Ok();
        }

        /// <summary>
        /// Retorna a quantidade de usuários acima de 18 anos e abaixo de 18 anos.
        /// </summary>
        [HttpGet("relatorio-idade")]
        [ProducesResponseType(typeof(IEnumerable<RelatorioIdade>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RelatorioIdade>>> GetUsuariosPorFaixaEtaria()
        {
            var hoje = DateTime.Today;

            // Pipeline de agregação
            var resultado = await _usuarios.Aggregate()
                .Project(u => new
                {
                    u.Id,
                    Idade = (hoje.Year - u.DataNascimento!.Value.Year) -
                            ((hoje.Month < u.DataNascimento.Value.Month ||
                              (hoje.Month == u.DataNascimento.Value.Month && hoje.Day < u.DataNascimento.Value.Day)) ? 1 : 0)
                })
                .Group(u => u.Idade >= 18 ? "18+" : "Menor de 18", g => new RelatorioIdade
                {
                    FaixaEtaria = g.Key,
                    Quantidade = g.Count()
                })
                .ToListAsync();

            return Ok(resultado);
        }
    }
}
