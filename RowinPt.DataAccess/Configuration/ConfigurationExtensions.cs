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
        private static IEnumerable<Type> EntityTypes
        {
            get
            {
                if (_entityTypes == null)
                {
                    _entityTypes = AssemblyLoader.LoadExportedTypes("rowinpt.domain")
                        .Where(t => typeof(IModel).IsAssignableFrom(t) && !typeof(UserModel).IsAssignableFrom(t))
                        .ToArray();
                }
                return _entityTypes;
            }
        }

        public static void ConfigureEditInfoOnModels(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in EntityTypes)
            {
                var builder = modelBuilder.Entity(entityType);
                builder.Property(nameof(IModel.CreatedBy)).IsRequired();
                builder.Property(nameof(IModel.EditedBy)).IsRequired();
            }
        }

        public static void ConfigureSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            var openGenericMethod = typeof(ConfigurationExtensions).GetMethod(
                nameof(SetSoftDeleteGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var entityType in EntityTypes)
            {
                var method = openGenericMethod.MakeGenericMethod(entityType);
                method.Invoke(null, new object[] { modelBuilder });
            }
        }

        private static void SetSoftDeleteGlobalQueryFilter<TModel>(ModelBuilder builder)
            where TModel : class, IModel => 
            builder.Entity<TModel>().HasQueryFilter(model => model.Active);

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
