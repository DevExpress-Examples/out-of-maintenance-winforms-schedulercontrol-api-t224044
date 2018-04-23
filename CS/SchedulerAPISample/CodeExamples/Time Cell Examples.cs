using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerAPISample.CodeExamples {
    class TimeCellExamples {

        static void TimeCellTypes(SchedulerControl scheduler) {
            #region #TimeCellTypes
            scheduler.CustomDrawTimeCell += scheduler_CustomDrawTimeCell;
            scheduler.CustomDrawDayViewAllDayArea += Scheduler_CustomDrawDayViewAllDayArea;
            scheduler.TimelineView.GetBaseTimeScale().Width = 150;
            scheduler.WeekView.Enabled = true;
            scheduler.DayView.DayCount = 3;
            scheduler.ActiveViewType = SchedulerViewType.Day;
            scheduler.GroupType = SchedulerGroupType.Date;
            scheduler.DateNavigationBar.Visible = false;
            scheduler.LookAndFeel.UseDefaultLookAndFeel = false;
            scheduler.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            scheduler.LookAndFeel.SkinName = "DevExpress Style";
            #endregion #TimeCellTypes
        }

        #region #@TimeCellTypes
        public static void scheduler_CustomDrawTimeCell(object sender, CustomDrawObjectEventArgs e) {
            e.DrawDefault();
            DrawCellInfo(e);
            e.Handled = true;
        }
        private static void Scheduler_CustomDrawDayViewAllDayArea(object sender, CustomDrawObjectEventArgs e) {
            e.DrawDefault();
            DrawCellInfo(e);
            e.Handled = true;
        }
        private static void DrawCellInfo(CustomDrawObjectEventArgs e) {
            string s = e.ObjectInfo.GetType().ToString().Substring("DevExpress.XtraScheduler.Drawing.".Length);
            SchedulerViewCellBase cell = e.ObjectInfo as SchedulerViewCellBase;
            if (cell != null) {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Cache.DrawString(s, new Font("Tahoma", 10), SystemBrushes.GrayText, cell.Bounds, sf);
            }
        }
        #endregion #@TimeCellTypes

        static void HorizontalSingleWeekCellType(SchedulerControl scheduler) {
            #region #HorizontalSingleWeekCellType
            scheduler.CustomDrawTimeCell += scheduler_CustomDrawTimeCell_01;
            scheduler.ActiveViewType = SchedulerViewType.Month;
            scheduler.GroupType = SchedulerGroupType.Date;
            scheduler.DateNavigationBar.Visible = false;
            scheduler.LookAndFeel.UseDefaultLookAndFeel = false;
            scheduler.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            scheduler.LookAndFeel.SkinName = "DevExpress Style";
            #endregion #HorizontalSingleWeekCellType
        }

        #region #@HorizontalSingleWeekCellType
        public static void scheduler_CustomDrawTimeCell_01(object sender, CustomDrawObjectEventArgs e) {
            e.DrawDefault();
            HorizontalSingleWeekCell cell = e.ObjectInfo as HorizontalSingleWeekCell;
            if (cell != null) {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                e.Cache.DrawRectangle(SystemPens.ActiveBorder, cell.Bounds);
                if (cell.FirstVisible) e.Cache.DrawString("First Visible Cell", new Font("Tahoma", 8),
                        Brushes.Blue, cell.Bounds, sf);
            }
            e.Handled = true;
        }
        #endregion #@HorizontalSingleWeekCellType
    }
}
