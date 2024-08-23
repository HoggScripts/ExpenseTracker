namespace BudgetApp
{
    public class Calendar
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public DurationScale _currentScale { get; set; }

        public string Format { get; set; }

        public Calendar()
        {
            Start = new DateTime(DateTime.Now.Year, 1, 1);
            End = Start.AddYears(1).AddSeconds(-1);
            _currentScale = DurationScale.Yearly;
            Format = "yyyy";
        }
        
        public Calendar(DateTime customStart, string customFormat)
        {
            this.Start = customStart;
            this.Format = customFormat;
        }

        public enum DurationScale
        {
            Yearly,
            Monthly,
            Daily
        }

        
        public void NavigateCalendar(ConsoleKey keyPressed, string monthlyFormat, string yearlyFormat, string dailyFormat)
        {
            switch (keyPressed)
            {
                case ConsoleKey.RightArrow:
                    MoveToNextDate();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveToPreviousDate();
                    break;
                case ConsoleKey.UpArrow:
                    IncreaseTimeScale(monthlyFormat, yearlyFormat, dailyFormat);
                    break;
                case ConsoleKey.DownArrow:
                    DecreaseTimeScale(monthlyFormat, yearlyFormat, dailyFormat);
                    break;
            }
        }
        
        private void MoveToNextDate()
        {
            switch (_currentScale)
            {
                case DurationScale.Yearly:
                    Start = Start.AddYears(1);
                    End = Start.AddYears(1).AddSeconds(-1);
                    break;
                case DurationScale.Monthly:
                    Start = Start.AddMonths(1);
                    End = Start.AddMonths(1).AddSeconds(-1);
                    break;
                case DurationScale.Daily:
                    Start = Start.AddDays(1);
                    End = Start.AddDays(1).AddSeconds(-1);
                    break;
            }
        }

        private void MoveToPreviousDate()
        {
            switch (_currentScale)
            {
                case DurationScale.Yearly:
                    Start = Start.AddYears(-1);
                    End = Start.AddYears(1).AddSeconds(-1);
                    break;
                case DurationScale.Monthly:
                    Start = Start.AddMonths(-1);
                    End = Start.AddMonths(1).AddSeconds(-1);
                    break;
                case DurationScale.Daily:
                    Start = Start.AddDays(-1);
                    End = Start.AddDays(1).AddSeconds(-1);
                    break;
            }
        }

        private void IncreaseTimeScale(string monthlyFormat, string yearlyFormat, string dailyFormat)
        {
            if (_currentScale == DurationScale.Yearly)
            {
                _currentScale = DurationScale.Monthly;
                Start = new DateTime(End.Year, End.Month, 1);
                End = Start.AddMonths(1).AddSeconds(-1);
                Format = monthlyFormat;
            }
            else if (_currentScale == DurationScale.Monthly)
            {
                _currentScale = DurationScale.Daily;
                End = Start.AddDays(1).AddSeconds(-1);
                Format = dailyFormat;
            }
            else if (_currentScale == DurationScale.Daily)
            {
                _currentScale = DurationScale.Yearly;
                Start = new DateTime(End.Year, 1, 1);
                End = Start.AddYears(1).AddSeconds(-1);
                Format = yearlyFormat;
            }
        }
        
        private void DecreaseTimeScale(string monthlyFormat, string yearlyFormat, string dailyFormat)
        {
            if (_currentScale == DurationScale.Yearly)
            {
                _currentScale = DurationScale.Daily;
                End = Start.AddDays(1).AddSeconds(-1);
                Format = dailyFormat;
            }
            else if (_currentScale == DurationScale.Daily)
            {
                _currentScale = DurationScale.Monthly;
                Start = new DateTime(End.Year, End.Month, 1);
                End = Start.AddMonths(1).AddSeconds(-1);
                Format = monthlyFormat;
            }
            else if (_currentScale == DurationScale.Monthly)
            {
                _currentScale = DurationScale.Yearly;
                Start = new DateTime(End.Year, 1, 1);
                End = Start.AddYears(1).AddSeconds(-1);
                Format = yearlyFormat;
            }
        }
    }
}
