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
                Console.WriteLine("12. Generar reporte");
                Console.WriteLine("13. Salir");

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
                        GenerateReport();
                        return;
                    case "13":
                        return;

                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }

        private static List<Category> _categories = new List<Category>();
        private static string description;

        private static void CreateCategory()
        {
            Console.WriteLine("Ingrese el nombre de la nueva categoría:");
            var name = Console.ReadLine();
            var newCategory = new Category(name, description);
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
            Console.WriteLine("Ingrese el ID de la nueva cuenta: ");
            var id = Console.ReadLine();
            Console.WriteLine("Ingrese el email de la nueva cuenta: ");
            var email = Console.ReadLine();
            _expenseTracker.CreateAccount(name,id, email);
            var newAccount = new Account(name, id, email);

            Console.WriteLine("Cuenta creada");
        }

        private static void ReadAccounts()
        {
            var accounts = _expenseTracker.ReadAccounts();
            Console.WriteLine("Cuentas: ");
            foreach (var account in accounts)
            {
                Console.WriteLine(account.ID + " " + account.Name + " " + account.Email);
            }
        }

        private static void UpdateAccount()
        {
            Console.WriteLine("Ingrese el nombre antiguo de la cuenta: ");
            var oldName = Console.ReadLine();
            Console.WriteLine("Ingrese el ID antiguo de la cuenta: ");
            var oldId = Console.ReadLine();
            Console.WriteLine("Ingrese el email antiguo de la cuenta:");
            var oldEmail = Console.ReadLine();

            var newName = oldName;
            var newId = oldId;
            var newEmail = oldEmail;

            Console.WriteLine("¿Desea cambiar el nombre? (s/n)");
            var changeName = Console.ReadLine();
            if (changeName.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el nuevo nombre de la cuenta: ");
                newName = Console.ReadLine();
            }

            Console.WriteLine("¿Desea cambiar el ID? (s/n)");
            var changeId = Console.ReadLine();
            if (changeId.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el nuevo ID de la cuenta:");
                newId = Console.ReadLine();
            }

            Console.WriteLine("¿Desea cambiar el email? (s/n)");
            var changeEmail = Console.ReadLine();
            if (changeEmail.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el nuevo email de la cuenta:");
                newEmail = Console.ReadLine();
            }

            _expenseTracker.UpdateAccount(oldName, newName, oldId, newId, oldEmail, newEmail);
            Console.WriteLine("Cuenta actualizada");
        }


        private static void DeleteAccount()
        {
            Console.WriteLine("Ingrese el ID o el nombre de la cuenta a eliminar: ");
            var id = Console.ReadLine();
            _expenseTracker.DeleteAccount(id);
            Console.WriteLine("Cuenta eliminada");
        }


        private static void AddTransaction()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var accountName = Console.ReadLine();
            Console.WriteLine("Ingrese el ID de la cuenta: ");
            var accountId = Console.ReadLine();
            Console.WriteLine("Ingrese el tipo de transacción (Ingreso/Gastos): ");
            var type = Console.ReadLine();
            Console.WriteLine("Ingrese la categoría: ");
            var category = Console.ReadLine();
            Console.WriteLine("Ingrese el monto: ");
            var amount = double.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la descripción: ");
            var description = Console.ReadLine();
            Console.WriteLine("Ingrese la fecha (dd/mm/yyyy): ");
            var date = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _expenseTracker.AddTransaction(accountName, accountId, type, category, amount, description, date);
            Console.WriteLine("Transacción agregada");
        }

        private static void CalculateCurrentBalance()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var accountName = Console.ReadLine();
            Console.WriteLine("Ingrese el ID de la cuenta: ");
            var accountId = Console.ReadLine();
            var balance = _expenseTracker.CalculateCurrentBalance(accountName, accountId);
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
        private static void GenerateReport()
        {
            Console.WriteLine("Ingrese el nombre de la categoría: ");
            var category = Console.ReadLine();
            Console.WriteLine("Ingrese la fecha de inicio (dd/mm/yyyy): ");
            var startDateInput = Console.ReadLine();

            if (!DateTime.TryParseExact(startDateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate))
            {
                Console.WriteLine("Formato de fecha no válido, por favor ingrese una fecha en el formato dd/mm/yyyy");
                return;
            }

            Console.WriteLine("Ingrese la fecha final (dd/mm/yyyy): ");
            var endDateInput = Console.ReadLine();

            if (!DateTime.TryParseExact(endDateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
            {
                Console.WriteLine("Formato de fecha no válido, por favor ingrese una fecha en el formato dd/mm/yyyy");
                return;
            }

            var report = _expenseTracker.GenerateExpenseReportByCategory(category, startDate, endDate);
            Console.WriteLine("Gastos en la categoría " + category + " desde " + startDate.ToString("dd/MM/yyyy") + " hasta " + endDate.ToString("dd/MM/yyyy") + ":");
            Console.WriteLine("Total gastado: $" + report.TotalExpense);
            Console.WriteLine("Transacciones:");
            foreach (var t in report.Transactions)
            {
                Console.WriteLine("Fecha: " + t.Date.ToString("dd/MM/yyyy") + ", Monto: $" + t.Amount + ", Descripción: " + t.Description);
            }
        }


    }
}
