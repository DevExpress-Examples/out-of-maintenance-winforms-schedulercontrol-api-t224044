using DevExpress.XtraScheduler;
using System.Drawing;

namespace SchedulerAPISample.CodeExamples {
    class AppointmentAppearance
    {
        static void AppointmentViewInfoCustomizingEvent(SchedulerControl scheduler)
        {
            #region #AppointmentViewInfoCustomizingEvent
            scheduler.AppointmentViewInfoCustomizing += scheduler_AppointmentViewInfoCustomizing;
            scheduler.ActiveView.LayoutChanged();
            #endregion #AppointmentViewInfoCustomizingEvent
        }

        #region #@AppointmentViewInfoCustomizingEvent
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
        #endregion #@AppointmentViewInfoCustomizingEvent
    }
}
