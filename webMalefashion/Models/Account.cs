namespace webMalefashion.Models; 

public class Account {
    public Account(string? username, string? password) {
        Username = username;
        Password = password;
        Role = "User";
    }

    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}