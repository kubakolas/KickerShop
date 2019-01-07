using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KickerShop.Models
{
    public class OrderTime
    {
        [BsonId]
        public ObjectId ID { get; set; }
        [BsonElement("clientName")]
        public string clientName { get; set; }
        [BsonElement("orderId")]
        public int orderId { get; set; }
        [BsonElement("time")]
        public TimeSpan time { get; set; }
}
}