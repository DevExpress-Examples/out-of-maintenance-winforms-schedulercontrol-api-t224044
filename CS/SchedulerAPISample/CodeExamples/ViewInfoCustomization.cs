using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace SchedulerAPISample.CodeExamples {
    class ViewInfoCustomization {

        static void LayoutViewInfoCustomizingEvent(SchedulerControl scheduler) {
            #region #LayoutViewInfoCustomizingEvent
            scheduler.LayoutViewInfoCustomizing += scheduler_LayoutViewInfoCustomizing; ;
            scheduler.ActiveView.LayoutChanged();
            #endregion #LayoutViewInfoCustomizingEvent
        }

        #region #@LayoutViewInfoCustomizingEvent
        public static void scheduler_LayoutViewInfoCustomizing(object sender, LayoutViewInfoCustomizingEventArgs e) {
            string s = e.ViewInfo.GetType().ToString().Substring("DevExpress.XtraScheduler.Drawing.".Length);
            if (e.Kind == LayoutElementKind.DateHeader) {
                SchedulerHeader header = e.ViewInfo as SchedulerHeader;
                if (header != null) header.Caption = s;
            }
            if (e.Kind == LayoutElementKind.Cell) {
                SchedulerViewCellBase cell = e.ViewInfo as SchedulerViewCellBase;
                if (cell != null) cell.Appearance.BackColor = Color.LightYellow;
                SingleWeekCellBase cellWeek = e.ViewInfo as SingleWeekCellBase;
                if (cellWeek != null) {
                    cellWeek.Appearance.BackColor = Color.LightCyan;
                    cellWeek.Header.Caption = s;
                }
            }
        }
        #endregion #@LayoutViewInfoCustomizingEvent

    }
}
