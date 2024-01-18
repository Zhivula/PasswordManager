using PasswordManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModel
{
    class RecordsViewModel : INotifyPropertyChanged
    {
        private string sHA512_EN;
        private string sHA512_DE;

        public string SHA512_EN
        {
            get => sHA512_EN;
            set
            {
                sHA512_EN = value;
                OnPropertyChanged(nameof(SHA512_EN));
            }
        }
        public string SHA512_DE
        {
            get => sHA512_DE;
            set
            {
                sHA512_DE = value;
                OnPropertyChanged(nameof(SHA512_DE));
            }
        }
        public RecordsViewModel()
        {
            string passPhrase = "TestPassphrase";        //Может быть любой строкой
            string saltValue = "TestSaltValue";        // Может быть любой строкой
            string hashAlgorithm = "SHA256";             // может быть "MD5"
            int passwordIterations = 3;                //Может быть любым числом
            string initVector = "5B19443818A7FFAA"; // Должно быть 16 байт
            int keySize = 256;                // Может быть 192 или 128
            string plainText = "HELLO123456HELLO123456";

            SHA512_EN = Data.SHA256.Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            SHA512_DE = Data.SHA256.Decrypt(SHA512_EN, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion 
    }
}
