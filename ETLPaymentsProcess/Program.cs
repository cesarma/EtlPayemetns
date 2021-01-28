using ETLPaymentsProcess.Pipelines;
using ETLPaymentsProcess.Util;
using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.Text;
using FileHelpers;
using ETLPaymentsProcess.Models;

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

                var engine = new FileHelperEngine<EgiftInfo>();

                var records = engine.ReadFile(Properties.Settings.Default.EgiftInfoFilePath);

                bool CUS_RESULT = false;
                if (records.Length <= 2000)
                    CUS_RESULT = PipelineRunner.RunProcess(new UpdateInsertPaymentsTableProcess("Egift Info Payments File Loading Process"), new ConsoleSpinner()); //SP
                else
                    CUS_RESULT = PipelineRunner.RunProcess(new EgiftInfoProcess("Egift Info Payments File Loading Process"), new ConsoleSpinner()); // SQLBulkCopy its

                //bool DEA_RESULT =   PipelineRunner.RunProcess(new ExchangeRateInfoProcess("Exchange RATE File Loading Process")); use SqlBulkCopy
                bool DEA_RESULT =  PipelineRunner.RunProcess(new UpdateInsertExRatesTableProcess("Exchange RATE File Loading Process"), new ConsoleSpinner()); //use Sp in order to make sure if the record has the same day and they wanna change tthe value of exchange rate per day not duplicates
                bool Payment_RESULT = PipelineRunner.RunProcess(new RunBuildInfosProcess("Payments Extract and Convert Exchange Avg File Loading Process"), new ConsoleSpinner());

                log.Info("Loading Completed.");

                if (CUS_RESULT && DEA_RESULT && Payment_RESULT)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<ul>");
                    sb.Append(String.Format(@"<li>{0}</li>", CUS_RESULT ? "Egift Info Payments File Loading Proces: Success" : "Egift Info Payments File Loading Proces: Error Occurred."));
                    sb.Append(String.Format(@"<li>{0}</li>", DEA_RESULT ? "Exchange RATE File Loading Process: Success" : "Exchange RATE File Loading Process: Error Occurred."));
                    sb.Append(String.Format(@"<li>{0}</li>", Payment_RESULT ? "Payments Extract and Convert Exchange Avg File Loading Process: Success" : "Payments Extract and Convert Exchange Avg File Loading Process: Error Occurred."));
                    sb.Append("</ul>");

                    SendEmail.SendAutomatedEmail("Confirming Payments files were uploaded successfully.</br>" + sb.ToString(), true, new String[] { LOG_FILE_PATH, Properties.Settings.Default.PaymentInfoFilePath });
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Errors occurred during the loading process.</br>");
                    sb.Append("<ul>");
                    sb.Append(String.Format(@"<li>{0}</li>", CUS_RESULT ? "Egift Info Payments File Loading Proces : Success" : "Egift Info Payments File Loading Proces: Error Occurred."));
                    sb.Append(String.Format(@"<li>{0}</li>", DEA_RESULT ? "Exchange RATE File Loading Process: Success" : "Exchange RATE File Loading Process: Error Occurred."));
                    sb.Append(String.Format(@"<li>{0}</li>", Payment_RESULT ? "Payments Extract and Convert Exchange Avg File Loading Process: Success" : "Payments Extract and Convert Exchange Avg File Loading Process: Error Occurred."));
                    sb.Append("</ul>");
                    sb.Append("</ul>");
                    SendEmail.SendAutomatedEmail("Confirming Payments files Errors occurred during the loading process:</br>" + sb.ToString(), false, new String[] { LOG_FILE_PATH });
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
