using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RedeSocialTBD.Models
{
    public class ComentarioModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("idUsuario"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario { get; set; }

        [BsonElement("idPublicacao"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdPublicacao { get; set; }

        [BsonElement("descricao")]
        public string? Descricao { get; set; }

        [BsonElement("data")]
        public DateTime? Data { get; set; }
    }
}
