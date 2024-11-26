using BusinessLogic.Concrete;
using Dal.Interface;
using DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestFixture]
    public class SupplierManagerTests
    {
        private SupplierManager _supplierManager;
        private Mock<ISupplierDal> _mockSupplierDal;

        [SetUp]
        public void Setup()
        {
            _mockSupplierDal = new Mock<ISupplierDal>();
            _supplierManager = new SupplierManager(_mockSupplierDal.Object);
        }

        [Test]
        public void GetAllSuppliers_ShouldReturnAllSuppliers()
        {
            // Arrange
            var expectedSuppliers = new List<Supplier>
            {
                new Supplier { SupplierId = 1, Name = "Supplier 1", IsBlocked = false },
                new Supplier { SupplierId = 2, Name = "Supplier 2", IsBlocked = true }
            };
            _mockSupplierDal.Setup(dal => dal.GetAll()).Returns(expectedSuppliers);

            // Act
            var actualSuppliers = _supplierManager.GetAllSuppliers();

            // Assert
            Assert.AreEqual(expectedSuppliers.Count, actualSuppliers.Count, "GetAllSuppliers should return the correct number of suppliers.");
            Assert.AreEqual(expectedSuppliers[0].Name, actualSuppliers[0].Name, "GetAllSuppliers should return the correct suppliers.");
        }

        [Test]
        public void AddSupplier_ShouldCallDalAddMethod()
        {
            // Arrange
            var newSupplier = new Supplier { Name = "New Supplier", IsBlocked = false };

            // Act
            _supplierManager.AddSupplier(newSupplier);

            // Assert
            _mockSupplierDal.Verify(dal => dal.Add(newSupplier), Times.Once, "AddSupplier should call the Dal's Add method exactly once.");
        }

        [Test]
        public void GetBlockedSuppliers_ShouldReturnOnlyBlockedSuppliers()
        {
            // Arrange
            var allSuppliers = new List<Supplier>
            {
                new Supplier { SupplierId = 1, Name = "Supplier 1", IsBlocked = false },
                new Supplier { SupplierId = 2, Name = "Supplier 2", IsBlocked = true }
            };
            _mockSupplierDal.Setup(dal => dal.GetBlockedSuppliers()).Returns(allSuppliers.FindAll(s => s.IsBlocked));

            // Act
            var blockedSuppliers = _supplierManager.GetBlockedSuppliers();

            // Assert
            Assert.AreEqual(1, blockedSuppliers.Count, "GetBlockedSuppliers should return the correct number of blocked suppliers.");
            Assert.IsTrue(blockedSuppliers.All(s => s.IsBlocked), "GetBlockedSuppliers should return only blocked suppliers.");
        }

        [Test]
        public void DeleteSupplier_ShouldCallDalDeleteMethod()
        {
            // Arrange
            var supplierId = 1;

            // Act
            _supplierManager.DeleteSupplier(supplierId);

            // Assert
            _mockSupplierDal.Verify(dal => dal.Delete(supplierId), Times.Once, "DeleteSupplier should call the Dal's Delete method exactly once.");
        }
    }
}
