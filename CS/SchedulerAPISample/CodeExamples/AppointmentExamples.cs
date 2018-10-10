using DevExpress.XtraScheduler;
using System;

namespace SchedulerAPISample.CodeExamples {
    class AppointmentExamples {

        static void CreateAppointmentFromSelection(SchedulerControl scheduler) {
            #region #CreateAppointmentFromSelection
            // Delete all appointments.
            scheduler.DataStorage.Appointments.Clear();
            // Select time interval
            scheduler.ActiveView.SetSelection(new TimeInterval(DateTime.Now, new TimeSpan(2, 40, 0)), scheduler.ActiveView.GetResources()[1]);
            // Group by resource.
            scheduler.GroupType = SchedulerGroupType.Resource;
            // Create a new appointment.
            Appointment apt = scheduler.DataStorage.CreateAppointment(AppointmentType.Normal);

            // Set the appointment's time interval to the selected time interval.
            apt.Start = scheduler.SelectedInterval.Start;
            apt.End = scheduler.SelectedInterval.End;

            // Set the appointment's resource to the resource which contains
            // the currently selected time interval.
            apt.ResourceId = scheduler.SelectedResource.Id;

            // Add the new appointment to the appointment collection.
            scheduler.DataStorage.Appointments.Add(apt);
            #endregion #CreateAppointmentFromSelection
        }

        static void UseCustomFields(SchedulerControl scheduler) {
            #region #UseCustomFields
            scheduler.DataStorage.Appointments.Clear();
            // Handle the FilterAppointment event to display appointments by some criteria.
            scheduler.DataStorage.FilterAppointment += Storage_FilterAppointment;
            scheduler.DayView.TopRowTime = TimeSpan.FromHours(DateTime.Now.Hour);

            // Add mapping to create a custom field. 
            // If Scheduler is bound to data, the string "DataFieldOne" should match the field name in the data source.
            // For unbound Scheduler, the data field name is required but not used.
            scheduler.DataStorage.Appointments.CustomFieldMappings.Add(new
               AppointmentCustomFieldMapping("PropertyOne", "DataFieldOne"));
            Appointment apt1 = scheduler.DataStorage.CreateAppointment(AppointmentType.Normal,
                DateTime.Now, new TimeSpan(1, 0, 0), "Visible Appointment");
            apt1.CustomFields["PropertyOne"] = "Visible";
            scheduler.DataStorage.Appointments.Add(apt1);
            Appointment apt2 = scheduler.DataStorage.CreateAppointment(AppointmentType.Normal,
                DateTime.Now.AddHours(2), new TimeSpan(0, 30, 0), "Hidden Appointment");
            apt2.CustomFields["PropertyOne"] = "Hidden";
            scheduler.DataStorage.Appointments.Add(apt2);
            #endregion #UseCustomFields
        }

        #region #@UseCustomFields
        static void Storage_FilterAppointment(object sender, PersistentObjectCancelEventArgs e) {
            string value = ((Appointment)e.Object).CustomFields["PropertyOne"] as string;
            e.Cancel = value != null && value == "Hidden";
        }
        #endregion #@UseCustomFields

    }
}
