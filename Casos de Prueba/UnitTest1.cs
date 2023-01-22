using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Casos_de_Prueba
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConvertCurrency_ConvertirMontoDeUSDaDOP_RetornaMontoConvertido()
        {
            // Arrange
            double monto = 100;
            string monedaOrigen = "USD";
            string monedaDestino = "DOP";
            var buscadorTasas = new BuscadorTasasStub();
            var expenseTracker = new ExpenseTracker(buscadorTasas);

            // Act
            var resultado = expenseTracker.ConvertCurrency(monto, monedaOrigen, monedaDestino);

            // Assert
            Assert.AreEqual(resultado, 8000);
        }

        [TestMethod]
        public void CreateCategory_ShouldAddNewCategoryToList()
        {
            // Arrange
            var category = new Category("Alimentos", "Gastos en comida");
            var sut = new ExpenseTracker();

            // Act
            sut.CreateCategory(category);
            var result = sut.GetCategories();

            // Assert
            Assert.IsTrue(result.Any(c => c.Name.Equals(category.Name) && c.Description.Equals(category.Description)));
        }


        [TestMethod]
        public void ReadCategories_ShouldReturnAllCategoriesInList()
        {
            // Arrange
            var category1 = new Category("Alimentos", "Gastos en comida");
            var category2 = new Category("Transporte", "Gastos en transporte");
            var sut = new ExpenseTracker();
            sut.CreateCategory(category1);
            sut.CreateCategory(category2);

            // Act
            var result = sut.GetCategories();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(c => c.Name == category1.Name && c.Description == category1.Description));
            Assert.IsTrue(result.Any(c => c.Name == category2.Name && c.Description == category2.Description));
        }

        [TestMethod]
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
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Any(c => c.Name == updatedCategory.Name && c.Description == updatedCategory.Description));
        }


        [TestMethod]
        public void DeleteCategory_ShouldRemoveCategoryFromList()
        {
            // Arrange
            var category1 = new Category("Alimentos", "Gastos en comida");
            var category2 = new Category("Transporte", "Gastos en transporte");
            var sut = new ExpenseTracker();
            sut.CreateCategory(category1);
            sut.CreateCategory(category2);

            // Act
            sut.DeleteCategory(category1.Name);
            var result = sut.GetCategories();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Any(c => c.Name == category1.Name && c.Description == category1.Description));
        }

    }

    internal class Category
    {
        private string v1;
        private string v2;

        public Category(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class CategoryService
    {
        public CategoryService()
        {
        }

        internal void CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        internal void DeleteCategory(object id)
        {
            throw new NotImplementedException();
        }

        internal object ReadCategories()
        {
            throw new NotImplementedException();
        }

        internal void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
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
        internal object ConvertCurrency(double monto, string monedaOrigen, string monedaDestino)
        {
            throw new NotImplementedException();
        }

        // ...
        internal void CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        internal void DeleteCategory(string name)
        {
            throw new NotImplementedException();
        }

        internal object GetCategories()
        {
            throw new NotImplementedException();
        }

        internal void UpdateCategory(string name, Category updatedCategory)
        {
            throw new NotImplementedException();
        }
    }
}