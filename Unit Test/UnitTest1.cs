using System.Collections.Generic;
using Xunit;
using Proyecto_final;
using CurrencyConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
public class ExpenseTrackerTests
{
    [Fact]
    public void CreateCategory_ShouldAddNewCategoryToList()
    {
        //arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Alimentos";

        //act
        expenseTracker.CreateCategory(categoryName);
        var categorias = expenseTracker.ReadCategories();

        //assert
        Assert.Contains(categoryName, categorias);

    }

    [Fact]
    public void ReadCategories_ShouldReturnAllCategoriesInList()
    {
        //arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Alimentos";
        var categoryName2 = "Comidas";

        //act
        expenseTracker.CreateCategory(categoryName);
        expenseTracker.CreateCategory(categoryName2);
        var categorias = expenseTracker.ReadCategories();

        //assert
        Assert.Equal(new List<string> { categoryName, categoryName2 }, categorias);
    }

    [Fact]
    public void UpdateCategory_ShouldUpdateExistingCategory()
    {
        // Arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Alimentos";
        var categoryUpdate = "Comida";

        //act
        expenseTracker.CreateCategory(categoryName);
        expenseTracker.UpdateCategory(categoryName, categoryUpdate);
        var categorias = expenseTracker.ReadCategories();

        //assert
        Assert.DoesNotContain(categoryName, categorias);
        Assert.Contains(categoryUpdate, categorias);
    }

    [Fact]
    public void DeleteCategory_ShouldRemoveCategoryFromList()
    {
        // Arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Alimentos";

        //act
        expenseTracker.CreateCategory(categoryName);
        expenseTracker.DeleteCategory(categoryName);
        var categorias = expenseTracker.ReadCategories();

        //assert
        Assert.DoesNotContain(categoryName, categorias);
    }

    [Fact]
    public void CreateAccount_ShouldAddNewAccountToList()
    {
        //arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Manolo";
        var id = "12";
        var email = "micorreo@email.com";

        //act
        expenseTracker.CreateAccount(categoryName, id, email);
        var accounts = expenseTracker.ReadAccounts();

        //assert
        Assert.Contains(accounts, a => a.Name == "Manolo" && a.ID == "12" && a.Email == "micorreo@email.com");

    }

    [Fact]
    public void ReadAccount_ShouldReturnAllAccountInList()
    {
        //arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Manolo";
        var id = "12";
        var email = "micorreo@email.com";
        var categoryName2 = "Pedro";
        var id2 = "13";
        var email2 = "tucorreo@email.com";

        //act
        expenseTracker.CreateAccount(categoryName, id, email);
        expenseTracker.CreateAccount(categoryName2, id2, email2);
        var accounts = expenseTracker.ReadAccounts();


        //assert
        Assert.Equal(2, accounts.Count);
        Assert.Equal("Manolo", accounts[0].Name);
        Assert.Equal("Pedro", accounts[1].Name);
        Assert.Equal("12", accounts[0].ID);
        Assert.Equal("tucorreo@email.com", accounts[1].Email);
    }

    [Fact]
    public void UpdateAccount_ShouldUpdateExistingAccount()
    {
        //arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Manolo";
        var id = "12";
        var email = "micorreo@email.com";
        var categoryName2 = "Pedro";
        var id2 = "13";
        var email2 = "tucorreo@email.com";

        //act
        expenseTracker.CreateAccount(categoryName, id, email);
        var success = expenseTracker.UpdateAccount(categoryName, categoryName2, id, id2, email, email2);
        var accounts = expenseTracker.ReadAccounts();

        //assert
        Assert.True(success);
        Assert.Contains(accounts, a => a.Name == "Pedro" && a.ID == "13" && a.Email == "tucorreo@email.com");
    }

    [Fact]
    public void DeleteAccount_ShouldRemoveAccountFromList()
    {
        //arrange
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);
        var categoryName = "Manolo";
        var id = "12";
        var email = "micorreo@email.com";
        var categoryName2 = "Pedro";
        var id2 = "13";
        var email2 = "tucorreo@email.com";

        //act
        expenseTracker.CreateAccount(categoryName, id, email);
        expenseTracker.CreateAccount(categoryName2, id2, email2);
        expenseTracker.DeleteAccount(categoryName);
        var accounts = expenseTracker.ReadAccounts();

        //assert
        Assert.Equal("Pedro", accounts[0].Name);
    }
}

internal class BuscadorTasasStub : IBuscadorTasas
{
    public async Task<List<Tasa>> ObtenerTasas()
    {
        List<Tasa> tasas = new List<Tasa>();
        tasas.Add(new Tasa()
        {
            Entidad = "Banco Popular",
            MonedaOrigen = "USD",
            MonedaDestino = "DOP",
            Valor = 56.3f
        });
        tasas.Add(new Tasa()
        {
            Entidad = "Banco BHD",
            MonedaOrigen = "USD",
            MonedaDestino = "DOP",
            Valor = 53.3f
        });
        return tasas;
    }
}

public class StubLectorInput : ILectorInput
{
    public string Input { get; set; }
    public StubLectorInput(string input) => Input = input;
    public string Leer()
    {
        return Input;
    }
}
