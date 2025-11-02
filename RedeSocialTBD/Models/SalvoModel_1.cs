using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RedeSocialTBD.Models
{
    public class SalvoModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public string? Nome { get; set; }

        [BsonElement("userName")]
        public string? UserName { get; set; }

        [BsonElement("senha")]
        public string? Senha { get; set; }

        [BsonElement("dataNascimento")]
        public DateTime? DataNascimento { get; set; }
    }
}
