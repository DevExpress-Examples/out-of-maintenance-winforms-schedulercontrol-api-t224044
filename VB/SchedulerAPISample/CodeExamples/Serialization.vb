Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Xml
Imports System

Namespace SchedulerAPISample.CodeExamples
    Friend Class SerializationExamples
        Private Shared Sub AppointmentSerialization(ByVal scheduler As SchedulerControl)
'            #Region "#AppointmentSerialization"
            SchedulerCompatibility.Base64XmlObjectSerialization = True
            For Each apt As Appointment In scheduler.ActiveView.GetAppointments()
                Dim helper As New AppointmentXmlPersistenceHelper(apt, Nothing)
                apt.Description = helper.ToXml()
            Next apt
'            #End Region ' #AppointmentSerialization
        End Sub

        Private Shared Sub AppointmentDeserialization(ByVal scheduler As SchedulerControl)
'            #Region "#AppointmentDeserialization"
            scheduler.Storage.Appointments.Clear()
            Dim xmlAppointment As String = "<Appointment Version=""1"" Start=""01/13/2016 04:22:00"" " & ControlChars.CrLf & _
"End=""01/13/2016 05:53:00"" Label=""5"" ResourceId=""3"" Status=""2"" Subject=""Meet Andrew Miller"" />"
            Dim objAppointment As Appointment = AppointmentXmlPersistenceHelper.ObjectFromXml(scheduler.Storage.Appointments, xmlAppointment)
            scheduler.Storage.Appointments.Add(objAppointment)
            scheduler.Start = New Date(2016, 1, 13)
'            #End Region ' #AppointmentDeserialization
        End Sub
    End Class
End Namespace
