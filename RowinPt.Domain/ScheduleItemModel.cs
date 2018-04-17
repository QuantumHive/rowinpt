using AlperAslanApps.Core;
using System;
using System.Collections.Generic;

namespace RowinPt.Domain
{
    public class ScheduleItemModel : IModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EditedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Active { get; set; }
        public Guid CompanyId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        public Guid ScheduleId { get; set; }
        public Guid CourseId { get; set; }
        public Guid? PersonalTrainerId { get; set; }

        public ScheduleModel Schedule { get; set; }
        public CourseModel Course { get; set; }
        public PersonalTrainerModel PersonalTrainer { get; set; }

        public IEnumerable<AgendaModel> Agenda { get; set; }
    }
}
