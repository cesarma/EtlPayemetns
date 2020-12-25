using ETLPaymentsProcess.Pipelines;
using ETLPaymentsProcess.Util;
using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.Text;

namespace ETLPaymentsProcess
{

    class Program
    {

        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static String TIMESTAMP = DateTime.Now.ToString("yyyyMMddHHmmss");
        private static String LOG_FILE_PATH = System.IO.Path.Combine(Properties.Settings.Default.LogFolder, "CIFLog" + TIMESTAMP + Guid.NewGuid().ToString() + ".log");

        static void Main(string[] args)
        {
            try
            {
                //logging setup. Logging is using popular library log4net.
                XmlConfigurator.Configure();

                log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
                foreach (IAppender a in h.Root.Appenders)
                {
                    if (a is FileAppender)
                    {
                        FileAppender fa = (FileAppender)a;
                        string logFileLocation = LOG_FILE_PATH;
                        fa.File = logFileLocation;
                        fa.ActivateOptions();
                        break;
                    }
                }

                log.Info("Program Started.");

                bool CUS_RESULT =  PipelineRunner.RunProcess(new EgiftInfoProcess("eGIFTiNFO File Loading Process"));
                bool DEA_RESULT =  PipelineRunner.RunProcess(new ExchangeRateInfoProcess("eXCHANGE RATE File Loading Process"));
                bool DEA22_RESULT =  PipelineRunner.RunProcess(new RunBuildInfosProcess("Payments File Loading Process"));

                log.Info("Loading Completed.");

                if (CUS_RESULT && DEA_RESULT)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<ul>");
                    sb.Append(String.Format(@"<li>{0}</li>", CUS_RESULT ? "Customer Info File: Success" : "Customer Info File: Error Occurred."));
                    sb.Append(String.Format(@"<li>{0}</li>", DEA_RESULT ? "Deal Detail File: Success" : "Deal Detail File: Error Occurred."));
                    sb.Append("</ul>");

                    SendEmail.SendAutomatedEmail("Confirming Customer Audit files were uploaded successfully.</br>" + sb.ToString(), true, new String[] { LOG_FILE_PATH, Properties.Settings.Default.PaymentInfoFilePath });
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Errors occurred during the loading process.</br>");
                    sb.Append("<ul>");
                    sb.Append(String.Format(@"<li>{0}</li>", CUS_RESULT ? "Customer Info File: Success" : "Customer Info File: Error Occurred."));
                    sb.Append(String.Format(@"<li>{0}</li>", DEA_RESULT ? "Deal Detail File: Success" : "Deal Detail File: Error Occurred."));
                    sb.Append("</ul>");
                    SendEmail.SendAutomatedEmail("Errors occurred during the loading process:</br>" + sb.ToString(), false, new String[] { LOG_FILE_PATH });
                }

                Console.WriteLine("All Processes Finished.Press Enter to exit.");


            }
            catch (Exception e)
            {
                log.Fatal(e);
            }


        }
    }
}
