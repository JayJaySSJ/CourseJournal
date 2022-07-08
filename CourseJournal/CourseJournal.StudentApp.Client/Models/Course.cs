using System;
using System.Collections.Generic;

namespace CourseJournal.StudentApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int TrainerId { get; set; }
        public List<Student> Students { get; set; }
        public int PresenceThreshold { get; set; }
        public int HwResultsThreshold { get; set; }
        public int WtResultsThreshold { get; set; }
    }
}