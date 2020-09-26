using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PersonApi.Tests
{
    public class CommunicationInformationServiceTest : BaseUnitTest
    {
        private PersonApi.Services.ICommunicationInformationService GetCommunicationService(DbLayer.DatabaseContext db)
        {
            var serviceProvider = GetMoqIServiceProvider(db);

            LoadPersonServiceMoq(serviceProvider);

            var _service = new PersonApi.Services.CommunicationInformationService(serviceProvider.Object);
            return _service;
        }

        private void LoadPersonServiceMoq(Mock<IServiceProvider> serviceProvider)
        {
            var mockObject = new Mock<PersonApi.Services.IPersonService>();

            var _addPrm = It.IsAny<Guid>();
            var _returnVal = new PersonApi.DbLayer.Tables.Person() { Id = _addPrm };
            mockObject.Setup(x => x.Get(_addPrm)).Returns(_returnVal);

            serviceProvider.Setup(x => x.GetService(typeof(PersonApi.Services.IPersonService))).Returns(mockObject.Object);
        }

        [Fact(DisplayName = "GetList_Should_BeEmpty")]
        public void GetList_Should_BeEmpty()
        {
            var _currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var _dbName = nameof(PersonServiceUnitTest) + "." + _currentMethodName;

            using (var db = DatabaseContextMocker.GetApiDbContext(_dbName))
            {
                //arrange                
                var _service = GetCommunicationService(db);

                //Act
                var _response = _service.GetList(Guid.NewGuid());

                //Assert
                Assert.True(_response.Count == 0);
            }
        }
    }
}
