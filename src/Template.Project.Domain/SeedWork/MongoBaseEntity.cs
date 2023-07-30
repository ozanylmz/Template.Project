using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Template.Project.Domain.SeedWork
{
    public abstract class MongoBaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; private set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedDate { get; private set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime? UpdateDate { get; private set; }

        protected MongoBaseEntity()
        {
            this.Id =ObjectId.GenerateNewId().ToString();
            this.CreatedDate = DateTime.Now.ToUniversalTime();
        }
        public void SetId(string id) => Id = id;

        public void SetCreatedDate(DateTime createdDate) => CreatedDate = createdDate;
        public void SetUpdateDate() => UpdateDate = DateTime.Now.ToUniversalTime();
    }
}
