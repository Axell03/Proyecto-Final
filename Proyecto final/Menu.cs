using CurrencyConverter;
using System;
using System.Collections.Generic;
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
                Console.WriteLine("12. Salir");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        CreateCategory();
                        break;
                    case "2":
                        ReadCategories();
                        break;
                    case "3":
                        UpdateCategory();
                        break;
                    case "4":
                        DeleteCategory();
                        break;
                    case "5":
                        CreateAccount();
                        break;
                    case "6":
                        ReadAccounts();
                        break;
                    case "7":
                        UpdateAccount();
                        break;
                    case "8":
                        DeleteAccount();
                        break;
                    case "9":
                        AddTransaction();
                        break;
                    case "10":
                        CalculateCurrentBalance();
                        break;
                    case "11":
                        ConvertCurrency();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }

        private static List<Category> _categories = new List<Category>();

        private static void CreateCategory()
        {
            Console.WriteLine("Ingrese el nombre de la nueva categoría:");
            var name = Console.ReadLine();
            var newCategory = new Category { Name = name };
            _categories.Add(newCategory);
            Console.WriteLine("La categoría ha sido creada");
        }

        private static void ReadCategories()
        {
            if (_categories.Count == 0)
            {
                Console.WriteLine("No hay categorías creadas");
                return;
            }

            Console.WriteLine("Categorías existentes:");
            foreach (var category in _categories)
            {
                Console.WriteLine(category.Name);
            }
        }

        private static void UpdateCategory()
        {
            Console.WriteLine("Ingrese el nombre de la categoría a actualizar:");
            var name = Console.ReadLine();
            var category = _categories.FirstOrDefault(c => c.Name == name);
            if (category == null)
            {
                Console.WriteLine("La categoría no existe");
                return;
            }

            Console.WriteLine("Ingrese el nuevo nombre de la categoría:");
            category.Name = Console.ReadLine();
            Console.WriteLine("La categoría ha sido actualizada");
        }

        private static void DeleteCategory()
        {
            Console.WriteLine("Ingrese el nombre de la categoría a eliminar:");
            var name = Console.ReadLine();
            var category = _categories.FirstOrDefault(c => c.Name == name);
            if (category == null)
            {
                Console.WriteLine("La categoría no existe");
                return;
            }

            _categories.Remove(category);
            Console.WriteLine("La categoría ha sido eliminada");
        }

        private static void CreateAccount()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var name = Console.ReadLine();
            _expenseTracker.CreateAccount(name);
            Console.WriteLine("Cuenta creada");
        }

        private static void ReadAccounts()
        {
            var accounts = _expenseTracker.ReadAccounts();
            Console.WriteLine("Cuentas: ");
            foreach (var account in accounts)
            {
                Console.WriteLine(account.Name);
            }
        }

        private static void UpdateAccount()
        {
            Console.WriteLine("Ingrese el nombre antiguo de la cuenta: ");
            var oldName = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo nombre de la cuenta: ");
            var newName = Console.ReadLine();
            _expenseTracker.UpdateAccount(oldName, newName);
            Console.WriteLine("Cuenta actualizada");
        }

        private static void DeleteAccount()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta a eliminar: ");
            var name = Console.ReadLine();
            _expenseTracker.DeleteAccount(name);
            Console.WriteLine("Cuenta eliminada");
        }

        private static void AddTransaction()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var accountName = Console.ReadLine();
            Console.WriteLine("Ingrese el tipo de transacción (Ingreso/Gastos): ");
            var type = Console.ReadLine();
            Console.WriteLine("Ingrese la categoría: ");
            var category = Console.ReadLine();
            Console.WriteLine("Ingrese el monto: ");
            var amount = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la descripción: ");
            var description = Console.ReadLine();
            Console.WriteLine("Ingrese la fecha (dd/mm/yyyy): ");
            var date = DateTime.Parse(s: Console.ReadLine());
            _expenseTracker.AddTransaction(accountName, type, category, amount, description, date);
            Console.WriteLine("Transacción agregada");
        }

        private static void CalculateCurrentBalance()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var accountName = Console.ReadLine();
            var balance = _expenseTracker.CalculateCurrentBalance(accountName);
            Console.WriteLine("Saldo actual: " + balance);
        }

        private static async void ConvertCurrency()
        {

            Console.WriteLine("Ingrese el monto a convertir: ");
            var amount = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la moneda de origen (USD): ");
            var fromCurrency = Console.ReadLine();
            Console.WriteLine("Ingrese la moneda de destino (DOP): ");
            var toCurrency = Console.ReadLine();
            var buscador = new BuscadorTasas();
            var result = await _expenseTracker.ConvertCurrencyAsync(amount, fromCurrency, toCurrency, buscador);
            Console.WriteLine("Monto convertido: " + result);


        }
    }
}
