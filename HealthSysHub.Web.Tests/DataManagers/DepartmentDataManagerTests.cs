using HealthSysHub.Web.DataManagers;
using HealthSysHub.Web.DBConfiguration;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;
using HealthSysHub.Web.DBConfiguration.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HealthSysHub.Web.Tests.DataManagers
{
    [TestFixture]
    public class DepartmentDataManagerTests
    {
        private Mock<ApplicationDBContext> _mockDbContext;
        private DepartmentDataManager _departmentDataManager;
        private Mock<DbSet<Department>> _mockDepartmentsDbSet;
        private List<Department> _departmentData;

        [SetUp]
        public void Setup()
        {
            _departmentData = new List<Department>
    {
        new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Cardiology" },
        new Department { DepartmentId = Guid.NewGuid(), DepartmentName = "Neurology" }
    };

            // Create mock DbSet
            var queryable = _departmentData.AsQueryable();
            _mockDepartmentsDbSet = new Mock<DbSet<Department>>();

            // Setup IQueryable implementation
            _mockDepartmentsDbSet.As<IQueryable<Department>>().Setup(m => m.Provider).Returns(queryable.Provider);
            _mockDepartmentsDbSet.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(queryable.Expression);
            _mockDepartmentsDbSet.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            _mockDepartmentsDbSet.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            // Setup async operations
            _mockDepartmentsDbSet.As<IAsyncEnumerable<Department>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Department>(queryable.GetEnumerator()));

            // Mock FindAsync
            _mockDepartmentsDbSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] ids) =>
                {
                    var id = (Guid)ids[0];
                    return _departmentData.FirstOrDefault(d => d.DepartmentId == id);
                });

            // Mock AddAsync
            _mockDepartmentsDbSet.Setup(x => x.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Department model, CancellationToken token) =>
                {
                    model.DepartmentId = Guid.NewGuid(); // Assign new ID for new entities
                    _departmentData.Add(model);
                    return Mock.Of<EntityEntry<Department>>();
                });

            // Create mock DbContext options
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .Options;

            // Create mock DbContext with options
            _mockDbContext = new Mock<ApplicationDBContext>(options);

            // Setup DbContext methods
            _mockDbContext.Setup(x => x.Set<Department>()).Returns(_mockDepartmentsDbSet.Object);
            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            _departmentDataManager = new DepartmentDataManager(_mockDbContext.Object);
        }

        #region GetDepartmentByIdAsync Tests
        [Test]
        public async Task GetDepartmentByIdAsync_ReturnsDepartment_WhenExists()
        {
            // Arrange
            var expectedDepartment = _departmentData.First();

            // Act
            var result = await _departmentDataManager.GetDepartmentByIdAsync(expectedDepartment.DepartmentId);

            // Assert
            Assert.AreEqual(expectedDepartment, result);
            _mockDbContext.Verify(x => x.Set<Department>(), Times.Once);
        }

        [Test]
        public async Task GetDepartmentsAsync_ReturnsAllDepartments()
        {
            // Act
            var result = await _departmentDataManager.GetDepartmentsAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
            _mockDbContext.Verify(x => x.Set<Department>(), Times.Once);
        }

        [Test]
        public async Task InsertOrUpdateDepartmentAsync_AddsNewDepartment_WhenIdIsEmpty()
        {
            // Arrange
            var newDepartment = new Department { DepartmentId = Guid.Empty, DepartmentName= "Pediatrics" };

            // Act
            var result = await _departmentDataManager.InsertOrUpdateDepartmentAsync(newDepartment);

            // Assert
            Assert.AreEqual(3, _departmentData.Count);
            _mockDepartmentsDbSet.Verify(x => x.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetDepartmentByIdAsync_ReturnsNull_WhenDepartmentDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _departmentDataManager.GetDepartmentByIdAsync(nonExistentId);

            // Assert
            Assert.IsNull(result);
        }

        #endregion

        #region GetDepartmentsAsync Tests


        [Test]
        public async Task GetDepartmentsAsync_ReturnsEmptyList_WhenNoDepartmentsExist()
        {
            // Arrange
            _departmentData.Clear();

            // Act
            var result = await _departmentDataManager.GetDepartmentsAsync();

            // Assert
            Assert.IsEmpty(result);
        }

        #endregion

        #region InsertOrUpdateDepartmentAsync Tests

       

        [Test]
        public async Task InsertOrUpdateDepartmentAsync_UpdatesExistingDepartment_WhenIdExists()
        {
            // Arrange
            var existingDepartment = _departmentData.First();
            var updatedDepartment = new Department
            {
                DepartmentId = existingDepartment.DepartmentId,
                DepartmentName = "Updated Cardiology"
            };

            // Act
            var result = await _departmentDataManager.InsertOrUpdateDepartmentAsync(updatedDepartment);

            // Assert
            Assert.AreEqual("Updated Cardiology", existingDepartment.DepartmentName);
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task InsertOrUpdateDepartmentAsync_DoesNotSaveChanges_WhenNoChangesDetected()
        {
            // Arrange
            var existingDepartment = _departmentData.First();
            var sameDepartment = new Department
            {
                DepartmentId = existingDepartment.DepartmentId,
                DepartmentName = existingDepartment.DepartmentName
            };

            // Act
            var result = await _departmentDataManager.InsertOrUpdateDepartmentAsync(sameDepartment);

            // Assert
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        #endregion

        // Helper classes for async testing
        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public ValueTask DisposeAsync()
            {
                _inner.Dispose();
                return new ValueTask();
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(_inner.MoveNext());
            }

            public T Current => _inner.Current;
        }

        internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
            {
                var expectedResultType = typeof(TResult).GetGenericArguments()[0];
                var executionResult = typeof(IQueryProvider)
                    .GetMethod(
                        name: nameof(IQueryProvider.Execute),
                        genericParameterCount: 1,
                        types: new[] { typeof(Expression) })
                    .MakeGenericMethod(expectedResultType)
                    .Invoke(this, new[] { expression });

                return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                    .MakeGenericMethod(expectedResultType)
                    .Invoke(null, new[] { executionResult });
            }
        }

        internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }
        }
    }
}