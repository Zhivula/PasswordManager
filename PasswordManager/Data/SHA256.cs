﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Data
{
    public class SHA256
    {
        public static string passPhrase = "TestPassphrase";      //Может быть любой строкой
        public static string saltValue = "TestSaltValue";        // Может быть любой строкой
        public static string hashAlgorithm = "SHA256";           // может быть "MD5"
        public static int passwordIterations = 10;               //Может быть любым числом
        public static string initVector = "5B19443818A7FFAA";    // Должно быть 16 байт
        public static int keySize = 256;                         // Может быть 192 или 128
        public static string plainText = "HELLO123456HELLO123456";

        public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            //Преобразование строк в байтовые массивы.
            //Предположим, что строки содержат только коды ASCII.
            //Если строки содержат символы Юникода, используйте Юникод, UTF7 или UTF8
            //Кодировки.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            //Преобразование открытого текста в массив байтов.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Во-первых, мы должны создать пароль, из которого будет получен ключ.
            //Этот пароль будет создан из указанной парольной фразы и
            //соли. Пароль будет создан с использованием указанного хэша
            //алгоритма. Создание пароля может выполняться в нескольких итерациях.
            PasswordDeriveBytes password = new PasswordDeriveBytes
            (
                passPhrase,
                saltValueBytes,
                hashAlgorithm,
                passwordIterations
            );

            //Используйте пароль для создания псевдослучайных байтов для шифрования
            //ключа. Укажите размер ключа в байтах (вместо битов).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            //Создать неинициализированный объект шифрования Rijndael.
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            //Создать шифратор из существующих байтов ключа и инициализации
            //вектор. Размер ключа определяется на основе количества байтов ключа.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );

            //Определите поток памяти, который будет использоваться для хранения зашифрованных данных.
            MemoryStream memoryStream = new MemoryStream();

            //Определите криптографический поток (всегда используйте режим записи для шифрования).
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            //Начните шифровать.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            //Шифровка конца.
            cryptoStream.FlushFinalBlock();

            //Преобразование зашифрованных данных из потока памяти в массив байтов.
            byte[] cipherTextBytes = memoryStream.ToArray();

            //Закройте оба потока.
            memoryStream.Close();
            cryptoStream.Close();

            //Преобразование зашифрованных данных в строку в кодировке base64.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            //Возвратите зашифрованную последовательность.
            return cipherText;
        }

        public static string Decrypt
    (
        string cipherText,
        string passPhrase,
        string saltValue,
        string hashAlgorithm,
        int passwordIterations,
        string initVector,
        int keySize
    )
    {
        //Преобразование строк, определяющих характеристики ключа шифрования, в байтовые массивы.
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
 
        //Преобразование нашего зашифрованного текста в массив байтов.
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
 
        //Во-первых, мы должны создать пароль, из которого будет получен ключ
        //Этот пароль будет создан из указанной парольной фразы и значения соли.
        //Пароль будет создан с использованием указанного алгоритма хэша. Создание пароля может выполняться в нескольких итерациях.
        PasswordDeriveBytes password = new PasswordDeriveBytes
        (
            passPhrase,
            saltValueBytes,
            hashAlgorithm,
            passwordIterations
        );
 
        //Используйте пароль для создания псевдослучайных байтов для шифрования
        //ключа. Укажите размер ключа в байтах (вместо битов).
        byte[] keyBytes = password.GetBytes(keySize / 8);
 
        //Создать неинициализированный объект шифрования Rijndael.
        RijndaelManaged symmetricKey = new RijndaelManaged();
 
        //Целесообразно установить режим шифрования "Цепочка блоков шифрования"
        //(CBC). Используйте параметры по умолчанию для других симметричных ключевых параметров.
        symmetricKey.Mode = CipherMode.CBC;
 
        //Создать дешифратор из существующих байтов ключа и инициализации
        //вектора. Размер ключа определяется на основе номера ключа
        //байты.
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor
        (
            keyBytes,
            initVectorBytes
        );
 
        //Определите поток памяти, который будет использоваться для хранения зашифрованных данных.
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
 
        //Определите криптографический поток (всегда используйте режим чтения для шифрования).
        CryptoStream cryptoStream = new CryptoStream
        (
            memoryStream,
            decryptor,
            CryptoStreamMode.Read
        );
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
 
        //Начните расшифровывать.
        int decryptedByteCount = cryptoStream.Read
        (
            plainTextBytes,
            0,
            plainTextBytes.Length
        );
 
        //Закройте оба потока.
        memoryStream.Close();
        cryptoStream.Close();
 
        //Преобразование расшифрованных данных в строку.
        //Предположим, что исходная строка открытого текста была UTF8-encoded.
        string plainText = Encoding.UTF8.GetString
        (
            plainTextBytes,
            0,
            decryptedByteCount
        );
 
        //Возвратите расшифрованную последовательность.  
        return plainText;
    }
    }
}
