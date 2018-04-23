Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraScheduler.Native
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Linq

Namespace SchedulerAPISample.CodeExamples
	Friend Class AppointmentConflicts
		Private Shared Sub ConflictCalculator(ByVal scheduler As SchedulerControl)
'			#Region "#ConflictCalculator"
			AddHandler scheduler.CustomDrawAppointmentBackground, AddressOf scheduler_CustomDrawAppointmentBackground
			scheduler.ActiveView.LayoutChanged()
'			#End Region ' #ConflictCalculator
		End Sub

		#Region "#@ConflictCalculator"
		Private Shared Sub scheduler_CustomDrawAppointmentBackground(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
			Dim scheduler As SchedulerControl = TryCast(sender, SchedulerControl)
			Dim viewInfo As AppointmentViewInfo = (TryCast(e.ObjectInfo, DevExpress.XtraScheduler.Drawing.AppointmentViewInfo))
            Dim apt As Appointment = viewInfo.Appointment
            Dim allAppointments As AppointmentBaseCollection = scheduler.ActiveView.GetAppointments()
			Dim aCalculator As New AppointmentConflictsCalculator(allAppointments)
			Dim visibleInterval As TimeInterval = scheduler.ActiveView.GetVisibleIntervals().Interval
			Dim isConflict As Boolean = aCalculator.CalculateConflicts(apt, visibleInterval).Count <> 0
			' Paint conflict appointments with a red and white hatch brush.
			If isConflict Then
				Dim rect As Rectangle = e.Bounds
                Dim brush As Brush = e.Cache.GetSolidBrush(scheduler.Storage.Appointments.Labels.GetById(apt.LabelKey).GetColor())
                e.Graphics.FillRectangle(brush, rect)
				rect.Inflate(-3, -3)
				Dim hatchBrush As New HatchBrush(HatchStyle.WideUpwardDiagonal, Color.Red, Color.White)
				e.Graphics.FillRectangle(hatchBrush, rect)
				e.Handled = True
			End If
		End Sub
		#End Region ' #@ConflictCalculator
	End Class
End Namespace
