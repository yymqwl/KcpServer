using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMain;
using GameFramework;



namespace GameMain.Lockstep
{
    public class LockstepReplayManager : LockstepManagerBase
    {
        public GameFrameworkAction m_Act_GameFrameTurn;

        private float AccumilatedTime = 0f;



        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (!LockstepStarted)
                return;

            AccumilatedTime += elapseSeconds;
            if (AccumilatedTime >= m_DeltaTimeF)
            {
                GameFrameTurn();
                AccumilatedTime -= m_DeltaTimeF;
                

                if(FrameCount>= FrameManager.GetMaxFrameId())
                {
                    Stop();//结束播放
                }
            }


        }

        public override void Stop()
        {
            base.Stop();
            DebugHandler.Log("finish");
        }
        public override void HandleCommands(List<Command> ls_command)
        {
            DebugHandler.Log(ls_command[0].Command_Id + ls_command[0].Dict_Param[1].ToString());
        }


        public void GameFrameTurn()//一帧的刷新
        {
            Update(m_DeltaTimeF);//刷新逻辑帧
            /////////////////////////
            ///////////////////
            FrameCount++;
            Frame fm = FrameManager.GetFrameById(FrameCount);
            if (fm == null || fm.Commands == null)
            {
                return;
            }
            HandleCommands(fm.Commands);

            GameFrameworkActionExtension.InvokeGracefully(m_Act_GameFrameTurn);
        }
    }
}