using System;
using Utility;

namespace CommonLibrary.BaseClasses
{
    public abstract class BaseService<T> where T : BaseDatabaseContext
    {
        protected IServiceProvider serviceProvider;
        protected T db;
        public BaseService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.db = this.serviceProvider.GetService<T>();
        }
    }
}