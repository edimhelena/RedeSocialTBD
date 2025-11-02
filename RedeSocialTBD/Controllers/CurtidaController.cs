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
    public class CurtidaController : Controller
    {
        private readonly IMongoCollection<CurtidaModel> _curtidas;
        private readonly IMongoCollection<UsuarioModel> _usuarios;
        private readonly IMongoCollection<PublicacaoModel> _publicacoes;

        public CurtidaController(MongoDbService mongoDbService)
        {
            _curtidas = mongoDbService.Database?.GetCollection<CurtidaModel>("curtidas");
            _usuarios = mongoDbService.Database?.GetCollection<UsuarioModel>("usuarios");
            _publicacoes = mongoDbService.Database?.GetCollection<PublicacaoModel>("publicacoes");
        }

        [HttpGet]
        public async Task<IEnumerable<CurtidaModel>> Get()
        {
            return await _curtidas.Find(FilterDefinition<CurtidaModel>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurtidaModel?>> GetbyId(string id)
        {
            var filtro = Builders<CurtidaModel>.Filter.Eq(x => x.Id, id);
            CurtidaModel curtida = await _curtidas.Find(filtro).FirstOrDefaultAsync();

            return curtida is not null ? Ok(curtida) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CurtidaModel curtida)
        {
            var usuarioExiste = await _usuarios.Find(u => u.Id == curtida.IdUsuario).AnyAsync();
            if (!usuarioExiste)
                return BadRequest("O usuário informado não existe.");

            var publicacaoExiste = await _publicacoes.Find(p => p.Id == curtida.IdPublicacao).AnyAsync();
            if (!publicacaoExiste)
                return BadRequest("A publicação informada não existe.");

            var curtidaExistente = await _curtidas.Find(c =>
                c.IdUsuario == curtida.IdUsuario && c.IdPublicacao == curtida.IdPublicacao
            ).FirstOrDefaultAsync();

            if (curtidaExistente != null)
                return Conflict("O usuário já curtiu essa publicação.");

            await _curtidas.InsertOneAsync(curtida);
            return CreatedAtAction(nameof(GetbyId), new { id = curtida.Id }, curtida);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filtro = Builders<CurtidaModel>.Filter.Eq(x => x.Id, id);
            await _curtidas.DeleteOneAsync(filtro);
            return Ok();
        }

        /// <summary>
        /// Retorna a quantidade total de curtidas por publicação.
        /// </summary>
        /// <returns>Lista de publicações com o número de curtidas.</returns>
        [HttpGet("relatorio")]
        [ProducesResponseType(typeof(IEnumerable<RelatorioCurtidas>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RelatorioCurtidas>>> GetCurtidasPorPublicacao()
        {
            var resultado = await _curtidas.Aggregate()
                .Group(
                    c => c.IdPublicacao,
                    g => new RelatorioCurtidas
                    {
                        IdPublicacao = g.Key,
                        TotalCurtidas = g.Count()
                    })
                .SortByDescending(x => x.TotalCurtidas)
                .ToListAsync();

            return Ok(resultado);
        }
    }
}
