using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        //[BsonId]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        private string _id { get; set; }
        public string Id
        {
            get
            {
                if (_id == null || _id == string.Empty)
                    _id = ObjectId.GenerateNewId().ToString();
                return _id;
            }
            set => _id = value;
        }
    }
}
