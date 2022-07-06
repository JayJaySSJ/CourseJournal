using System;

namespace CourseJournal.AdminApp.Client
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }

    public class DateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
    }
}