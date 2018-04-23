Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample.CodeExamples
    Friend Class TimeCellExamples

        Private Shared Sub TimeCellTypes(ByVal scheduler As SchedulerControl)
            '			#Region "#TimeCellTypes"
            AddHandler scheduler.CustomDrawTimeCell, AddressOf scheduler_CustomDrawTimeCell
            AddHandler scheduler.CustomDrawDayViewAllDayArea, AddressOf Scheduler_CustomDrawDayViewAllDayArea
            scheduler.TimelineView.GetBaseTimeScale().Width = 150
            scheduler.WeekView.Enabled = True
            scheduler.DayView.DayCount = 3
            scheduler.ActiveViewType = SchedulerViewType.Day
            scheduler.GroupType = SchedulerGroupType.Date
            scheduler.DateNavigationBar.Visible = False
            scheduler.LookAndFeel.UseDefaultLookAndFeel = False
            scheduler.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
            scheduler.LookAndFeel.SkinName = "DevExpress Style"
            '			#End Region ' #TimeCellTypes
        End Sub

#Region "#@TimeCellTypes"
        Public Shared Sub scheduler_CustomDrawTimeCell(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            e.DrawDefault()
            DrawCellInfo(e)
            e.Handled = True
        End Sub
        Private Shared Sub Scheduler_CustomDrawDayViewAllDayArea(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            e.DrawDefault()
            DrawCellInfo(e)
            e.Handled = True
        End Sub
        Private Shared Sub DrawCellInfo(ByVal e As CustomDrawObjectEventArgs)
            Dim s As String = e.ObjectInfo.GetType().ToString().Substring("DevExpress.XtraScheduler.Drawing.".Length)
            Dim cell As SchedulerViewCellBase = TryCast(e.ObjectInfo, SchedulerViewCellBase)
            If cell IsNot Nothing Then
                Dim sf As New StringFormat()
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Center
                e.Cache.DrawString(s, New Font("Tahoma", 10), SystemBrushes.GrayText, cell.Bounds, sf)
            End If
        End Sub
#End Region ' #@TimeCellTypes

        Private Shared Sub HorizontalSingleWeekCellType(ByVal scheduler As SchedulerControl)
            '			#Region "#HorizontalSingleWeekCellType"
            AddHandler scheduler.CustomDrawTimeCell, AddressOf scheduler_CustomDrawTimeCell_01
            scheduler.ActiveViewType = SchedulerViewType.Month
            scheduler.GroupType = SchedulerGroupType.Date
            scheduler.DateNavigationBar.Visible = False
            scheduler.LookAndFeel.UseDefaultLookAndFeel = False
            scheduler.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
            scheduler.LookAndFeel.SkinName = "DevExpress Style"
            '			#End Region ' #HorizontalSingleWeekCellUse
        End Sub

#Region "#@HorizontalSingleWeekCellType"
        Public Shared Sub scheduler_CustomDrawTimeCell_01(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            e.DrawDefault()
            Dim cell As HorizontalSingleWeekCell = TryCast(e.ObjectInfo, HorizontalSingleWeekCell)
            If cell IsNot Nothing Then
                Dim sf As New StringFormat()
                sf.LineAlignment = StringAlignment.Center
                e.Cache.DrawRectangle(SystemPens.ActiveBorder, cell.Bounds)
                If cell.FirstVisible Then
                    e.Cache.DrawString("First Visible Cell", New Font("Tahoma", 8), Brushes.Blue, cell.Bounds, sf)
                End If
            End If
            e.Handled = True
        End Sub
#End Region ' #@HorizontalSingleWeekCellType
    End Class
End Namespace
