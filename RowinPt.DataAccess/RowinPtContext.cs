using AlperAslanApps.Core;
using Microsoft.EntityFrameworkCore;
using RowinPt.DataAccess.Configuration;
using RowinPt.Domain;

namespace RowinPt.DataAccess
{
    public class RowinPtContext : DbContext
    {
        private readonly string _connectionString;

        public RowinPtContext(string connectionString)
        {
            connectionString.ThrowIfNull(nameof(connectionString));
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureModels();
            modelBuilder.ConfigureEditInfoOnModels();
            modelBuilder.ConfigureSoftDeleteQueryFilter();
            modelBuilder.ConfigureDeleteBehaviorRestrict();
        }

        public DbSet<CourseTypeModel> CourseTypes { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<PersonalTrainerModel> PersonalTrainers { get; set; }
        public DbSet<ScheduleModel> Schedule { get; set; }
        public DbSet<ScheduleItemModel> ScheduleItems { get; set; }
        public DbSet<AgendaModel> Agenda { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public DbSet<MeasurementModel> Measurements { get; set; }
        public DbSet<AbsenceNotesModel> AbsenceNotes { get; set; }
    }
}
