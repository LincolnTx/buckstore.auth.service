using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace buckstore.auth.service.application.Services
{
    public class UserQueryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set;}
        
        [BsonElement]
        public string Name { get; set; }
        
        [BsonElement]
        public string Email { get; set;}
        
        [BsonElement]
        public string Password { get; set;}
        
        [BsonElement]
        public byte[] PasswordSalt { get; set;}
        
        [BsonElement]
        public string Cpf { get; set;}

        public UserQueryModel(string name, string email, string password, byte[] passwordSalt, string cpf)
        {
            UserId = ObjectId.GenerateNewId().ToString();
            Name = name;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            Cpf = cpf;
        }
    }
}