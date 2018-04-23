using DevExpress.XtraScheduler;
using System.Drawing;

namespace SchedulerAPISample.CodeExamples {
    class AppointmentEvents
    {
        static void AppointmentChangingEvent(SchedulerControl scheduler)
        {
            #region #AppointmentChangingEvent
            scheduler.Storage.AppointmentChanging += Storage_AppointmentChanging;
            scheduler.ActiveView.LayoutChanged();
            #endregion #AppointmentChangingEvent
        }

        #region #@AppointmentChangingEvent
        private static void Storage_AppointmentChanging(object sender, PersistentObjectCancelEventArgs e) {
            object busyKey = ((SchedulerStorage)sender).Appointments.Statuses.GetByType(AppointmentStatusType.Busy).Id;
            if (busyKey != null) {
                e.Cancel = ((Appointment)e.Object).StatusKey.Equals(busyKey);
            }
        }
        #endregion #@AppointmentChangingEvent
    }
}
