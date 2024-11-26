using BusinessLogic.Concrete;
using Dal.Interface;
using DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestFixture]
    public class ProductManagerTests
    {
        private ProductManager _productManager;
        private Mock<IProductDal> _mockProductDal;

        [SetUp]
        public void Setup()
        {
            _mockProductDal = new Mock<IProductDal>();
            _productManager = new ProductManager(_mockProductDal.Object);
        }

        [Test]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", Price = 10.0m, CreatedAt = DateTime.Now, SupplierId = 1 },
                new Product { ProductId = 2, Name = "Product 2", Price = 20.0m, CreatedAt = DateTime.Now, SupplierId = 1 }
            };
            _mockProductDal.Setup(dal => dal.GetAll()).Returns(expectedProducts);

            // Act
            var actualProducts = _productManager.GetAllProducts();

            // Assert
            Assert.AreEqual(expectedProducts.Count, actualProducts.Count, "GetAllProducts should return the correct number of products.");
            Assert.AreEqual(expectedProducts[0].Name, actualProducts[0].Name, "GetAllProducts should return the correct products.");
        }

        [Test]
        public void AddProduct_ShouldCallDalAddMethod()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product", Price = 30.0m, CreatedAt = DateTime.Now, SupplierId = 2 };

            // Act
            _productManager.AddProduct(newProduct);

            // Assert
            _mockProductDal.Verify(dal => dal.Add(newProduct), Times.Once, "AddProduct should call the Dal's Add method exactly once.");
        }

        [Test]
        public void GetProductsBySupplierId_ShouldReturnCorrectProducts()
        {
            // Arrange
            var supplierId = 1;
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", Price = 10.0m, CreatedAt = DateTime.Now, SupplierId = supplierId },
                new Product { ProductId = 2, Name = "Product 2", Price = 20.0m, CreatedAt = DateTime.Now, SupplierId = supplierId }
            };
            _mockProductDal.Setup(dal => dal.GetBySupplierId(supplierId)).Returns(expectedProducts);

            // Act
            var actualProducts = _productManager.GetProductsBySupplierId(supplierId);

            // Assert
            Assert.AreEqual(expectedProducts.Count, actualProducts.Count, "GetProductsBySupplierId should return the correct number of products for the supplier.");
            Assert.AreEqual(expectedProducts[0].Name, actualProducts[0].Name, "GetProductsBySupplierId should return the correct products for the supplier.");
        }

        [Test]
        public void DeleteProduct_ShouldCallDalDeleteMethod()
        {
            // Arrange
            var productId = 1;

            // Act
            _productManager.DeleteProduct(productId);

            // Assert
            _mockProductDal.Verify(dal => dal.Delete(productId), Times.Once, "DeleteProduct should call the Dal's Delete method exactly once.");
        }

        [Test]
        public void GetAllSorted_ShouldReturnSortedProducts()
        {
            // Arrange
            var sortBy = "Price";
            var ascending = true;
            var expectedProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", Price = 10.0m, CreatedAt = DateTime.Now, SupplierId = 1 },
                new Product { ProductId = 2, Name = "Product 2", Price = 20.0m, CreatedAt = DateTime.Now, SupplierId = 1 }
            };
            _mockProductDal.Setup(dal => dal.GetAllSorted(sortBy, ascending)).Returns(expectedProducts);

            // Act
            var sortedProducts = _productManager.GetAllSorted(sortBy, ascending);

            // Assert
            Assert.AreEqual(expectedProducts.Count, sortedProducts.Count, "GetAllSorted should return the correct number of sorted products.");
            Assert.AreEqual(expectedProducts[0].Price, sortedProducts[0].Price, "GetAllSorted should return the correct products sorted by price.");
        }
    }
}
