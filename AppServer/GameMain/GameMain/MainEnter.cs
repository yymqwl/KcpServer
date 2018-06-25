using GameFramework.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameFramework.Fsm;

namespace GameMain
{
    public  class MainEnter
    {

        //ProcedureManager m_pdm;
        public  void Enter()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptHandler);

            var pd_mg =  GameMainEntry.GetModule<ProcedureManager>();
            var pd_fsm = new FsmManager();
            var pd_setting = new Procedure_Setting();
            pd_mg.Initialize(pd_fsm, pd_setting);
            pd_mg.StartProcedure<Procedure_Setting>();




            bool m_quit = false;
            DateTime lastdt = DateTime.Now;
            DateTime curdt = DateTime.Now;
            TimeSpan curts;
            float elapseSeconds = 0;
            Task.Factory.StartNew(() =>
            {
                bool bloop = true;
                while ( bloop)
                {
                    string strcmd = Console.ReadLine();
                    if (strcmd == "quit")
                    {
                        bloop = false;
                        m_quit = true;
                    }
                    else if (strcmd =="exp")
                    {
                    }
                }
            }
            );




            while (!m_quit)
            {
                Thread.Sleep(GameConstant.TThreadInternal);
                curdt = DateTime.Now;
                curts =  curdt - lastdt;
                elapseSeconds = (float)curts.TotalMilliseconds / 1000;
                GameMainEntry.Update(elapseSeconds, elapseSeconds);

                //m_pdm.Execute((float)curts.TotalMilliseconds/1000);
                lastdt = curdt;
            }
            
            //m_pdm.Shut();
            return ;

        }

         void ExceptHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            AppModules.Instance.LogManager.Log.Error("Sender : " + sender.ToString() + "  ExceptHandler caught : " + e.Message);
            Environment.Exit(0);

        }

        public void Exit()
        {
        }

    }
}
