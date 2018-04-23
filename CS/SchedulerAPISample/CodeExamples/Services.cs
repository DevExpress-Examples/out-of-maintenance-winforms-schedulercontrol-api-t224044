using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerAPISample.CodeExamples
{
    class Services
    {
        static void AppointmentFormatStringServiceAction(SchedulerControl scheduler)
        {
            #region #AppointmentFormatStringService
            IAppointmentFormatStringService oldService = scheduler.GetService<IAppointmentFormatStringService>();
            if (oldService != null)
            {
                MyAppointmentFormatStringService newService = new MyAppointmentFormatStringService(oldService);
                scheduler.RemoveService(typeof(IAppointmentFormatStringService));
                scheduler.AddService(typeof(IAppointmentFormatStringService), newService);
            }
            scheduler.ActiveView.LayoutChanged();
            #endregion #AppointmentFormatStringService
        }

        #region #@AppointmentFormatStringService
        public class MyAppointmentFormatStringService : AppointmentFormatStringServiceWrapper
        {
            public MyAppointmentFormatStringService(IAppointmentFormatStringService service)
                : base(service) { }
            public override string GetVerticalAppointmentStartFormat(
                IAppointmentViewInfo aptViewInfo)
            {
                return "{0: HHmm:ss - 'VerticalAppointmentStart'}";
            }
            public override string GetVerticalAppointmentEndFormat(
                IAppointmentViewInfo aptViewInfo)
            {
                return "{0: HHmm:ss - 'VerticalAppointmentEnd'}";
            }
            public override string GetHorizontalAppointmentEndFormat(
                IAppointmentViewInfo aptViewInfo)
            {
                return "{0: HHmm - 'HorizontalAppointmentEnd'}";
            }
            public override string GetHorizontalAppointmentStartFormat(
                IAppointmentViewInfo aptViewInfo)
            {
                return "{0: HHmm - 'HorizontalAppointmentStart'}";
            }
            public override string GetContinueItemStartFormat(IAppointmentViewInfo aptViewInfo)
            {
                return "{0: HHmm MMM dd - 'ContinueItemStart'}";
            }
            public override string GetContinueItemEndFormat(IAppointmentViewInfo aptViewInfo)
            {
                return "{0: HHmm MMM dd - 'ContinueItemEnd'}";
            }
        }
        #endregion #@AppointmentFormatStringService
    }
}
