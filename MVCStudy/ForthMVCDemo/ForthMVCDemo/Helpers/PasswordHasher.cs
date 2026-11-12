using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    private const int SaltSize = 16, HashSize = 32, Iterations = 100000;

    public static string CreateSalt()
    {
        var salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    public static string Hash(string password, string saltBase64)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password,
                   Convert.FromBase64String(saltBase64), Iterations))
            return Convert.ToBase64String(pbkdf2.GetBytes(HashSize));
    }
}