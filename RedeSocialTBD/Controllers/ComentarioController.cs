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
    public class ComentarioController : Controller
    {
        private readonly IMongoCollection<ComentarioModel> _comentarios;
        private readonly IMongoCollection<UsuarioModel> _usuarios;
        private readonly IMongoCollection<PublicacaoModel> _publicacoes;

        public ComentarioController(MongoDbService mongoDbService)
        {
            _comentarios = mongoDbService.Database?.GetCollection<ComentarioModel>("comentarios");
            _usuarios = mongoDbService.Database?.GetCollection<UsuarioModel>("usuarios");
            _publicacoes = mongoDbService.Database?.GetCollection<PublicacaoModel>("publicacoes");
        }

        [HttpGet]
        public async Task<IEnumerable<ComentarioModel>> Get()
        {
            return await _comentarios.Find(FilterDefinition<ComentarioModel>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComentarioModel?>> GetbyId(string id)
        {
            var filtro = Builders<ComentarioModel>.Filter.Eq(x => x.Id, id);
            ComentarioModel comentario = await _comentarios.Find(filtro).FirstOrDefaultAsync();

            return comentario is not null ? Ok(comentario) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ComentarioModel comentario)
        {
            var usuarioExiste = await _usuarios.Find(u => u.Id == comentario.IdUsuario).AnyAsync();
            if (!usuarioExiste)
                return BadRequest("O usuário informado não existe.");

            var publicacaoExiste = await _publicacoes.Find(p => p.Id == comentario.IdPublicacao).AnyAsync();
            if (!publicacaoExiste)
                return BadRequest("A publicação informada não existe.");

            await _comentarios.InsertOneAsync(comentario);
            return CreatedAtAction(nameof(GetbyId), new { id = comentario.Id }, comentario);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filtro = Builders<ComentarioModel>.Filter.Eq(x => x.Id, id);
            await _comentarios.DeleteOneAsync(filtro);
            return Ok();
        }

        /// <summary>
        /// Retorna o total de comentários feitos por cada usuário.
        /// </summary>
        /// <returns>Lista de usuários com o número de comentários feitos.</returns>
        [HttpGet("relatorio")]
        [ProducesResponseType(typeof(IEnumerable<RelatorioComentarios>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RelatorioComentarios>>> GetComentariosPorUsuario()
        {
            var resultado = await _comentarios.Aggregate()
                .Group(
                    c => c.IdUsuario,
                    g => new RelatorioComentarios
                    {
                        IdUsuario = g.Key,
                        TotalComentarios = g.Count()
                    })
                .SortByDescending(x => x.TotalComentarios)
                .ToListAsync();

            return Ok(resultado);
        }
    }
}
