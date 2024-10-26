using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    // Hashes the password using SHA-256 without a salt
    public static string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }

    // Verifies the password by comparing the hashed value with the stored hash
    public static bool VerifyPassword(string password, string storedHash)
    {
        var hashToVerify = HashPassword(password);
        return hashToVerify == storedHash;
    }
}
