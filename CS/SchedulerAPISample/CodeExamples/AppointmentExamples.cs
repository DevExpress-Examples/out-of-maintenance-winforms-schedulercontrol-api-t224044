using DevExpress.XtraScheduler;
using System;

namespace SchedulerAPISample.CodeExamples {
    class AppointmentExamples {

        static void CreateAppointmentFromSelection(SchedulerControl scheduler) {
            #region #CreateAppointmentFromSelection
            // Delete all appointments.
            scheduler.Storage.Appointments.Clear();
            // Select time interval
            scheduler.ActiveView.SetSelection(new TimeInterval(DateTime.Now, new TimeSpan(2, 40, 0)), scheduler.ActiveView.GetResources()[1]);
            // Group by resource.
            scheduler.GroupType = SchedulerGroupType.Resource;
            // Create a new appointment.
            Appointment apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal);

            // Set the appointment's time interval to the selected time interval.
            apt.Start = scheduler.SelectedInterval.Start;
            apt.End = scheduler.SelectedInterval.End;

            // Set the appointment's resource to the resource which contains
            // the currently selected time interval.
            apt.ResourceId = scheduler.SelectedResource.Id;

            // Add the new appointment to the appointment collection.
            scheduler.Storage.Appointments.Add(apt);
            #endregion #CreateAppointmentFromSelection

        }
    }
}
