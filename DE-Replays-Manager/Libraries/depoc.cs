using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class depoc
{
    public static string systemdir = "4UGuysDedicated";

    public static string tkDB = "ALB5pTyp3NsAAAAAAAAAAe_RlDlXjkWehC9zrKPjFHVyM2MWtIK7S-ihNI-MfSF8";

    public static byte[] MDebuf(byte[] cipherData, byte[] Key, byte[] IV)
    {
        MemoryStream memoryStream = new MemoryStream();
        Rijndael rijndael = Rijndael.Create();
        rijndael.Key = Key;
        rijndael.IV = IV;
        CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(cipherData, 0, cipherData.Length);
        cryptoStream.Close();
        return memoryStream.ToArray();
    }

    public static string MDebuf(string cipherText, string Password)
    {
        string text = Reverse(Password);
        text = text.Replace("X", "0").Replace("D", "6");
        string strPassword = text;
        byte[] cipherData = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(strPassword, new byte[13]
        {
            73, 118, 97, 110, 32, 77, 101, 100, 118, 101,
            100, 101, 118
        });
        byte[] bytes = MDebuf(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
        return Encoding.Unicode.GetString(bytes);
    }

    public static string Reverse(string s)
    {
        char[] array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }
}