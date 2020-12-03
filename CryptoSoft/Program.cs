using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoSoft

{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "";
            string destination = "";
            const string KEYPATH = @"C:\Users\Gabriel\Documents\key.txt";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-s" || args[i] == "--source")
                {
                    source = args[i + 1];
                }
                if (args[i] == "-d" || args[i] == "--destination")
                {
                    destination = args[i + 1];
                }
            }
            FileEncrypt((StringEncrypter(FileFinder(source, destination), KeyFinder(KEYPATH))), source, destination);
            Console.WriteLine("Encryption working... Please wait");
        }

        public static string FileFinder(string source, string destination)
        {
            string filePath = source;
            string dest = destination;

            Console.WriteLine($"Encrypt from {filePath} to {dest}");

            string textToEncrypt = File.ReadAllText(filePath);

            return textToEncrypt;
        }

        public static string KeyFinder(string source)
        {
            string key = File.ReadAllText(source);
            return key;
        }

        public static string StringEncrypter(string text, string key)
        {
            List<int> ascii = new List<int>();
            List<int> enc = new List<int>();
            string encryptedString = "";

            byte[] bytes = Encoding.ASCII.GetBytes(text);
            foreach (byte b in bytes)
            {
                ascii.Add(b);
            }
            for (int i = 0; i < ascii.Count; i++)
            {
                int j = 0;
                enc.Add(ascii[i] ^ key[j]);
                j++;
                encryptedString += char.ConvertFromUtf32(enc[i]);
            }
            return encryptedString;
        }

        public static void FileEncrypt(string encryptedString, string source, string destination)
        {
            string fileName = Path.Combine(Path.GetFileName(source) + ".encrypted");
            File.Copy(source, $@"{destination}\{fileName}");
            File.WriteAllText($@"{destination}\{fileName}", encryptedString);
        }
    }
}