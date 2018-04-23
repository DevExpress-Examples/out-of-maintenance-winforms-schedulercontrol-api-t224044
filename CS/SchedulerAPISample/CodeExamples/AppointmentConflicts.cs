using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Native;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace SchedulerAPISample.CodeExamples
{
    class AppointmentConflicts
    {
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
                Brush brush = e.Cache.GetSolidBrush(scheduler.Storage.Appointments.Labels[apt.LabelId].Color);
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
