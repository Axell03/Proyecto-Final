using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;


namespace CurrencyConverter
{

    public interface IBuscadorTasas
    {
         Task<List<Tasa>> ObtenerTasas();
    }
}