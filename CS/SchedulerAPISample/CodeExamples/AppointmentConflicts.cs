using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Native;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace SchedulerAPISample.CodeExamples
{
    class AppointmentConflicts
    {
        static void AllowAppointmentConflictsEvent(SchedulerControl scheduler) {
            #region #AllowAppointmentConflictsEvent
            // Concurrent appointments with the same resource are not allowed.
            scheduler.OptionsCustomization.AllowAppointmentConflicts = AppointmentConflictsMode.Custom;
            scheduler.AllowAppointmentConflicts += Scheduler_AllowAppointmentConflicts;

            scheduler.DataStorage.Appointments.Clear();
            scheduler.GroupType = SchedulerGroupType.Resource;
            Appointment apt1 = scheduler.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, DateTime.Now, DateTime.Now.AddHours(2));
            apt1.ResourceId = scheduler.DataStorage.Resources[0].Id;
            scheduler.DataStorage.Appointments.Add(apt1);
            Appointment apt2 = scheduler.DataStorage.Appointments.CreateAppointment(AppointmentType.Normal, DateTime.Now, DateTime.Now.AddHours(2));
            apt2.ResourceId = scheduler.DataStorage.Resources[1].Id;
            scheduler.DataStorage.Appointments.Add(apt2);
            #endregion #AllowAppointmentConflictsEvent
        }
        #region #@AllowAppointmentConflictsEvent
        static void Scheduler_AllowAppointmentConflicts(object sender, AppointmentConflictEventArgs e) {
            e.Conflicts.Clear();
            FillConflictedAppointmentsCollection(e.Conflicts, e.Interval, ((SchedulerControl)sender).Storage.Appointments.Items, e.AppointmentClone);
        }
        static void FillConflictedAppointmentsCollection(AppointmentBaseCollection conflicts, TimeInterval interval,
            AppointmentBaseCollection collection, Appointment currApt) {
            for (int i = 0; i < collection.Count; i++) {
                Appointment apt = collection[i];
                if (new TimeInterval(apt.Start, apt.End).IntersectsWith(interval) & !(apt.Start == interval.End || apt.End == interval.Start)) {
                    if (apt.ResourceId == currApt.ResourceId) {
                        conflicts.Add(apt);
                    }
                }
                if (apt.Type == AppointmentType.Pattern) {
                    FillConflictedAppointmentsCollection(conflicts, interval, apt.GetExceptions(), currApt);
                }
            }
        }
        #endregion #@AllowAppointmentConflictsEvent

        static void ConflictCalculator(SchedulerControl scheduler)
        {
            #region #ConflictCalculator
            scheduler.CustomDrawAppointmentBackground += scheduler_CustomDrawAppointmentBackground;
            scheduler.ActiveView.LayoutChanged();
            #endregion #ConflictCalculator
        }

        #region #@ConflictCalculator
        private static void scheduler_CustomDrawAppointmentBackground(object sender, CustomDrawObjectEventArgs e)
        {
            SchedulerControl scheduler = sender as SchedulerControl;
            AppointmentViewInfo viewInfo = (e.ObjectInfo as DevExpress.XtraScheduler.Drawing.AppointmentViewInfo);
            Appointment apt = viewInfo.Appointment;
            AppointmentBaseCollection allAppointments = scheduler.ActiveView.GetAppointments();
            AppointmentConflictsCalculator aCalculator = new AppointmentConflictsCalculator(allAppointments);
            TimeInterval visibleInterval = scheduler.ActiveView.GetVisibleIntervals().Interval;
            bool isConflict = aCalculator.CalculateConflicts(apt, visibleInterval).Count != 0;
            // Paint conflict appointments with a red and white hatch brush.
            if (isConflict)
            {
                Rectangle rect = e.Bounds;
                Brush brush = e.Cache.GetSolidBrush(scheduler.DataStorage.Appointments.Labels.GetById(apt.LabelKey).GetColor());
                e.Graphics.FillRectangle(brush, rect);
                rect.Inflate(-3, -3);
                HatchBrush hatchBrush = new HatchBrush(HatchStyle.WideUpwardDiagonal, Color.Red, Color.White);
                e.Graphics.FillRectangle(hatchBrush, rect);
                e.Handled = true;
            }
        }
        #endregion #@ConflictCalculator
    }
}
