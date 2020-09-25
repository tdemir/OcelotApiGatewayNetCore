using System.Linq;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CommonLibrary.BaseClasses
{
    public abstract class BaseDatabaseContext : DbContext
    {
        protected readonly IConfiguration configuration;
        // public BaseDatabaseContext(DbContextOptions<BaseDatabaseContext> options, IConfiguration configuration) : base(options)
        // {
        //     this.configuration = configuration;
        // }
        protected BaseDatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            if (!optionsBuilder.IsConfigured)
            {
                var _assembyName = this.GetType().Assembly.FullName;
                var constr = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(constr, _assembly => _assembly.MigrationsAssembly(_assembyName));
            }
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SingularizeTableNames(modelBuilder);
            SetEFGlobalQeuryFilters(modelBuilder);
            //base.OnModelCreating(modelBuilder);
        }


        private void SingularizeTableNames(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }
        }

        private void SetEFGlobalQeuryFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isDeletedProperty = entityType.FindProperty(nameof(BaseTable.IsDeleted));
                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var _memberExpression = Expression.Property(parameter, isDeletedProperty.PropertyInfo);
                    var _matchExpression = Expression.Equal(_memberExpression, Expression.Constant(false));

                    var filter = Expression.Lambda(_matchExpression, parameter);

                    MutableEntityTypeExtensions.SetQueryFilter(entityType, filter);
                }
            }
        }

    }
}