using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RedeSocialTBD.Data;
using RedeSocialTBD.Models;
using RedeSocialTBD.Models.Relatorios;

namespace RedeSocialTBD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalvoController : Controller
    {
        private readonly IMongoCollection<SalvoModel> _salvos;
        private readonly IMongoCollection<UsuarioModel> _usuarios;
        private readonly IMongoCollection<PublicacaoModel> _publicacoes;

        public SalvoController(MongoDbService mongoDbService)
        {
            _salvos = mongoDbService.Database?.GetCollection<SalvoModel>("salvos");
            _usuarios = mongoDbService.Database?.GetCollection<UsuarioModel>("usuarios");
            _publicacoes = mongoDbService.Database?.GetCollection<PublicacaoModel>("publicacoes");
        }

        [HttpGet]
        public async Task<IEnumerable<SalvoModel>> Get()
        {
            return await _salvos.Find(FilterDefinition<SalvoModel>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalvoModel?>> GetbyId(string id)
        {
            var filtro = Builders<SalvoModel>.Filter.Eq(x => x.Id, id);
            SalvoModel salvo = await _salvos.Find(filtro).FirstOrDefaultAsync();

            return salvo is not null ? Ok(salvo) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(SalvoModel salvo)
        {
            var usuarioExiste = await _usuarios.Find(u => u.Id == salvo.IdUsuario).AnyAsync();
            if (!usuarioExiste)
                return BadRequest("O usuário informado não existe.");

            var publicacaoExiste = await _publicacoes.Find(p => p.Id == salvo.IdPublicacao).AnyAsync();
            if (!publicacaoExiste)
                return BadRequest("A publicação informada não existe.");

            var salvoExistente = await _salvos.Find(s =>
                s.IdUsuario == salvo.IdUsuario && s.IdPublicacao == salvo.IdPublicacao
            ).FirstOrDefaultAsync();

            if (salvoExistente != null)
                return Conflict("Essa publicação já está salva por este usuário.");

            await _salvos.InsertOneAsync(salvo);
            return CreatedAtAction(nameof(GetbyId), new { id = salvo.Id }, salvo);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filtro = Builders<SalvoModel>.Filter.Eq(x => x.Id, id);
            await _salvos.DeleteOneAsync(filtro);
            return Ok();
        }

        /// <summary>
        /// Retorna a quantidade total de salvamentos por publicação.
        /// </summary>
        /// <returns>Lista de publicações com o número de vezes que foram salvas.</returns>
        [HttpGet("relatorio")]
        [ProducesResponseType(typeof(IEnumerable<RelatorioSalvos>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RelatorioSalvos>>> GetSalvosPorPublicacao()
        {
            var resultado = await _salvos.Aggregate()
                .Group(
                    s => s.IdPublicacao,
                    g => new RelatorioSalvos
                    {
                        IdPublicacao = g.Key,
                        TotalSalvos = g.Count()
                    })
                .SortByDescending(x => x.TotalSalvos)
                .ToListAsync();

            return Ok(resultado);
        }
    }
}
