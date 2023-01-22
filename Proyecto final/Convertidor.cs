using CurrencyConverter;
using Proyecto_final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    public class ConvertidorDeMoneda
    {
        /* DEPENDENCY INJECTION - Esta clase requiere un IBuscadorTasas en el constructor
         * lo que permite en los casos de pruebas pasarle el stub */
        IBuscadorTasas buscadorTasas;
        public ConvertidorDeMoneda(IBuscadorTasas buscadorTasas)
        {
            this.buscadorTasas = buscadorTasas;
        }

        // Esto no es necesario para su programa. Esto sólo es mostrando como recorrer la lista de tasas
        public void MostrarReporteDeTasas()
        {
            var tasas = buscadorTasas.ObtenerTasas();
            foreach (Tasa tasa in tasas)
                Console.WriteLine($"{tasa.Entidad,-45}  {tasa.Valor,6}  {tasa.MonedaOrigen}->{tasa.MonedaDestino}");
        }

        public float ComprarDolaresEnElPopular(float valorEnPesos)
        {
            // Aquí es que se hace la llamada que navega por internet.
            // En los casos de pruebas buscadorTasas será una instancia del stub, y por lo tanto, no irá a internet
            var tasas = buscadorTasas.ObtenerTasas();

            // Filtrando lista, según criterios Entidad, MonedaOrigen y Destino, y tomando el 1er resultado
            var tasaVentaDolaresPopular = tasas.Where(x => x.Entidad == "Banco Popular"
                                                     && x.MonedaOrigen == "USD"
                                                     && x.MonedaDestino == "DOP").First();

            /* Debido a que todas las tasas están en DOP, para convertir de Pesos a Dólares se debe dividir,
             mientras que para convertir de Dólares a Pesos se debe multiplicar. Es decir:
                   
                   DOP->USD:    cantidadDolares = cantidadPesos   ÷ tasaPesos
                   USD->DOP:    cantidadPesos   = cantidadDolares × tasaPesos 
            */
            return valorEnPesos / tasaVentaDolaresPopular.Valor;
        }
    }
}

