using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace RedeSocialTBD.Models
{
    public class PublicacaoModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("id_usuario"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario { get; set; }

        [BsonElement("descricao")]
        public string? Descricao { get; set; }

        [BsonElement("data")]
        public DateTime? Data { get; set; }

        [BsonElement("hashtags")]
        public List<string>? Hashtags { get; set; } = new List<string>();
    }
}
