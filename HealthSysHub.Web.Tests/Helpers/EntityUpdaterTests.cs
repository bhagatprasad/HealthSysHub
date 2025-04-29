using HealthSysHub.Web.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSysHub.Web.Tests.Helpers
{
    [TestFixture]
    public class EntityUpdaterTests
    {
        public class TestEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool IsActive { get; set; }
        }

        #region HasChanges Tests

        [Test]
        public void HasChanges_ReturnsFalse_WhenNoPropertiesChanged()
        {
            // Arrange
            var existingEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Value = 10,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            var incomingEntity = new TestEntity
            {
                Id = existingEntity.Id,
                Name = "Test",
                Value = 10,
                CreatedDate = existingEntity.CreatedDate,
                IsActive = true
            };

            // Act
            var result = EntityUpdater.HasChanges(existingEntity, incomingEntity);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasChanges_ReturnsTrue_WhenAnyPropertyChanged()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = "Old Name", Value = 10 };
            var incomingEntity = new TestEntity { Name = "New Name", Value = 10 };

            // Act
            var result = EntityUpdater.HasChanges(existingEntity, incomingEntity);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasChanges_ReturnsFalse_WhenOnlyExcludedPropertiesChanged()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = "Test", CreatedDate = DateTime.Now };
            var incomingEntity = new TestEntity { Name = "Test", CreatedDate = DateTime.Now.AddDays(1) };

            // Act
            var result = EntityUpdater.HasChanges(existingEntity, incomingEntity, "CreatedDate");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void HasChanges_ReturnsTrue_WhenNonExcludedPropertiesChanged()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = "Old", Value = 1 };
            var incomingEntity = new TestEntity { Name = "New", Value = 1 };

            // Act
            var result = EntityUpdater.HasChanges(existingEntity, incomingEntity, "Value");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasChanges_HandlesNullValues_Properly()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = null };
            var incomingEntity = new TestEntity { Name = "Test" };

            // Act
            var result = EntityUpdater.HasChanges(existingEntity, incomingEntity);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasChanges_ReturnsFalse_WhenBothValuesAreNull()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = null };
            var incomingEntity = new TestEntity { Name = null };

            // Act
            var result = EntityUpdater.HasChanges(existingEntity, incomingEntity);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region UpdateProperties Tests

        [Test]
        public void UpdateProperties_UpdatesAllProperties_WhenNoExclusions()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = "Old", Value = 1 };
            var incomingEntity = new TestEntity { Name = "New", Value = 2 };

            // Act
            EntityUpdater.UpdateProperties(existingEntity, incomingEntity);

            // Assert
            Assert.AreEqual("New", existingEntity.Name);
            Assert.AreEqual(2, existingEntity.Value);
        }

        [Test]
        public void UpdateProperties_DoesNotUpdateExcludedProperties()
        {
            // Arrange
            var originalDate = DateTime.Now;
            var existingEntity = new TestEntity { Name = "Old", CreatedDate = originalDate };
            var incomingEntity = new TestEntity { Name = "New", CreatedDate = DateTime.Now.AddDays(1) };

            // Act
            EntityUpdater.UpdateProperties(existingEntity, incomingEntity, "CreatedDate");

            // Assert
            Assert.AreEqual("New", existingEntity.Name);
            Assert.AreEqual(originalDate, existingEntity.CreatedDate);
        }

        [Test]
        public void UpdateProperties_HandlesNullValues_Properly()
        {
            // Arrange
            var existingEntity = new TestEntity { Name = "Old" };
            var incomingEntity = new TestEntity { Name = null };

            // Act
            EntityUpdater.UpdateProperties(existingEntity, incomingEntity);

            // Assert
            Assert.IsNull(existingEntity.Name);
        }

        [Test]
        public void UpdateProperties_UpdatesBooleanProperties_Correctly()
        {
            // Arrange
            var existingEntity = new TestEntity { IsActive = true };
            var incomingEntity = new TestEntity { IsActive = false };

            // Act
            EntityUpdater.UpdateProperties(existingEntity, incomingEntity);

            // Assert
            Assert.IsFalse(existingEntity.IsActive);
        }

        [Test]
        public void UpdateProperties_UpdatesGuidProperties_Correctly()
        {
            // Arrange
            var newId = Guid.NewGuid();
            var existingEntity = new TestEntity { Id = Guid.Empty };
            var incomingEntity = new TestEntity { Id = newId };

            // Act
            EntityUpdater.UpdateProperties(existingEntity, incomingEntity);

            // Assert
            Assert.AreEqual(newId, existingEntity.Id);
        }

        #endregion
    }
}
