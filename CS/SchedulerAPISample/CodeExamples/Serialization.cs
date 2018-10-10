using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Xml;
using System;

namespace SchedulerAPISample.CodeExamples {
    class SerializationExamples {
        static void AppointmentSerialization(SchedulerControl scheduler) {
            #region #AppointmentSerialization
            SchedulerCompatibility.Base64XmlObjectSerialization = true;
            foreach (Appointment apt in scheduler.ActiveView.GetAppointments()) {
                AppointmentXmlPersistenceHelper helper = 
                    new AppointmentXmlPersistenceHelper(apt, null);
                apt.Description = helper.ToXml();
            }
            #endregion #AppointmentSerialization
        }

        static void AppointmentDeserialization(SchedulerControl scheduler) {
            #region #AppointmentDeserialization
            scheduler.DataStorage.Appointments.Clear();
            string xmlAppointment = @"<Appointment Version=""1"" Start=""01/13/2016 04:22:00"" 
End=""01/13/2016 05:53:00"" Label=""5"" ResourceId=""3"" Status=""2"" Subject=""Meet Andrew Miller"" />";
            Appointment objAppointment = AppointmentXmlPersistenceHelper.ObjectFromXml(scheduler.DataStorage.Appointments, xmlAppointment);
            scheduler.DataStorage.Appointments.Add(objAppointment);
            scheduler.Start = new DateTime(2016, 1, 13);
            #endregion #AppointmentDeserialization
        }
    }
}
