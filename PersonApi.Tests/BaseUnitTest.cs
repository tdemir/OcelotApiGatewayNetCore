using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonApi.Tests
{
    public abstract class BaseUnitTest
    {
        protected Mock<IServiceProvider> GetMoqIServiceProvider(DbLayer.DatabaseContext db)
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(DbLayer.DatabaseContext))).Returns(db);

            return serviceProvider;
        }
    }
}
