Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System
Imports System.Drawing
Imports System.Linq

Namespace SchedulerAPISample.CodeExamples
    Friend Class CustomDraw

        Private Shared Sub CustomDrawAppointmentEvent(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawAppointmentEvent"
            AddHandler scheduler.CustomDrawAppointment, AddressOf scheduler_CustomDrawAppointment
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawAppointmentEvent
        End Sub

        #Region "#@CustomDrawAppointmentEvent"
        Public Shared Sub scheduler_CustomDrawAppointment(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            If TypeOf DirectCast(sender, SchedulerControl).ActiveView Is DayView Then
                Dim viewInfo As AppointmentViewInfo = TryCast(e.ObjectInfo, AppointmentViewInfo)
                ' Get DevExpress images.
                Dim im As Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/add_16x16.png")

                Dim imageBounds As New Rectangle(viewInfo.InnerBounds.X, viewInfo.InnerBounds.Y, im.Width, im.Height)
                Dim mainContentBounds As New Rectangle(viewInfo.InnerBounds.X, viewInfo.InnerBounds.Y + im.Width + 1, viewInfo.InnerBounds.Width, viewInfo.InnerBounds.Height - im.Height - 1)
                ' Draw image in the appointment.
                e.Cache.Graphics.DrawImage(im, imageBounds)

                Dim statusDelta As Integer = 0
                For i As Integer = 0 To viewInfo.StatusItems.Count - 1
                    Dim statusItem As AppointmentViewInfoStatusItem = TryCast(viewInfo.StatusItems(i), AppointmentViewInfoStatusItem)
                    ' Fill the status bar.
                    e.Cache.FillRectangle(statusItem.BackgroundViewInfo.Brush, statusItem.BackgroundViewInfo.Bounds)
                    e.Cache.FillRectangle(statusItem.ForegroundViewInfo.Brush, statusItem.ForegroundViewInfo.Bounds)
                    ' Draw the satus bar rectangle.
                    e.Cache.DrawRectangle(New Pen(statusItem.ForegroundViewInfo.BorderColor), statusItem.BackgroundViewInfo.Bounds)
                    e.Cache.DrawRectangle(New Pen(statusItem.ForegroundViewInfo.BorderColor), statusItem.ForegroundViewInfo.Bounds)
                    statusDelta = Math.Max(statusDelta, statusItem.Bounds.Width)
                Next i
                ' Draw the appointment caption text.
                e.Cache.DrawString(viewInfo.DisplayText.Trim(), viewInfo.Appearance.Font, viewInfo.Appearance.GetForeBrush(e.Cache), mainContentBounds, StringFormat.GenericDefault)
                Dim subjSize As SizeF = e.Graphics.MeasureString(viewInfo.DisplayText.Trim(), viewInfo.Appearance.Font, mainContentBounds.Width)
                Dim lineYposition As Integer = CInt(subjSize.Height)

                Dim descriptionLocation As New Rectangle(mainContentBounds.X, mainContentBounds.Y + lineYposition, mainContentBounds.Width, mainContentBounds.Height - lineYposition)
                If viewInfo.Appointment.Description.Trim() <> "" Then
                    ' Draw the line above the appointment description.
                    e.Cache.Graphics.DrawLine(viewInfo.Appearance.GetForePen(e.Cache), descriptionLocation.Location, New Point(descriptionLocation.X + descriptionLocation.Width, descriptionLocation.Y))
                    ' Draw the appointment description text.
                    e.Cache.DrawString(viewInfo.Appointment.Description.Trim(), viewInfo.Appearance.Font, viewInfo.Appearance.GetForeBrush(e.Cache), descriptionLocation, StringFormat.GenericTypographic)
                End If
                e.Handled = True
            End If
        End Sub
        #End Region ' #@CustomDrawAppointmentEvent

        Private Shared Sub CustomDrawAppointmentBackgroundEvent(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawAppointmentBackgroundEvent"
            AddHandler scheduler.CustomDrawAppointmentBackground, AddressOf scheduler_CustomDrawAppointmentBackground
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawAppointmentBackgroundEvent
        End Sub

        #Region "#@CustomDrawAppointmentBackgroundEvent"
        Public Shared Sub scheduler_CustomDrawAppointmentBackground(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim viewInfo As AppointmentViewInfo = TryCast(e.ObjectInfo, AppointmentViewInfo)
            ' Specify the ratio of a completed task to the entire task.
            Dim completenessRatio As Double = 0.25 * (CInt((viewInfo.Appointment.ResourceId)) Mod 4)
            ' Draw an appointment as usual.
            e.DrawDefault()
            ' Draw a background rectangle.
            Dim bounds As Rectangle = CalculateEntireAppointmentBounds(viewInfo)
            DrawBackGroundCore(e.Cache, bounds, completenessRatio)
            ' Indicate that no default drawing is required.
            e.Handled = True
        End Sub
        Private Shared Function CalculateEntireAppointmentBounds(ByVal viewInfo As AppointmentViewInfo) As Rectangle
            Dim leftOffset As Integer = 0
            Dim rightOffset As Integer = 0
            Dim scale As Double = viewInfo.Bounds.Width / viewInfo.Interval.Duration.TotalMilliseconds
            If Not viewInfo.HasLeftBorder Then
                Dim hidden As Double = (viewInfo.Interval.Start - viewInfo.AppointmentInterval.Start).TotalMilliseconds
                leftOffset = CInt((hidden * scale))
            End If
            If Not viewInfo.HasRightBorder Then
                Dim hidden As Double = (viewInfo.AppointmentInterval.End - viewInfo.Interval.End).TotalMilliseconds
                rightOffset = CInt((hidden * scale))
            End If
            Dim bounds As Rectangle = viewInfo.InnerBounds
            Return Rectangle.FromLTRB(bounds.Left - leftOffset, bounds.Y, bounds.Right + rightOffset, bounds.Bottom)
        End Function
        Private Shared Sub DrawBackGroundCore(ByVal cache As DevExpress.Utils.Drawing.GraphicsCache, ByVal bounds As Rectangle, ByVal completenessRatio As Double)
            Dim brush1 As Brush = New SolidBrush(Color.LightGray)
            cache.FillRectangle(brush1, New Rectangle(bounds.X, bounds.Y, CInt((bounds.Width * completenessRatio)), bounds.Height))
        End Sub
        #End Region ' #@CustomDrawAppointmentBackgroundEvent

        Private Shared Sub CustomDrawDayHeaderEvent(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawDayHeaderEvent"
            AddHandler scheduler.CustomDrawDayHeader, AddressOf scheduler_CustomDrawDayHeader
            scheduler.ActiveViewType = SchedulerViewType.FullWeek
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawDayHeaderEvent
        End Sub
        #Region "#@CustomDrawDayHeaderEvent"
        Public Shared Sub scheduler_CustomDrawDayHeader(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim header As DayHeader = TryCast(e.ObjectInfo, DayHeader)
            ' Draw the outer rectangle.
            e.Cache.FillRectangle(New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.LightBlue, Color.Blue, System.Drawing.Drawing2D.LinearGradientMode.Vertical), e.Bounds)
            Dim innerRect As Rectangle = Rectangle.Inflate(e.Bounds, -2, -2)
            ' Draw the inner rectangle.
            e.Cache.FillRectangle(New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Blue, Color.LightSkyBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical), innerRect)
            ' Draw the header caption.
            e.Cache.DrawString(header.Caption, header.Appearance.HeaderCaption.Font, New SolidBrush(Color.White), innerRect, header.Appearance.HeaderCaption.GetStringFormat())
            e.Handled = True

        End Sub
        #End Region ' #@CustomDrawDayHeaderEvent

        Private Shared Sub CustomDrawDayOfWeekHeaderEvent(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawDayOfWeekHeaderEvent"
            AddHandler scheduler.CustomDrawDayOfWeekHeader, AddressOf scheduler_CustomDrawDayOfWeekHeader
            scheduler.ActiveViewType = SchedulerViewType.Month
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawDayOfWeekHeaderEvent
        End Sub
        #Region "#@CustomDrawDayOfWeekHeaderEvent"
        Public Shared Sub scheduler_CustomDrawDayOfWeekHeader(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs)
            Dim header As DayOfWeekHeader = TryCast(e.ObjectInfo, DayOfWeekHeader)
            ' Draw the outer rectangle.
            e.Cache.FillRectangle(New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.LightGreen, Color.Green, System.Drawing.Drawing2D.LinearGradientMode.Vertical), e.Bounds)
            Dim innerRect As Rectangle = Rectangle.Inflate(e.Bounds, -2, -2)
            ' Draw the inner rectangle.
            e.Cache.FillRectangle(New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Green, Color.LightGreen, System.Drawing.Drawing2D.LinearGradientMode.Vertical), innerRect)
            ' Draw the header caption.
            e.Cache.DrawString(header.Caption, header.Appearance.HeaderCaption.Font, New SolidBrush(Color.White), innerRect, header.Appearance.HeaderCaption.GetStringFormat())
            e.Handled = True

        End Sub
        #End Region ' #@CustomDrawDayOfWeekHeaderEvent

        Private Shared Sub CustomDrawDayViewAllDayAreaEvent(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawDayViewAllDayAreaEvent"
            AddHandler scheduler.CustomDrawDayViewAllDayArea, AddressOf scheduler_CustomDrawDayViewAllDayArea
            scheduler.ActiveViewType = SchedulerViewType.Day
            scheduler.GroupType = SchedulerGroupType.Resource
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawDayViewAllDayAreaEvent
        End Sub
        #Region "#@CustomDrawDayViewAllDayAreaEvent"
        Public Shared Sub scheduler_CustomDrawDayViewAllDayArea(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim cell As AllDayAreaCell = CType(e.ObjectInfo, AllDayAreaCell)
            Dim resource As Resource = cell.Resource
            Dim interval As TimeInterval = cell.Interval
            Dim apts As AppointmentBaseCollection = DirectCast(sender, SchedulerControl).Storage.GetAppointments(interval)
            ' Specify what precentage of the appointment duration should be painted.
            Dim percent As Single = CalcCurrentWorkTimeLoad(apts, interval, resource)
            Dim brush As Brush
            ' Select the brush color.
            If percent = 0.0 Then
                brush = Brushes.LightYellow
            ElseIf percent < 0.5 Then
                brush = Brushes.LightBlue
            Else
                brush = Brushes.LightCoral
            End If
            ' Paint the area with the selected color.
            e.Cache.FillRectangle(brush, e.Bounds)
            ' Draw the percentage text. 
            Dim format As New StringFormat()
            format.LineAlignment = StringAlignment.Center
            format.Alignment = StringAlignment.Center
            e.Cache.DrawString(String.Format("{0:P}", percent), cell.Appearance.Font, Brushes.Black, e.Bounds, format)
            e.Handled = True
        End Sub
        Private Shared Function CalcCurrentWorkTimeLoad(ByVal apts As AppointmentBaseCollection, ByVal interval As TimeInterval, ByVal resource As Resource) As Single
            Dim aptsByResource As New AppointmentBaseCollection()
            Dim aptQuery = apts.Where(Function(a) a.ResourceId.Equals(resource.Id))
            aptsByResource.AddRange(aptQuery.ToList())

            Dim calc As New DevExpress.XtraScheduler.Tools.IntervalLoadRatioCalculator(interval, aptsByResource)
            Return calc.Calculate()
        End Function
        #End Region ' #@CustomDrawDayViewAllDayAreaEvent

        Private Shared Sub CustomDrawDayViewTimeRulerEvent(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawDayViewTimeRulerEvent"
            AddHandler scheduler.CustomDrawDayViewTimeRuler, AddressOf scheduler_CustomDrawDayViewTimeRuler
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawDayViewTimeRulerEvent
        End Sub

        #Region "#@CustomDrawDayViewTimeRulerEvent"
        Public Shared Sub scheduler_CustomDrawDayViewTimeRuler(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim info As TimeRulerViewInfo = TryCast(e.ObjectInfo, TimeRulerViewInfo)
            ' Clear all captions.
            For Each item In info.Items
                Dim viewInfoText As ViewInfoTextItem = TryCast(item, ViewInfoTextItem)
                If viewInfoText IsNot Nothing Then
                    viewInfoText.Text = String.Empty
                End If
            Next item
            ' Draw the TimeRuler as usual, but with empty captions.
            e.DrawDefault()
            ' Draw the image in the header.
            Dim im As Image = Image.FromFile("image.png")
            Dim imageBounds As New Rectangle(info.HeaderBounds.X, info.HeaderBounds.Y, im.Width, im.Height)
            e.Cache.Graphics.DrawImage(im, imageBounds)
            ' Cancel default painting procedure.
            e.Handled = True
        End Sub
        #End Region ' #@CustomDrawDayViewTimeRulerEvent

        Private Shared Sub CustomDrawGroupSeparator(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawGroupSeparatorEvent"
            AddHandler scheduler.CustomDrawGroupSeparator, AddressOf scheduler_CustomDrawGroupSeparator
            scheduler.GroupType = SchedulerGroupType.Resource
            scheduler.ActiveView.LayoutChanged()
'            #End Region ' #CustomDrawGroupSeparatorEvent
        End Sub

        #Region "#@CustomDrawGroupSeparatorEvent"
        Public Shared Sub scheduler_CustomDrawGroupSeparator(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            e.DrawDefault()
            Dim im As Image = Image.FromFile("image.png")
            Dim imageBounds As New Rectangle(e.Bounds.X - (im.Width \ 2), e.Bounds.Y, im.Width, im.Height)
            e.Cache.Graphics.DrawImage(im, imageBounds)
            e.Handled = True
        End Sub
        #End Region ' #@CustomDrawGroupSeparatorEvent

        Private Shared Sub CustomDrawNavigationButton(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawNavigationButtonEvent"
            AddHandler scheduler.CustomDrawNavigationButton, AddressOf scheduler_CustomDrawNavigationButton
            scheduler.Services.DateTimeNavigation.NavigateBackward()
'            #End Region ' #CustomDrawNavigationButtonEvent
        End Sub

        #Region "#@CustomDrawNavigationButtonEvent"
        Public Shared Sub scheduler_CustomDrawNavigationButton(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim navButton As NavigationButtonNext = TryCast(e.ObjectInfo, NavigationButtonNext)
            Dim scheduler As SchedulerControl = TryCast(sender, SchedulerControl)
            Dim storage As SchedulerStorage = TryCast(scheduler.Storage, SchedulerStorage)
            ' Do not count by resources.
            If scheduler.GroupType <> SchedulerGroupType.None Then
                Return
            End If

            If navButton IsNot Nothing AndAlso scheduler IsNot Nothing AndAlso storage IsNot Nothing Then
                ' Count appointments within the interval used by the Next navigation button.
                Dim apts As AppointmentBaseCollection = scheduler.Storage.Appointments.Items
                Dim aptSearchInterval As TimeSpan = scheduler.OptionsView.NavigationButtons.AppointmentSearchInterval
                Dim lastVisibleTime As Date = scheduler.ActiveView.GetVisibleIntervals().Last().End
                Dim aptCount As Integer = apts.Where(Function(a) (a.Start > lastVisibleTime) AndAlso (a.Start < lastVisibleTime.Add(aptSearchInterval))).Count()
                navButton.DisplayTextItem.Text = String.Format("Next {0} appointments", aptCount)
            End If
        End Sub
        #End Region ' #@CustomDrawNavigationButtonEvent

        Private Shared Sub CustomDrawResourceHeader(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawResourceHeaderEvent"
            ' Add information to resources.
            Dim resStorage As ResourceStorage = scheduler.Storage.Resources
            resStorage.CustomFieldMappings.Add(New ResourceCustomFieldMapping("PostCode", "mPostCode"))
            resStorage.CustomFieldMappings.Add(New ResourceCustomFieldMapping("Address", "mAddress"))
            resStorage(0).CustomFields("PostCode") = "16285 Schwedt"
            resStorage(0).CustomFields("Address") = "Landhausstraße 52 "
            ' Increase default height to fit a multi-line caption.
            scheduler.OptionsView.ResourceHeaders.Height = 50
            scheduler.GroupType = SchedulerGroupType.Resource
            AddHandler scheduler.CustomDrawResourceHeader, AddressOf scheduler_CustomDrawResourceHeader
'            #End Region ' #CustomDrawResourceHeaderEvent
        End Sub

        #Region "#@CustomDrawResourceHeaderEvent"
        Public Shared Sub scheduler_CustomDrawResourceHeader(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            Dim header As ResourceHeader = CType(e.ObjectInfo, ResourceHeader)
            ' Get the resource information from custom fields.
            Dim postcode As String = If(header.Resource.CustomFields("PostCode") IsNot Nothing, header.Resource.CustomFields("PostCode").ToString(), String.Empty)
            Dim address As String = If(header.Resource.CustomFields("Address") IsNot Nothing, header.Resource.CustomFields("Address").ToString(), String.Empty)
            ' Specify the header caption and appearance.
            header.Appearance.HeaderCaption.ForeColor = Color.Blue
            header.Caption = header.Resource.Caption & System.Environment.NewLine & address & System.Environment.NewLine & postcode
            header.Appearance.HeaderCaption.Font = e.Cache.GetFont(header.Appearance.HeaderCaption.Font, FontStyle.Bold)
            ' Draw the header using default methods.
            e.DrawDefault()
            e.Handled = True
        End Sub
        #End Region ' #@CustomDrawResourceHeaderEvent

        Private Shared Sub CustomDrawTimeCell(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawTimeCellEvent"
            AddHandler scheduler.CustomDrawTimeCell, AddressOf scheduler_CustomDrawTimeCell
            scheduler.DayView.DayCount = 3
            scheduler.ActiveViewType = SchedulerViewType.Day
            scheduler.GoToDate(New Date(Date.Today.Year, Date.Today.Month, 13))
'            #End Region ' #CustomDrawTimeCellEvent
        End Sub

        #Region "#@CustomDrawTimeCellEvent"
        Public Shared Sub scheduler_CustomDrawTimeCell(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            ' Create brushes.
            Dim brickBrush As New System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DiagonalBrick, Color.LightYellow, Color.Firebrick)
            Dim solidBrush As Brush = New SolidBrush(Color.Plum)
            ' Get the cell to draw.
            Dim cell As SelectableIntervalViewInfo = TryCast(e.ObjectInfo, SelectableIntervalViewInfo)
            ' Draw the cell.
            If cell IsNot Nothing Then
                If cell.Selected Then
                    e.Graphics.FillRectangle(solidBrush, e.Bounds)
                    e.Handled = True
                ElseIf cell.Interval.Start.Day = 13 Then
                    e.Graphics.FillRectangle(brickBrush, e.Bounds)
                    e.Handled = True
                End If
            End If
        End Sub
        #End Region ' #@CustomDrawTimeCellEvent

        Public Shared Sub CustomDrawWeekViewTopLeftCorner(ByVal scheduler As SchedulerControl)
'            #Region "#CustomDrawWeekViewTopLeftCornerEvent"
            scheduler.ActiveViewType = SchedulerViewType.Timeline
            scheduler.GroupType = SchedulerGroupType.Date
            scheduler.OptionsView.ResourceHeaders.Height = 50
            AddHandler scheduler.CustomDrawWeekViewTopLeftCorner, AddressOf scheduler_CustomDrawWeekViewTopLeftCorner
'            #End Region ' #CustomDrawWeekViewTopLeftCornerEvent
        End Sub

        #Region "#@CustomDrawWeekViewTopLeftCornerEvent"
        Public Shared Sub scheduler_CustomDrawWeekViewTopLeftCorner(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.CustomDrawObjectEventArgs)
            e.DrawDefault()
            Dim objectToDraw As UpperLeftCorner = TryCast(e.ObjectInfo, UpperLeftCorner)
            Dim text As String = "Employee"
            ' Draw the text rotated 45 degrees counterclockwise.
            Dim myFont As Font = objectToDraw.CaptionAppearance.Font
            Dim textSize As SizeF = e.Graphics.MeasureString(text, myFont)
            e.Graphics.TranslateTransform(e.Bounds.Width \ 2, e.Bounds.Height \ 2)
            e.Graphics.RotateTransform(-45F)
            e.Graphics.DrawString(text, myFont, Brushes.Blue, -(textSize.Width / 2.0F), -(textSize.Height / 2.0F))
            e.Graphics.RotateTransform(45F)
            e.Graphics.TranslateTransform(-e.Bounds.Width \ 2, -e.Bounds.Height \ 2)
            e.Handled = True
        End Sub
        #End Region ' #@CustomDrawWeekViewTopLeftCornerEvent
    End Class
End Namespace
