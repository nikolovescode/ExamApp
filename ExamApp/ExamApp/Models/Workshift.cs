using System;
using System.Collections.Generic;
using System.Text;

namespace ExamApp.Models
{
    public class Workshift {
        public int Id { get; set; }
        public int IdWorkTask { get; set; }
        public string TitleWorkTask { get; set; }
        public int PlannedWorkshiftId { get; set; }
        public string CalendarUserEmail { get; set; }
        public bool WasEffective { get; set; }
        public int MinutesWorking { get; set; }
    }
}
