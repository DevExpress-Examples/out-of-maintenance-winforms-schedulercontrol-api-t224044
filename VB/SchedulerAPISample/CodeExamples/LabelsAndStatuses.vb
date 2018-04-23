Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample.CodeExamples
	Friend Class LabelsAndStatuses
		Private Shared Sub CustomLabelsAndStatusesAction(ByVal scheduler As SchedulerControl)
'			#Region "#CustomLabelsAndStatuses"
			scheduler.Storage.Appointments.Clear()

			Dim IssueList() As String = { "Consultation", "Treatment", "X-Ray" }
			Dim IssueColorList() As Color = { Color.Ivory, Color.Pink, Color.Plum }
			Dim PaymentStatuses() As String = { "Paid", "Unpaid" }
			Dim PaymentColorStatuses() As Color = { Color.Green, Color.Red }


			Dim labelStorage As IAppointmentLabelStorage = scheduler.Storage.Appointments.Labels
			labelStorage.Clear()
			Dim count As Integer = IssueList.Length
			For i As Integer = 0 To count - 1
				Dim label As IAppointmentLabel = labelStorage.CreateNewLabel(i, IssueList(i))
				label.SetColor(IssueColorList(i))
				labelStorage.Add(label)
			Next i
			Dim statusStorage As IAppointmentStatusStorage = scheduler.Storage.Appointments.Statuses
			statusStorage.Clear()
			count = PaymentStatuses.Length
			For i As Integer = 0 To count - 1
				Dim status As IAppointmentStatus = statusStorage.CreateNewStatus(i, PaymentStatuses(i), PaymentStatuses(i))
				status.SetBrush(New SolidBrush(PaymentColorStatuses(i)))
				statusStorage.Add(status)
			Next i
'			#End Region ' #CustomLabelsAndStatuses
		End Sub
	End Class
End Namespace
