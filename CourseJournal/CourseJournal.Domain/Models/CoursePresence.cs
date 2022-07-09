using System;

namespace CourseJournal.Domain.Models
{
    public enum PresenceStatus
    {
        Present = 0,
        Absent = 1,
        Justified = 2
    };

    public class CoursePresence
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime LessonDate { get; set; }
        public PresenceStatus PresenceStatus { get; set; }
    }
}