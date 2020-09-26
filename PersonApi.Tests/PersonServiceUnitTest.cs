using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace PersonApi.Tests
{
    public class PersonServiceUnitTest : BaseUnitTest
    {
        private PersonApi.Services.IPersonService GetPersonService(DbLayer.DatabaseContext db)
        {
            var serviceProvider = GetMoqIServiceProvider(db);

            var _service = new PersonApi.Services.PersonService(serviceProvider.Object);
            return _service;
        }

        [Fact(DisplayName = "GetList_Should_Get1Item")]
        public void GetList_Should_Get1Item()
        {
            var _currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var _dbName = nameof(PersonServiceUnitTest) + "." + _currentMethodName;

            using (var db = DatabaseContextMocker.GetApiDbContext(_dbName))
            {
                //arrange                
                var _service = GetPersonService(db);

                //Act
                var _response = _service.GetList();

                //Assert
                Assert.True(_response.Count == 1);
            }
        }

        public static IEnumerable<object[]> Get_Should_Work_Parameters
        {
            get
            {
                return new[]
                {
                    new object[] { new Guid("AF34F40D-9803-4113-808D-687A46557892"), false },
                    new object[] { new Guid("A9593D3F-FDAF-4424-AF15-FF368791897B"), true}
               };
            }
        }

        [Theory(DisplayName = "Get_Should_Work")]
        [MemberData(nameof(Get_Should_Work_Parameters))]
        public void Get_Should_Work(Guid personId, bool isNull)
        {
            var _currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var _dbName = nameof(PersonServiceUnitTest) + "." + _currentMethodName;

            using (var db = DatabaseContextMocker.GetApiDbContext(_dbName))
            {
                //arrange                
                var _service = GetPersonService(db);

                //Act
                var _response = _service.Get(personId);

                //Assert

                Assert.True(isNull ?
                            (_response == null) :
                            (_response != null));
            }
        }

        [Fact(DisplayName = "AddPerson_Should_Work")]
        public void AddPerson_Should_Work()
        {
            var _currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var _dbName = nameof(PersonServiceUnitTest) + "." + _currentMethodName;

            using (var db = DatabaseContextMocker.GetApiDbContext(_dbName))
            {
                //arrange                
                var _service = GetPersonService(db);
                var _person = new DbLayer.Tables.Person()
                {
                    Name = "name",
                    Surname = "surname",
                    Company = "company"
                };

                //Act
                var _response = _service.Add(_person);

                //Assert
                Assert.True(_response.Id == _person.Id);
            }
        }

        [Fact(DisplayName = "UpdatePerson_Should_Work")]
        public void UpdatePerson_Should_Work()
        {
            var _currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var _dbName = nameof(PersonServiceUnitTest) + "." + _currentMethodName;

            using (var db = DatabaseContextMocker.GetApiDbContext(_dbName))
            {
                //arrange                
                var _service = GetPersonService(db);
                var _person = new DbLayer.Tables.Person()
                {
                    Name = "name",
                    Surname = "surname",
                    Company = "company"
                };
                db.People.Add(_person);
                db.SaveChanges();

                //Act
                var _response = _service.Update(_person);

                //Assert
                Assert.True(_response != null && _response.Id == _person.Id);
            }
        }

        [Fact(DisplayName = "DeletePerson_Should_Work")]
        public void DeletePerson_Should_Work()
        {
            var _currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var _dbName = nameof(PersonServiceUnitTest) + "." + _currentMethodName;

            using (var db = DatabaseContextMocker.GetApiDbContext(_dbName))
            {
                //arrange                
                var _service = GetPersonService(db);

                var _testGuid = db.People.First().Id;


                //Act
                _service.Delete(_testGuid);
                var _count = db.People.Count(f => f.Id == _testGuid);

                //Assert
                Assert.True(_count == 0);
            }
        }


    }
}
