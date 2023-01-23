using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

/*
 * Este archivo completo lo pueden copiar y pegar en su proyecto.
 * Para esto, tienen que tener instalado "AngleSharp" en su proyecto, y asegurense
 * de cambiar el nombre del namespace al que están usando.
 */

namespace CurrencyConverter
{
    // esto es símplemente para agrupar los valores de cada tasa
    public struct Tasa
    {
        public string entity;
        public float Valor;
        public string MonedaOrigen;
        public string MonedaDestino;

        public Tasa(float valor, string monedaOrigen, string monedaDestino, string entidad = "")
        {
            entity = entidad;
            Valor = valor;
            MonedaOrigen = monedaOrigen;
            MonedaDestino = monedaDestino;
        }

    }

    public class BuscadorTasas : IBuscadorTasas
    {

        public async Task<List<Tasa>> ObtenerTasas()
        {
            List<Tasa> tasas = new List<Tasa>();
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.infodolar.com.do/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address); //deben de sustituirlo por un await
            var cells = document.QuerySelectorAll("table#Dolar tbody tr");

            foreach (var i in cells)
            {
                var bankName = i.Children[0].QuerySelector("span.nombre")?.TextContent.Trim() ?? "";
                var buyPriceConSimbolo = i.Children[1].TextContent.Split('\n')[1].Trim();
                var sellPriceConSimbolo = i.Children[2].TextContent.Split('\n')[1].Trim();
                float buyPrice = buyPriceConSimbolo != "" ? Convert.ToSingle(buyPriceConSimbolo.Replace("$", "")) : 0.0f;
                float sellPrice = sellPriceConSimbolo != "" ? Convert.ToSingle(sellPriceConSimbolo.Replace("$", "")) : 0.0f;
                tasas.Add(new Tasa(buyPrice, "USD", "DOP", bankName));
                tasas.Add(new Tasa(sellPrice, "DOP", "USD", bankName));
            }
            return tasas;

        }

        internal Task <float>GetExchangeRateAsync()
        {
            throw new NotImplementedException();
        }
    }

}

