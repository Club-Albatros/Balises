
using System;
using System.Text;
using DotNetNuke.Services.Scheduling;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class BalisesService : SchedulerClient
    {

        private StringBuilder log = new StringBuilder();

        public BalisesService(ScheduleHistoryItem historyItem)
        {
            ScheduleHistoryItem = historyItem;
        }

        public override void DoWork()
        {
            Progressing();

            try
            {
                log.AppendLine("Finished");
                ScheduleHistoryItem.AddLogNote(log.ToString());
                ScheduleHistoryItem.Succeeded = true;
            }
            catch (Exception ex)
            {
                ScheduleHistoryItem.AddLogNote(log.ToString() + "<br />Scheduled task failed: " + ex.Message + "(" + ex.StackTrace + ")<br />");
                ScheduleHistoryItem.Succeeded = false;
                Errored(ref ex);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }
    }
}