using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using NuGet.Protocol;
using webMalefashion.Models;

namespace webMalefashion.Controllers; 

public class AccountController : Controller {
    public static Customer? _currentCustomer = new();
    private readonly MalefashionContext db = new();
    
    private readonly IConfiguration _config;
    public AccountController(IConfiguration config)
    {
        _config = config;
    }
    
    public IActionResult Register() {
        return View();
    }
    
    [SuppressMessage("ReSharper.DPA", "DPA0009: High execution time of DB command")]
    public IActionResult Signin() {
        if (Request.Cookies["name"] == null) {
            return View();
        }
        return RedirectToAction("UserDetails", "Home");
    }

    [HttpPost]
    [Route("api/user/register")]
    [SuppressMessage("ReSharper.DPA", "DPA0009: High execution time of DB command", MessageId = "time: 571ms")]
    public OkObjectResult RegisterApi([FromBody] dynamic data) {
        var format = "yyyy-MM-dd"; // your datetime format
        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
        
        _currentCustomer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data.ToString(), dateTimeConverter);

        int id = 1;
        try {
            id = db.Customers.Select(c => c.Id).Max() + 1;
        }
        catch (Exception) {
            id = 1;
        }
        _currentCustomer.Id = id;
        _currentCustomer.Role = "User";
        
        // encode password
        if (_currentCustomer.Password != null) _currentCustomer.Password = Sha256(_currentCustomer.Password);
        db.Customers.Add(_currentCustomer);
        db.SaveChangesAsync();

        var response = new {
            token = GenerateToken(_currentCustomer),
            name = _currentCustomer.Name,
            email = _currentCustomer.Email,
            role = _currentCustomer.Role
        };
        
        HttpContext.Response.Cookies.Append("token", response.token,
            // cookie expired for 7 days
            new CookieOptions{Expires = DateTime.Now.AddDays(7)});
        HttpContext.Response.Cookies.Append("name", response.name);

        return Ok(response);
    }

    [HttpPost]
    [Route("api/user/signin")]
    [SuppressMessage("ReSharper.DPA", "DPA0009: High execution time of DB command", MessageId = "time: 666ms")]
    public ActionResult SigninApi([FromBody] dynamic data) {
        Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data.ToString());
        string password = Sha256(customer.Password);
        
        _currentCustomer = db.Customers
            .FirstOrDefault(x => 
                x.Email.ToLower() == customer.Email.ToLower() && 
                x.Password == password);
        
        if (_currentCustomer != null) {
            var response = new {
                token = GenerateToken(_currentCustomer),
                name = _currentCustomer.Name,
                email = _currentCustomer.Email,
                role = _currentCustomer.Role
            };
        
            HttpContext.Response.Cookies.Append("token", response.token,
                // cookie expired for 7 days
                new CookieOptions{Expires = DateTime.Now.AddDays(7)});

            HttpContext.Response.Cookies.Append("name", response.name);

            return Ok(response);
        }
        return Unauthorized("username or password is in correct");
    }

    [HttpPut]
    [Route("api/user/update")]
    [Authorize]
    [SuppressMessage("ReSharper.DPA", "DPA0009: High execution time of DB command", MessageId = "time: 666ms")]

    public ActionResult UpdateApi([FromBody] dynamic data) {
        var format = "yyyy-MM-dd"; // your datetime format
        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

        var securityToken = new JwtSecurityToken(Request.Cookies["token"]);
        var tokenData = securityToken.Claims.First(c => c.Type == ClaimTypes.UserData).Value;
        
        Customer customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(tokenData);
        Customer customerNewData = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(data.ToString(), dateTimeConverter);
        
        _currentCustomer = db.Customers.First(c => c.Id == customer.Id);

        _currentCustomer.Name = customerNewData.Name.IsNullOrEmpty() ? _currentCustomer.Name : customerNewData.Name;
        _currentCustomer.DateOfBirth = customerNewData.DateOfBirth;
        _currentCustomer.PhoneNumber = customerNewData.PhoneNumber.IsNullOrEmpty() ? _currentCustomer.PhoneNumber : customerNewData.PhoneNumber;
        _currentCustomer.Email = customerNewData.Email.IsNullOrEmpty() ? _currentCustomer.Email : customerNewData.Email;
        _currentCustomer.Password = customerNewData.Password.IsNullOrEmpty() ? _currentCustomer.Password : customerNewData.Password;
        _currentCustomer.Address = customerNewData.Address.IsNullOrEmpty() ? _currentCustomer.Address : customerNewData.Address;

        db.Customers.Update(_currentCustomer);
        db.SaveChangesAsync();
        
        var response = new {
            token = GenerateToken(_currentCustomer),
            name = _currentCustomer.Name,
            email = _currentCustomer.Email,
            role = _currentCustomer.Role
        };
        
        HttpContext.Response.Cookies.Append("token", response.token,
            // cookie expired for 7 days
            new CookieOptions{Expires = DateTime.Now.AddDays(7)});

        HttpContext.Response.Cookies.Append("name", response.name);

        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("api/account/validate")]
    public ActionResult ValidateToken() {
        return Ok();
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
    
    private string GenerateToken(Customer user)
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