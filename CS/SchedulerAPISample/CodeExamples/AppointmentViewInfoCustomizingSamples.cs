using DevExpress.XtraScheduler;
using System.Drawing;

namespace SchedulerAPISample.CodeExamples {
    class AppointmentViewInfoCustomizingSamples
    {
        static void AppointmentAppearance(SchedulerControl scheduler)
        {
            #region #AppointmentAppearance
            scheduler.AppointmentViewInfoCustomizing += scheduler_AppointmentViewInfoCustomizing;
            scheduler.ActiveView.LayoutChanged();
            #endregion #AppointmentAppearance
        }

        #region #@AppointmentAppearance
        public static void scheduler_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e)
            {
                switch (e.ViewInfo.Status.Type)
                {
                    case AppointmentStatusType.Busy:
                        e.ViewInfo.Appearance.ForeColor = Color.Red;
                        break;
                    case AppointmentStatusType.Free:
                        e.ViewInfo.Appearance.ForeColor = Color.Blue;
                        break;
                    case AppointmentStatusType.OutOfOffice:
                        e.ViewInfo.Appearance.ForeColor = Color.Green;
                        break;
                }
            }
        #endregion #@AppointmentAppearance

    static void StatusDisplayType (SchedulerControl scheduler) {
            #region #StatusDisplayType
            Appointment apt = scheduler.ActiveView.GetAppointments()[0];
            apt.End = apt.End.AddDays(1);
            apt.Description = "Time";
            scheduler.ActiveViewType = SchedulerViewType.Day;
            scheduler.DayView.AppointmentDisplayOptions.AllDayAppointmentsStatusDisplayType = AppointmentStatusDisplayType.Bounds;
            scheduler.DayView.AppointmentDisplayOptions.ShowAllDayAppointmentStatusVertically = false;
            scheduler.AppointmentViewInfoCustomizing += scheduler_AppointmentViewInfoCustomizing_1;
            scheduler.ActiveView.LayoutChanged();
            #endregion #StatusDisplayType
        }
        #region #@StatusDisplayType
        public static void scheduler_AppointmentViewInfoCustomizing_1(object sender, AppointmentViewInfoCustomizingEventArgs e) {
            if (e.ViewInfo.Description.Contains("Time"))
                e.ViewInfo.StatusDisplayType = AppointmentStatusDisplayType.Time;
        }
        #endregion #@StatusDisplayType
    }

}
