using System.Collections;
using System.Collections.Generic;
using GameFramework.Procedure;
using GameFramework;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Setting;
using GameMain.Net;

namespace GameMain
{
    public class Procedure_Setting : ProcedureBase
    {


        public override void OnInit(ProcedureOwner procedureOwner)
        {
            ////////添加组件的顺序
            GameMainEntry.GetModule<LogManager>();
            GameMainEntry.GetModule<NetWorkManager>();
            ///

            GameMainEntry.Init();
        }

        public override void OnEnter(ProcedureOwner procedureOwner)
        {

     

            foreach (GameFrameworkModule module in GameMainEntry.GameFrameworkModules)
            {
                DebugHandler.Log(module.GetType().Name+ module.Priority);
            }

            ///////////////////加载所有模块
            /////////////////////////

            //////////////////////////////////////////////////////////////////////////
          
        }



        public override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {

        }

        public override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            //throw new NotImplementedException();
        }

        public override void OnDestroy(ProcedureOwner procedureOwner)
        {
            //throw new NotImplementedException();
        }
    }


}