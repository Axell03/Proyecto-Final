using CurrencyConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final
{
    class Program
    {
        static ExpenseTracker _expenseTracker;
        static void Main(string[] args)
        {
           
            
            _expenseTracker = new ExpenseTracker(new BuscadorTasas());
            while (true)
            {
                Console.WriteLine("\nMenu de opciones:");
                Console.WriteLine("1. Crear una categoría");
                Console.WriteLine("2. Leer categorías");
                Console.WriteLine("3. Actualizar una categoría");
                Console.WriteLine("4. Eliminar una categoría");
                Console.WriteLine("5. Crear una cuenta");
                Console.WriteLine("6. Leer cuentas");
                Console.WriteLine("7. Actualizar una cuenta");
                Console.WriteLine("8. Eliminar una cuenta");
                Console.WriteLine("9. Agregar una transacción");
                Console.WriteLine("10. Calcular el saldo actual de una cuenta");
                Console.WriteLine("11. Convertir moneda");
                Console.WriteLine("12. Generar resumen de gastos por cuenta");
                Console.WriteLine("13. Salir");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        MenuReal.CreateCategory();
                        break;
                    case "2":
                        MenuReal.ReadCategories();
                        break;
                    case "3":
                        MenuReal.UpdateCategory();
                        break;
                    case "4":
                        MenuReal.DeleteCategory();
                        break;
                    case "5":
                        MenuReal.CreateAccount();
                        break;
                    case "6":
                        MenuReal.ReadAccounts();
                        break;
                    case "7":
                        MenuReal.UpdateAccount();
                        break;
                    case "8":
                        MenuReal.DeleteAccount();
                        break;
                    case "9":
                        MenuReal.AddTransaction();
                        break;
                    case "10":
                        MenuReal.CalculateCurrentBalance();
                        break;
                    case "11":
                        MenuReal.ConvertCurrency();
                        break;
                    case "12":
                        MenuReal.ShowExpenseSummaryByAccount();
                        break;
                    case "13":
                        return;

                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }




    }
}
