using ETLPaymentsProcess.Pipelines;
using log4net;
using System;

namespace ETLPaymentsProcess.Util
{
    class PipelineRunner
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool RunProcess(NamedEtlProcess process)
        {
            try
            {

                //Console.WriteLine(process.ReadableName+ " Started.");
                log.Debug(process.ReadableName + " Started.");
                process.Execute();

                var errors = process.GetAllErrors();
                bool hasError = false;
                foreach (var e in errors)
                {
                    hasError = true;
                    //Console.WriteLine(e);
                    log.Error(String.Format("Error found in {0}", process.ReadableName), e);
                }
                Console.WriteLine(process.ReadableName + " Finished.");
                log.Info(process.ReadableName + " Finished.");
                return !hasError;
            }
            catch (Exception e)
            {
                log.Error(String.Format("{0} failed.", process.ReadableName), e);
                return false;
            }

        }

        //public static bool runProcess(NamedEtlProcess process)
        //{
        //    try
        //    {
        //        PipelineRunner.Run(process);
        //        if (process.Errors != null && process.Errors.Count() > 0)
        //        {
        //            foreach (var e in process.Errors)
        //            {
        //                log.Error(String.Format("Error found in {0}", process.ReadableName), e);
        //            }
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(String.Format("{0} failed.", process.ReadableName), e);
        //        return false;
        //    }
        //}

    }
}
