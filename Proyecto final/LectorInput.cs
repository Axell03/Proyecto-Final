using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    public class LectorInput : ILectorInput
    {
        public string Leer()
        {
            return Console.ReadLine();
        }
    }

    public interface ILectorInput
    {
        string Leer();
    }
}
