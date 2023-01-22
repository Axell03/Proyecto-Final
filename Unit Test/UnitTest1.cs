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
    public void ConvertCurrency_ConvertirMontoDeUSDaDOP_RetornaMontoConvertido()
    {
        // Arrange
        double monto = 100;
        string monedaOrigen = "USD";
        string monedaDestino = "DOP";
        var buscadorTasas = new BuscadorTasasStub();
        var expenseTracker = new ExpenseTracker(buscadorTasas);

        // Act
        var resultado = expenseTracker.ConvertCurrencyAsync(monto, monedaOrigen, monedaDestino);

        // Assert
        ///Assert.Equal(99999999, resultado);
    }
    [Fact]
    public void CreateCategory_ShouldAddNewCategoryToList()
    {
        // Arrange
        var buscadorTasas = new BuscadorTasasStub();
        var sut = new ExpenseTracker(buscadorTasas);

        // Act
        sut.CreateCategory("Alimentos");
        ///var result = sut.GetCategories();

        // Assert
        Assert.Contains("Categorías creada", (string?)result);

        //////////Assert.Throws<ArgumentNullException>(() => sut.CreateCategory("Alimentos"));

        //Assert.Contains(result, c => c.Name == category.Name && c.Description == category.Description);
    }

    [Fact]
    public void ReadCategories_ShouldReturnAllCategoriesInList()
    {
        // Arrange
        var category = new Category("Alimentos", "Gastos en comida");
        var buscadorTasas = new BuscadorTasasStub();
        var sut = new ExpenseTracker(buscadorTasas);
        ///sut.CreateCategory(category);


        // Act
        ///var result = sut.GetCategories();

        // Assert
        ///Assert.Equal(1,result);
        ///Assert.Contains("Categorías existentes", (string?)result);
       // Assert.Contains(result, c => c.Name == category.Name && c.Description == category.Description);

    }

    [Fact]
    public void UpdateCategory_ShouldUpdateExistingCategory()
    {
        // Arrange
        var category = new Category("Alimentos", "Gastos en comida");
        var buscadorTasas = new BuscadorTasasStub();
        var sut = new ExpenseTracker(buscadorTasas);
        ///sut.CreateCategory(category);
        var updatedCategory = new Category("Alimentos", "Gastos en alimentación");

        // Act
        ///sut.UpdateCategory(category.Name, updatedCategory);
        ///var result = sut.GetCategories();

        // Assert
        ///Assert.Single((System.Collections.IEnumerable)result);
        ///Assert.Contains("Categoria cambiada", (string?)result);

        // Assert.Contains(result, c => c.Name == updatedCategory.Name && c.Description == updatedCategory.Description);
    }

    [Fact]
    public void DeleteCategory_ShouldRemoveCategoryFromList()
    {
        // Arrange

        var category = new Category("Alimentos", "Gastos en comida");
        var buscadorTasas = new BuscadorTasasStub();
        var sut = new ExpenseTracker(buscadorTasas);
        ///sut.CreateCategory(category);

        // Act
        sut.DeleteCategory(category.Name);
        ///var result = sut.GetCategories();

        // Assert
        ///Assert.Equal(1, result);
        ///Assert.Contains("Categoria eliminada", (string?)result);
        //Assert.Contains(result, c => c.Name == category.Name && c.Description == category.Description);
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

