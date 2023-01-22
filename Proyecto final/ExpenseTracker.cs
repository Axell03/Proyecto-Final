using AngleSharp;
using CurrencyConverter;
using Proyecto_final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
class ExpenseTracker
{
    private List<string> _categories = new List<string>();
    private List<Account> _accounts = new List<Account>();
    public BuscadorTasas _BuscadorTasas;

    public ExpenseTracker(BuscadorTasas BuscadorTasas)
    {
        _BuscadorTasas = BuscadorTasas;
    }
    public void CreateCategory(string name)
    {
        _categories.Add(name);
    }

    public List<string> ReadCategories()
    {
        return _categories;
    }

    public void UpdateCategory(string oldName, string newName)
    {
        int index = _categories.IndexOf(oldName);
        _categories[index] = newName;
    }

    public void DeleteCategory(string name)
    {
        _categories.Remove(name);
    }

    public void CreateAccount(string name,string id,string email)
    {
        _accounts.Add(new Account(name, id, email));
    }

    public List<Account> ReadAccounts()
    {
        return _accounts;
    }

    public void UpdateAccount(string oldName, string newName, string oldId, string newId, string oldEmail, string newEmail)
    {
        var account = _accounts.FirstOrDefault(a => a.Name == oldName && a.ID == oldId && a.Email == oldEmail);
        if (account != null)
        {
            if (newName != null)
            {
                account.Name = newName;
            }
            if (newId != null)
            {
                account.ID = newId;
            }
            if (newEmail != null)
            {
                account.Email = newEmail;
            }
        }
        else
        {
            Console.WriteLine("No se encontró la cuenta especificada");
        }
    }

    public void DeleteAccount(string name)
    {
        _accounts.RemoveAll(a => a.Name == name);
    }

    public Task<double> ConvertCurrencyAsync(double amount, string fromCurrency, string toCurrency)
    {
        return ConvertCurrencyAsync(amount, fromCurrency, toCurrency, _BuscadorTasas);
    }

    public async Task<double> ConvertCurrencyAsync(double amount, string fromCurrency, string toCurrency, BuscadorTasas _BuscadorTasas)
    {
        var tasas = _BuscadorTasas.ObtenerTasas();
        var tasa = tasas.FirstOrDefault(t => t.MonedaOrigen == fromCurrency && t.MonedaDestino == toCurrency);
        tasa = tasas.FirstOrDefault(t => t.MonedaOrigen == fromCurrency && t.MonedaDestino == toCurrency);
        if (tasa.Valor == 0)
        {
            throw new Exception("No se encontró tasa de cambio para las monedas especificadas");
        }
        return amount * tasa.Valor;
    }



    public void AddTransaction(string accountName, string accountId, string type, string category, double amount, string description, DateTime date)
    {
        var account = _accounts.FirstOrDefault(a => a.Name == accountName && a.ID == accountId);
        if (account == null)
            throw new Exception("Cuenta no encontrada");
        var transaction = new Transaction
        {
            Type = type,
            Category = category,
            Amount = amount,
            Description = description,
            Date = date
        };
        account.Transactions.Add(transaction);
    }
    public double CalculateCurrentBalance(string accountName, string accountId)
    {
        var account = _accounts.FirstOrDefault(a => a.Name == accountName && a.ID == accountId);
        if (account == null)
            throw new Exception("Cuenta no encontrada");
        double income = 0;
        double expense = 0;
        foreach (var t in account.Transactions)
        {
            if (t.Type == "Ingreso")
            {
                income += t.Amount;
            }
            else if (t.Type == "Gastos")
            {
                expense += t.Amount;
            }
        }
        return income - expense;
    }
    public void GenerateReport(string accountName)
    {
        var account = _accounts.FirstOrDefault(a => a.Name == accountName);
        if (account == null)
            throw new Exception("Cuenta no encontrada");
        double income = 0;
        double expense = 0;
        foreach (var t in account.Transactions)
        {
            if (t.Type == "Ingreso")
            {
                income += t.Amount;
            }
            else if (t.Type == "Gastos")
            {
                expense += t.Amount;
            }
        }
        Console.WriteLine("Ingresos: " + income);
        Console.WriteLine("Gastos: " + expense);
        Console.WriteLine("Saldo: " + (income - expense));
    }
    public Report GenerateExpenseReportByCategory(string category, DateTime startDate, DateTime endDate)
    {
        var expenses = new List<Transaction>();
        foreach (var account in _accounts)
        {
            expenses.AddRange(account.Transactions.Where(t => t.Category == category && t.Date >= startDate && t.Date <= endDate));
        }

        var report = new Report
        {
            Category = category,
            StartDate = startDate,
            EndDate = endDate,
            TotalExpenses = expenses.Sum(t => t.Amount)
        };

        return report;
    }

}