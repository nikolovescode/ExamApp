using System;
using System.Collections.Generic;
using System.Text;

namespace ExamApp.Models
{
    public class WorkNote
    {
        public int Id { get; set; }
        public string TitleWorkNote { get; set; }
        public string TitleWorkTask { get; set; }
        public string CalendarUserEmail { get; set; }
    }
}
