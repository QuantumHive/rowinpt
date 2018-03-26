using System;
using System.Collections.Generic;

namespace RowinPt.Domain
{
    public class CustomerModel : UserModel
    {
        public string Number { get; set; }
        public int? Length { get; set; }
        public DateTime? BirthDate { get; set; }

        public IEnumerable<AgendaModel> Agenda { get; set; }
        public IEnumerable<SubscriptionModel> Subscriptions { get; set; }
        public IEnumerable<MeasurementModel> Measurements { get; set; }
        public AbsenceNotesModel AbsenceNotes { get; set; }
    }
}
