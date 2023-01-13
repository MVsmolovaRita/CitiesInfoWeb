using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CitiesInfoWeb.Controllers
{//authentification- проверка данных в базе
 //autharization - проверка данных пользователя на доступ
    [ApiController] //для проверок ошибок с model state, иструмент для выполнения 

    [Route("api/authentification")]

    

    
    public class AuthentificationController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthentificationController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        public class AuthentificationPayLoad
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("authentificate")]
        public ActionResult<string> Authentificate(AuthentificationPayLoad authentificationPayLoad)
        {
            //проверить userName и password

            var user = ValidateUserCredential(authentificationPayLoad.UserName,
                authentificationPayLoad.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            //создание token
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentification:SecretForKey"]));
            var loginCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);//подпись для ключа
            // требования
            var claimForToken = new List<Claim>();
            claimForToken.Add(new Claim("Id",user.Id.ToString()));
            claimForToken.Add(new Claim("First_Name", user.FirstName));
            claimForToken.Add(new Claim("Last_Name", user.LastName));
            claimForToken.Add(new Claim("City", user.City));

            var jwt = new JwtSecurityToken(
                _configuration["Authentification:Issuer"],
                _configuration["Authentification:Audience"],
                claimForToken, //payload
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                loginCredentials
                ) ;
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(tokenToReturn);
        }

        private CitiesInfoWebUser ValidateUserCredential(string userName, string password)
        {
            return new CitiesInfoWebUser(1, "Steve5", "Steve", "Jonson", "London");
        }
        private class CitiesInfoWebUser
        {

            public int Id { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public CitiesInfoWebUser(int id, string userName, string firstName, string lastName, string city)
            {
                Id = id;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;

            }

        }
    }


}
