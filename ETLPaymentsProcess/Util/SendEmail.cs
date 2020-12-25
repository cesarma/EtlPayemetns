using ETLPaymentsProcess.Properties;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETLPaymentsProcess.Util
{
    public class SendEmail
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendAutomatedEmail(string htmlString, bool isSuccess, string[] attachmentPaths = null)
        {
            string mailServer = Settings.Default.EmailServer;
            string emailFrom = Settings.Default.EmailFrom;
            string emailTo = Settings.Default.EmailTo;
            string emailSubjSuccSub = Settings.Default.EmailSuccSub;
            string emailSubjFailed = Settings.Default.EmailFailedSub;

            List<string> listTo = emailTo.Split(';').ToList();

            MailMessage message = new MailMessage();

            message.From = new MailAddress(emailFrom);

            listTo.ForEach(x => message.To.Add(x));

            message.IsBodyHtml = true;
            message.Body = htmlString;

            string env = Environment.MachineName.ToString().ToUpper();
            if (env.Contains("PRO"))
            {
                env = "Production";
            }
            else if (env.Contains("UAT"))
            {
                env = "Pre-production";
            }
            else if (env.Contains("DEV"))
            {
                env = "Test";
            }

            message.Subject = String.Format("({0}):", env) + (isSuccess ? Settings.Default.EmailSuccSub : Settings.Default.EmailFailedSub);
            if (attachmentPaths != null)
            {
                foreach (string path in attachmentPaths)
                {
                    Attachment att = new Attachment(path);
                    message.Attachments.Add(att);
                }
            }
            SmtpClient client = new SmtpClient(mailServer);

            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                log.Error("Not able to send email notification. ", ex);
                Console.WriteLine("Exception caught in SendEmail(): {0}",
                            ex.ToString());
            }
        }
    }
}
