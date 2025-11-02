using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RedeSocialTBD.Data;
using RedeSocialTBD.Models;

namespace RedeSocialTBD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicacaoController : Controller
    {
        private readonly IMongoCollection<PublicacaoModel> _publicacoes;
        private readonly IMongoCollection<UsuarioModel> _usuarios;

        public PublicacaoController(MongoDbService mongoDbService)
        {
            _publicacoes = mongoDbService.Database?.GetCollection<PublicacaoModel>("publicacoes");
            _usuarios = mongoDbService.Database?.GetCollection<UsuarioModel>("usuarios");
        }

        [HttpGet]
        public async Task<IEnumerable<PublicacaoModel>> Get()
        {
            return await _publicacoes.Find(FilterDefinition<PublicacaoModel>.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublicacaoModel?>> GetbyId(string id)
        {
            var filtro = Builders<PublicacaoModel>.Filter.Eq(x => x.Id, id);
            PublicacaoModel publicacao = await _publicacoes.Find(filtro).FirstOrDefaultAsync();

            return publicacao is not null ? Ok(publicacao) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(PublicacaoModel publicacao)
        {
            var usuarioExiste = await _usuarios.Find(u => u.Id == publicacao.IdUsuario).AnyAsync();
            if (!usuarioExiste)
                return BadRequest("O usuário informado não existe.");
            await _publicacoes.InsertOneAsync(publicacao);
            return CreatedAtAction(nameof(GetbyId), new { id = publicacao.Id }, publicacao);
        }

        [HttpPut]
        public async Task<ActionResult> Update(PublicacaoModel publicacao)
        {
            var filtro = Builders<PublicacaoModel>.Filter.Eq(x => x.Id, publicacao.Id);
            var update = Builders<PublicacaoModel>.Update
                .Set(x => x.Descricao, publicacao.Descricao)
                .Set(x => x.Data, publicacao.Data)
                .Set(x => x.Hashtags, publicacao.Hashtags);
            await _publicacoes.UpdateOneAsync(filtro, update);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filtro = Builders<PublicacaoModel>.Filter.Eq(x => x.Id, id);
            await _publicacoes.DeleteOneAsync(filtro);
            return Ok();
        }
    }
}
