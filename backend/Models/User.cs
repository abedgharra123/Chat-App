using System;

namespace backend.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required byte[] Password { get; set; }
    public required byte[] PasswordKey { get; set; }
}
