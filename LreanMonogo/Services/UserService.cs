using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MyFinancePal.Models;
using MyFinancePal.Resource;

namespace MyFinancePal.Services
{

    public interface IUserService
    {
        public Task<User?> GetAsync(string id);

        public  Task<List<User>> GetAllAsync();

        public Task<LoginResource> Register(User newUser);

        public Task<LoginResource> Login(string email, string password);

        public void Logout();

        public Task UpdateProfile(string id, User updateUser);

        public void ViewDashboard();
    }

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IGFGEncryption _encryption;
        private readonly ICategoryService _categoryService;

        public UserService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings, IGFGEncryption encryption, ICategoryService categoryService)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                bookStoreDatabaseSettings.Value.UsersCollectionName);
            _encryption = encryption;
            _categoryService = categoryService;
        }


        public async Task<List<User>> GetAllAsync()
        {
           return await _usersCollection.Find(x => true).ToListAsync();
        }

        public async Task<User?> GetAsync(string id)
        {
            var user = await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            user.Password = _encryption.decodeString(user.Password);
            return user;
        }

        public async Task<LoginResource> Register(User newUser)
        {
            newUser.Password = _encryption.encodeString(newUser.Password);
            if (!CheckExistingEmail(newUser.Email))
            {
                return new LoginResource { EmailAlreadyExist = true };
            }

            await _usersCollection.InsertOneAsync(newUser);

            var user = await _usersCollection.Find(x => x.Email == newUser.Email).FirstOrDefaultAsync();
            CreateDefaultCategories(user);

            return loginResource(user);
        }

        public async Task<LoginResource> Login(string email,string password)
        {
          var user=  await _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();


          if (user is null) return new LoginResource();

          if (_encryption.decodeString(user.Password) != password) return new LoginResource();

          return loginResource(user);

        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProfile(string id,User updateUser)
        {
            updateUser.Password = _encryption.encodeString(updateUser.Password);

            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updateUser);
        }

        public void ViewDashboard()
        {
            throw new NotImplementedException();
        }

        private string Authenticate(string username, string password)
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

        private LoginResource loginResource(User user)
        {
            return new LoginResource
            {
                Result=true,
                Token= Authenticate(user.Name, user.Password),
                UserId=user.Id
        };

        }
        private async void CreateDefaultCategories(User user)
        {
            if(user is not null)
            {
                await _categoryService.CreateDefaultCategories(user.Id);
            }
        }


        private bool CheckExistingEmail(string email)
        {
            var user =  _usersCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

            return user is null ? true : false;
        }
    }
}
