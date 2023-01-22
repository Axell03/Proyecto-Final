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
        var expenseTracker = new ExpenseTracker();

        // Act
        var resultado = expenseTracker.ConvertCurrency(monto, monedaOrigen, monedaDestino);

        // Assert
        Assert.Equal(99999999, resultado);
    }
    [Fact]
    public void CreateCategory_ShouldAddNewCategoryToList()
    {
        // Arrange
        var category = new Category("Alimentos", "Gastos en comida");
        var sut = new ExpenseTracker();

        // Act
        sut.CreateCategory(category);
        var result = sut.GetCategories();

        // Assert
        Assert.Contains("Categorías creada", (string?)result);

        //Assert.Contains(result, c => c.Name == category.Name && c.Description == category.Description);
    }

    [Fact]
    public void ReadCategories_ShouldReturnAllCategoriesInList()
    {
        // Arrange
        var category = new Category("Alimentos", "Gastos en comida");
        var sut = new ExpenseTracker();
        sut.CreateCategory(category);


        // Act
        var result = sut.GetCategories();

        // Assert
        Assert.Equal(1,result);
        Assert.Contains("Categorías existentes", (string?)result);
       // Assert.Contains(result, c => c.Name == category.Name && c.Description == category.Description);

    }

    [Fact]
    public void UpdateCategory_ShouldUpdateExistingCategory()
    {
        // Arrange
        var category = new Category("Alimentos", "Gastos en comida");
        var sut = new ExpenseTracker();
        sut.CreateCategory(category);
        var updatedCategory = new Category("Alimentos", "Gastos en alimentación");

        // Act
        sut.UpdateCategory(category.Name, updatedCategory);
        var result = sut.GetCategories();

        // Assert
        Assert.Single((System.Collections.IEnumerable)result);
        Assert.Contains("Categoria cambiada", (string?)result);

       // Assert.Contains(result, c => c.Name == updatedCategory.Name && c.Description == updatedCategory.Description);
    }

    [Fact]
    public void DeleteCategory_ShouldRemoveCategoryFromList()
    {
        // Arrange
        var category = new Category("Alimentos", "Gastos en comida");
        var sut = new ExpenseTracker();
        sut.CreateCategory(category);

        // Act
        sut.DeleteCategory(category.Name);
        var result = sut.GetCategories();

        // Assert
        Assert.Equal(1, result);
        Assert.Contains("Categoria eliminada", (string?)result);
        //Assert.Contains(result, c => c.Name == category.Name && c.Description == category.Description);
    }
}

internal class BuscadorTasasStub
{
    public BuscadorTasasStub()
    {
    }
}

public class ExpenseTracker
{
    private object _categories;

    internal object ConvertCurrency(double monto, string monedaOrigen, string monedaDestino)
    {

        throw new NotImplementedException();
    }

    internal void CreateCategory(Category category)
    {
        Console.WriteLine("Alimentos", "Gastos en comida");
        var name = Console.ReadLine();
        var description = Console.ReadLine();
        var newCategory = new Category(name, description);
        Console.WriteLine("La categoría ha sido creada");
    }
    internal void DeleteCategory(string name)
    {
        Console.WriteLine("Alimentos", "Gastos en comida");
        name = Console.ReadLine();
        var category = _categories;
        if (category == null)
        {
            Console.WriteLine("La categoría no existe");
            return;
        }
        Console.WriteLine("Categoria eliminada");
    }

    internal object GetCategories()
    {
        throw new NotImplementedException();
    }

    internal void UpdateCategory(string name, Category updatedCategory)
    {
        Console.WriteLine("Alimentos", "Gastos en comida");
        name = Console.ReadLine();
        var category = _categories;
        if (category == null)
        {
            Console.WriteLine("La categoría no existe");
            return;
        }

        Console.WriteLine("Alimentos", "Gastos en alimentación");

        Console.WriteLine("Categoria cambiada");
    }
}