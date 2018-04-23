using DevExpress.XtraScheduler;
using System;

namespace SchedulerAPISample.CodeExamples {
    class TimeRuler
    {
        static void TimeMarkerAction(SchedulerControl scheduler)
        {
            #region #TimeMarker
            scheduler.DayView.TimeRulers.Add(new DevExpress.XtraScheduler.TimeRuler());

            // Display the time marker if the view contains a current date.
            scheduler.DayView.TimeMarkerVisibility = TimeMarkerVisibility.TodayView;
            // Display the time indicator in the current date's column only.
            scheduler.DayView.TimeIndicatorDisplayOptions.Visibility = TimeIndicatorVisibility.CurrentDate;
            // Show the time indicator on top when it overlaps an appointment.
            scheduler.DayView.TimeIndicatorDisplayOptions.ShowOverAppointment = true;
            // Hide the time marker in the second time ruler.
            scheduler.DayView.TimeRulers[1].TimeMarkerVisibility = TimeMarkerVisibility.Never;

            scheduler.ActiveViewType = SchedulerViewType.Day;
            scheduler.DayView.DayCount = 3;
            scheduler.DayView.TopRowTime = DateTime.Now.AddHours(-1).TimeOfDay;
            scheduler.Start = DateTime.Today.AddDays(-1);
            #endregion #TimeMarker
        }
    }
}
