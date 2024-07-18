using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearnMongo.Models;
using LearnMongo.Resource;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace LearnMongo.Services
{

    public interface IUserService
    {
        public Task<User?> GetAsync(string id);

        public Task Register(User newUser);

        public Task<LoginResource> Login(string email, string password);

        public void Logout();

        public Task UpdateProfile(string id, User updateUser);

        public void ViewDashboard();
    }

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                bookStoreDatabaseSettings.Value.UsersCollectionName);

        }

        public async Task<User?> GetAsync(string id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Register(User newUser)
        {
            await _usersCollection.InsertOneAsync(newUser);
        }

        public async Task<LoginResource> Login(string email,string password)
        {
          var user=  await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

          var resource = new LoginResource();

          if (user is null) return resource;

          if (user.Password != password) return resource;

          resource.Token= Authenticate(user.Name, user.Password);
          resource.Result = true;

          return resource;

        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProfile(string id,User updateUser)
        {
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updateUser);
        }

        public void ViewDashboard()
        {
            throw new NotImplementedException();
        }

        public string Authenticate(string username, string password)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("MalekmalekMalekMalekMalekmalekMalekMalek$$$$");

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(tokenKey),
                     SecurityAlgorithms.HmacSha256Signature)
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
