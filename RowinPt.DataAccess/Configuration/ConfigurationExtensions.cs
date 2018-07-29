using AlperAslanApps.Core;
using AlperAslanApps.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using RowinPt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RowinPt.DataAccess.Configuration
{
    public static class ConfigurationExtensions
    {
        private static IEnumerable<Type> _entityTypes;
        public static IEnumerable<Type> EntityTypes
        {
            get
            {
                if (_entityTypes == null)
                {
                    _entityTypes = AssemblyLoader.LoadExportedTypes("rowinpt.domain")
                        .Where(t => typeof(IModel).IsAssignableFrom(t))
                        .ToArray();
                }
                return _entityTypes;
            }
        }

        public static void ConfigureEditInfoOnModels(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in EntityTypes.Where(t => t != typeof(UserModel)))
            {
                var builder = modelBuilder.Entity(entityType);
                builder.Property(nameof(IEditInfo.CreatedBy)).IsRequired();
                builder.Property(nameof(IEditInfo.EditedBy)).IsRequired();
            }
        }

        public static void ConfigureQueryFilters(this ModelBuilder modelBuilder, Guid companyId)
        {
            InvokeOpenGenericMethod(nameof(SetSoftDeleteGlobalQueryFilter), new object[] { modelBuilder });

            //https://github.com/aspnet/EntityFrameworkCore/issues/10271
            //BUG: cannot pass guid as value/variable to queryfilter, wait for fix
            //Current workaround is handled in the Repository class.
            
            //InvokeOpenGenericMethod(nameof(SetCompanyIdGlobalQueryFilter), new object[] { modelBuilder, companyId });
        }

        private static void InvokeOpenGenericMethod(string methodName, object[] parameters)
        {
            var openGenericMethod = typeof(ConfigurationExtensions).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

            //cannot apply query filter on derived types CustomerModel and PersonalTrainerModel, so it is applied now on the UserModel base class
            foreach (var entityType in EntityTypes.Where(t => t != typeof(CustomerModel) && t != typeof(PersonalTrainerModel)))
            {
                var method = openGenericMethod.MakeGenericMethod(entityType);
                method.Invoke(null, parameters);
            }
        }

        private static void SetSoftDeleteGlobalQueryFilter<TModel>(ModelBuilder builder)
            where TModel : class, IModel => 
            builder.Entity<TModel>().HasQueryFilter(model => model.Active);


        private static void SetCompanyIdGlobalQueryFilter<TModel>(ModelBuilder builder, Guid companyId)
            where TModel : class, IModel =>
            builder.Entity<TModel>().HasQueryFilter(model => model.CompanyId == companyId);

        public static void ConfigureModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new AgendaConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new AbsenceNotesConfiguration());
        }

        public static void ConfigureDeleteBehaviorRestrict(this ModelBuilder modelBuilder)
        {
            var cascadingForeignKeys =
                from entityType in modelBuilder.Model.GetEntityTypes()
                from foreignKey in entityType.GetForeignKeys()
                where !foreignKey.IsOwnership
                where foreignKey.DeleteBehavior == DeleteBehavior.Cascade
                select foreignKey;

            foreach (var foreignKey in cascadingForeignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
