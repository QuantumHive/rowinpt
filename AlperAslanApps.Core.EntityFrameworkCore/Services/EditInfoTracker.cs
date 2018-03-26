using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlperAslanApps.Core.EntityFrameworkCore.Services
{
    [DebuggerStepThrough]
    public class EditInfoTracker : IEditInfoHandler
    {
        private readonly DbContext _databaseContext;
        private readonly IUserContext _userContext;
        private readonly ITimeProvider _timeProvider;

        public EditInfoTracker(DbContext databaseContext,
            IUserContext userContext,
            ITimeProvider timeProvider)
        {
            _databaseContext = databaseContext;
            _userContext = userContext;
            _timeProvider = timeProvider;
        }

        public void Track()
        {
            TrackCreatedModels();
            TrackModifiedModels();
        }

        private void TrackCreatedModels()
        {
            var addedEntities = GetChangeTrackersEntries(EntityState.Added);

            foreach (var entity in addedEntities.OfType<IModel>())
            {
                entity.CreatedOn = entity.EditedOn = _timeProvider.Now;
                entity.CreatedBy = entity.EditedBy = _userContext.Id;
                entity.Active = true;

                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
            }
        }

        private void TrackModifiedModels()
        {
            var modifiedEntities = GetChangeTrackersEntries(EntityState.Modified);

            foreach (var entity in modifiedEntities.OfType<IModel>())
            {
                //_databaseContext.Entry(entity).Reference(nameof(IModel.EditInfo)).Load();
                entity.EditedOn = _timeProvider.Now;
                entity.EditedBy = _userContext.Id;
            }
        }

        private IEnumerable<object> GetChangeTrackersEntries(EntityState state)
        {
            return
                from entry in _databaseContext.ChangeTracker.Entries()
                where entry.State == state
                select entry.Entity;
        }
    }
}
