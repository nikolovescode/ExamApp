using System;
using System.Collections.Generic;
using System.Text;

namespace ExamApp.Models
{
    public class PlannedWorkshift
    {
        public int Id { get; set; }

        public int IdWorkTask { get; set; }

        public string TitleWorkTask { get; set; }

        public string CalendarUserEmail { get; set; }

        public int MinutesToWork { get; set; }

        public int Minute { get; set; }

        public int Hour { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public bool Priority { get; set; }

        public bool Done { get; set; }
    }
}
