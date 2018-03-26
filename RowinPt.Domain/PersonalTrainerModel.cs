using System.Collections.Generic;

namespace RowinPt.Domain
{
    public class PersonalTrainerModel : UserModel
    {
        public bool Admin { get; set; }

        public IEnumerable<ScheduleItemModel> ScheduleItems { get; set; }
    }
}
