using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RenomeadorDeArquivos
{
    class Security
    {

        public string StringToSha1 (string normalString)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            StringBuilder cryptoString = new StringBuilder();

            byte[] buffer = Encoding.Default.GetBytes(normalString);
            buffer = sha1.ComputeHash(buffer);
            foreach (byte item in buffer)
            {
                cryptoString.Append(item.ToString("x2"));
            }
            return (cryptoString.ToString().ToUpper());
        }
    }
}
