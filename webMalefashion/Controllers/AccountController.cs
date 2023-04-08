using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using NuGet.Protocol;
using webMalefashion.Models;

namespace webMalefashion.Controllers; 

public class AccountController : Controller {

    private readonly MalefashionContext db = new();
    
    private readonly IConfiguration _config;
    public AccountController(IConfiguration config)
    {
        _config = config;
    }
    
    
    public IActionResult Register() {
        return View();
    }
    
    public IActionResult Signin() {
        return View();
    }

    [HttpPost]
    [Route("api/user/register")]
    public OkObjectResult RegisterApi([FromBody] dynamic data) {
        var format = "dd/MM/yyyy"; // your datetime format
        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
        
        Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data.ToString(), dateTimeConverter);

        var id = db.Customers.Select(c => c.Id).Max() + 1;
        customer.Id = id;
        
        // encode password
        if (customer.Password != null) customer.Password = Sha256(customer.Password);
        db.Customers.Add(customer);
        db.SaveChangesAsync();

        return Ok(new {
            token = GenerateToken(new Account(customer.Username, customer.Password)),
            username = customer.Username,
            email = customer.Email
        });
    }

    [HttpPost]
    [Route("api/user/signin")]
    public ActionResult SigninApi([FromBody] dynamic data) {
        var format = "dd/MM/yyyy";
        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
        
        Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data.ToString(), dateTimeConverter);

        string password = Sha256(customer.Password);
        Customer currentCustomer = db.Customers
            .FirstOrDefault(x => 
                x.Username.ToLower() == customer.Username.ToLower() && 
                x.Password == password);
        if (currentCustomer != null) {
            return Ok(new {
                token = GenerateToken(new Account(currentCustomer.Username, currentCustomer.Password)),
                username = currentCustomer.Username,
                email = currentCustomer.Email
            });
        }
        return Unauthorized("username or password is in correct");
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    [Route("api/test")]
    public string TestToken() {
        return "Congratulation, token is valid";
    }
    
    static string Sha256(string password)
    {
        var crypt = new SHA256Managed();
        string hash = String.Empty;
        byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password));
        foreach (byte theByte in crypto)
        {
            hash += theByte.ToString("x2");
        }
        return hash;
    }
    
    private string GenerateToken(Account user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userString = Newtonsoft.Json.JsonConvert.SerializeObject(user);
        var claims = new[]
        {
            new Claim(ClaimTypes.UserData,userString),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}