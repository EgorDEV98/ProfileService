using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using ProfileService.Application.Configurations;

namespace ProfileService.Application.Common;

public interface IEncryptor
{
    string? Encrypt(string? plainText);
    string? Decrypt(string? cipherText);
}

public class AesEncryptor : IEncryptor
{
    private readonly byte[] key;
    private readonly byte[] iv;

    public AesEncryptor(IOptions<EncryptionOptions> options)
    {
        var encryptionKey = options.Value.Key;

        using (SHA256 sha256 = SHA256.Create())
        {
            this.key = sha256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
        }
        using (SHA256 sha256 = SHA256.Create())
        {
            this.iv = new byte[16];
            Array.Copy(sha256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey)), this.iv, this.iv.Length);
        }
    }

    public string Encrypt(string? plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            throw new ArgumentNullException(nameof(plainText));
        }

        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new MemoryStream();
        using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public string Decrypt(string? cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            throw new ArgumentNullException(nameof(cipherText));
        }

        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
        using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}