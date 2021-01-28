using ETLPaymentsProcess.Pipelines;
using log4net;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETLPaymentsProcess.Util
{
    class PipelineRunner
    {
      
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public  static bool hasProcess = true;        


   
        public static bool RunProcess(NamedEtlProcess process, ConsoleSpinner spin)
        {
            try
            {

                //Console.WriteLine(process.ReadableName+ " Started.");
                log.Debug(process.ReadableName + " Started.");
                // bool hasProcess = true;


                Task.Factory.StartNew(() => Runspin(hasProcess, spin));

                


                process.Execute();
             //   Runspin(hasProcess, spin);
                var errors = process.GetAllErrors();
                bool hasError = false;
                foreach (var e in errors)
                {
                    hasError = true;
                    hasProcess = false;
                    Task.Factory.StartNew(() => Runspin(hasProcess, spin));
                    //   Runspin(hasProcess, spin);
                    //Console.WriteLine(e);
                    log.Error(String.Format("Error found in {0}", process.ReadableName), e);
                }
                hasProcess = false;
                // Runspin(hasProcess, spin);
                Task.Factory.StartNew(() => Runspin(hasProcess, spin));
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

        private static void Runspin(bool hasProcess)
        {
            throw new NotImplementedException();
        }

        private static void Runspin(bool hasProcess, ConsoleSpinner spin)
        {
            
            Console.Write("Working Please wait....");
           
            while (hasProcess)
            {
                spin.Turn();
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

    public class ConsoleSpinner
    {
        int counter;

        public void Turn()
        {
            try
            {

            
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); counter = 0; break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
                default: Console.Write("/"); counter = 0; break;
            }
            Thread.Sleep(100);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
            catch (Exception)
            {

                
            }
        }
    }
}
