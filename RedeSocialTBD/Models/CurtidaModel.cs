using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RedeSocialTBD.Models
{
    public class CurtidaModel
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("id_Publicacao"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdPublicacao { get; set; }

        [BsonElement("id_Usuario"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario { get; set; }

        [BsonElement("data")]
        public DateTime? Data { get; set; }
    }
}
