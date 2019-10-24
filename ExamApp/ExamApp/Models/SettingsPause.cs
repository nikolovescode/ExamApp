using System;
using System.Collections.Generic;
using System.Text;

namespace ExamApp.Models
{
    public class SettingsPause
    {
        public int Id { get; set; }
        public int MinPauseBeforeActivity { get; set; }
        public string CalendarUserEmail { get; set; }
    }
}
