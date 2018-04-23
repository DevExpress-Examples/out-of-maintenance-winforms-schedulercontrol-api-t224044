Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraBars.Docking
Imports DevExpress.XtraScheduler

Namespace SchedulerAPISample
    Partial Public Class DisplayResultControl
        Inherits UserControl

        Private CustomResourceCollection As New BindingList(Of CustomResource)()
        Private CustomEventList As New BindingList(Of CustomAppointment)()

        Public Property SchedulerControl() As SchedulerControl

        Public Sub New()
            InitializeComponent()
            Me.SchedulerControl = Me.schedulerControl1

            InitHelper.InitResources(CustomResourceCollection)
            InitHelper.InitAppointments(CustomEventList, CustomResourceCollection)

            Dim mappingsResource As ResourceMappingInfo = Me.schedulerStorage1.Resources.Mappings
            mappingsResource.Id = "ResID"
            mappingsResource.Caption = "Name"

            Dim mappingsAppointment As AppointmentMappingInfo = Me.schedulerStorage1.Appointments.Mappings
            mappingsAppointment.Start = "StartTime"
            mappingsAppointment.End = "EndTime"
            mappingsAppointment.Subject = "Subject"
            mappingsAppointment.AllDay = "AllDay"
            mappingsAppointment.Description = "Description"
            mappingsAppointment.Label = "Label"
            mappingsAppointment.Location = "Location"
            mappingsAppointment.RecurrenceInfo = "RecurrenceInfo"
            mappingsAppointment.ReminderInfo = "ReminderInfo"
            mappingsAppointment.ResourceId = "OwnerId"
            mappingsAppointment.Status = "Status"
            mappingsAppointment.Type = "EventType"

            Me.schedulerStorage1.Resources.DataSource = CustomResourceCollection
            Me.schedulerStorage1.Appointments.DataSource = CustomEventList

            Me.schedulerControl1.Start = Date.Now
        End Sub
    End Class
End Namespace
