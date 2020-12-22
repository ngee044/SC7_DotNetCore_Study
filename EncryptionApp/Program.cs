using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace EncryptionApp
{
    public static class Protector
    {
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BABABAS");
        private static readonly int iterations = 2000;

        public static string Encrypt(string plainText, string password)
        {
            return "";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
