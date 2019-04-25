using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenomeadorDeArquivos.Entities.Exceptions
{
    class ImageException : ApplicationException
    {
        public ImageException(string message) : base(message)
        {
        }
    }
}
