using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter;
using System.Globalization;
using Microsoft.Graph;



namespace Proyecto_final
{
    public static class MenuReal
    {

        static ExpenseTracker _expenseTracker = new ExpenseTracker(new BuscadorTasas());
        public static List<Category> _categories = new List<Category>();
        public static string description;


        public static ILectorInput Lector { get; set; } = new LectorInput();


        public static void CreateCategory()
        {
            Console.WriteLine("Ingrese el nombre de la nueva categoría:");
            var name = Lector.Leer();
            var newCategory = new Category(name, description);
            _categories.Add(newCategory);
            Console.WriteLine("La categoría ha sido creada");
        }

        public static void ReadCategories()
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

        public static void UpdateCategory()
        {
            Console.WriteLine("Ingrese el nombre de la categoría a actualizar:");
            var name = Lector.Leer();
            var category = _categories.FirstOrDefault(c => c.Name == name);
            if (category == null)
            {
                Console.WriteLine("La categoría no existe");
                return;
            }

            Console.WriteLine("Ingrese el nuevo nombre de la categoría:");
            category.Name = Lector.Leer();
            Console.WriteLine("La categoría ha sido actualizada");
        }

        public static void DeleteCategory()
        {
            Console.WriteLine("Ingrese el nombre de la categoría a eliminar:");
            var name = Lector.Leer();
            var category = _categories.FirstOrDefault(c => c.Name == name);
            if (category == null)
            {
                Console.WriteLine("La categoría no existe");
                return;
            }

            _categories.Remove(category);
            Console.WriteLine("La categoría ha sido eliminada");
        }

        public static void CreateAccount()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var name = Lector.Leer();
            Console.WriteLine("Ingrese el ID de la nueva cuenta: ");
            var id = Lector.Leer();
            Console.WriteLine("Ingrese el email de la nueva cuenta: ");
            var email = Lector.Leer();
            _expenseTracker.CreateAccount(name, id, email);
            var newAccount = new Account(name, id, email);

            Console.WriteLine("Cuenta creada");
        }

        public static void ReadAccounts()
        {
            var accounts = _expenseTracker.ReadAccounts();
            Console.WriteLine("Cuentas: ");
            foreach (var account in accounts)
            {
                Console.WriteLine(account.ID + " " + account.Name + " " + account.Email);
            }
        }

        public static void UpdateAccount()
        {
            Console.WriteLine("Ingrese el nombre antiguo de la cuenta: ");
            var oldName = Lector.Leer();
            Console.WriteLine("Ingrese el ID antiguo de la cuenta: ");
            var oldId = Lector.Leer();
            Console.WriteLine("Ingrese el email antiguo de la cuenta:");
            var oldEmail = Lector.Leer();

            var newName = oldName;
            var newId = oldId;
            var newEmail = oldEmail;

            Console.WriteLine("¿Desea cambiar el nombre? (s/n)");
            var changeName = Lector.Leer();
            if (changeName.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el nuevo nombre de la cuenta: ");
                newName = Lector.Leer();
            }

            Console.WriteLine("¿Desea cambiar el ID? (s/n)");
            var changeId = Lector.Leer();
            if (changeId.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el nuevo ID de la cuenta:");
                newId = Lector.Leer();
            }

            Console.WriteLine("¿Desea cambiar el email? (s/n)");
            var changeEmail = Lector.Leer();
            if (changeEmail.ToLower() == "s")
            {
                Console.WriteLine("Ingrese el nuevo email de la cuenta:");
                newEmail = Lector.Leer();
            }
            if (_expenseTracker.UpdateAccount(oldName, newName, oldId, newId, oldEmail, newEmail))
                Console.WriteLine("Cuenta actualizada");
            else
                Console.WriteLine("No se encontró la cuenta especificada");
        }


        public static void DeleteAccount()
        {
            Console.WriteLine("Ingrese el ID o el nombre de la cuenta a eliminar: ");
            var id = Lector.Leer();
            _expenseTracker.DeleteAccount(id);
            Console.WriteLine("Cuenta eliminada");
        }


        public static void AddTransaction()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var accountName = Lector.Leer();
            Console.WriteLine("Ingrese el ID de la cuenta: ");
            var accountId = Lector.Leer();
            Console.WriteLine("Ingrese el tipo de transacción (Ingreso/Gastos): ");
            var type = Lector.Leer();
            Console.WriteLine("Ingrese la categoría: ");
            var category = Lector.Leer();
            Console.WriteLine("Ingrese el monto: ");
            var amount = double.Parse(Lector.Leer());
            Console.WriteLine("Ingrese la descripción: ");
            var description = Lector.Leer();
            Console.WriteLine("Ingrese la fecha (dd/mm/yyyy): ");
            var date = DateTime.ParseExact(Lector.Leer(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            _expenseTracker.AddTransaction(accountName, accountId, type, category, amount, description, date);
            Console.WriteLine("Transacción agregada");
        }

        public static void CalculateCurrentBalance()
        {
            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            var accountName = Lector.Leer();
            Console.WriteLine("Ingrese el ID de la cuenta: ");
            var accountId = Lector.Leer();
            var balance = _expenseTracker.CalculateCurrentBalance(accountName, accountId);
            Console.WriteLine("Saldo actual: " + balance);
        }


        public static void Loading(Task<double> tarea)
        {
            string loadingString = "Cargando Tasas...";
            int i = 0;
            while (!tarea.IsCompleted)
            {
                if (i < loadingString.Length)
                    Console.Write(loadingString[i]);
                else
                    Console.Write('.');

                i++;
                Task.Delay(30).Wait();
            }
            Console.Write("\n");
        }

        public static async void ConvertCurrency()
        {

            Console.WriteLine("Ingrese el monto a convertir: ");
            var amount = double.Parse(Lector.Leer());
            Console.WriteLine("Ingrese la moneda de origen (USD): ");
            var fromCurrency = Lector.Leer();
            Console.WriteLine("Ingrese la moneda de destino (DOP): ");
            var toCurrency = Lector.Leer();
            var resultTask =  _expenseTracker.ConvertCurrencyAsync(amount, fromCurrency, toCurrency);
            Loading(resultTask);
            var result = await resultTask;
            Console.WriteLine("Monto convertido: " + result);


        }
        public static void ShowExpenseSummaryByAccount()
        {
            var summary = _expenseTracker.GenerateExpenseSummaryByAccount();
            foreach (var item in summary)
            {
                Console.WriteLine("Cuenta: " + item.Key + ", Total gastado: $" + item.Value);
            }
        }


    }
}
