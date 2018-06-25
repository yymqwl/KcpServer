using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GameMain;
using GameFramework;

namespace GameMain.Lockstep
{
    public class LockstepGameManager : LockstepManagerBase
    {


        //public const int m_AddCount_Nub = 40;//每次追加播放的帧
        //public float m_t_AccumilatedTime = 0f;//积累的没有播放的帧
        public  GameFrameworkAction m_Act_GameFrameTurn;

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (!LockstepStarted)
                return;





            while(FrameCount< FrameManager.GetMaxFrameId())
            {
                GameFrameTurn();
            }
        }



        public void GameFrameTurn()//一帧的刷新
        {
            Update(m_DeltaTimeF);//刷新逻辑帧
            /////////////////////////
            ///////////////////
            FrameCount++;
            Frame fm = FrameManager.GetFrameById(FrameCount);
            if(fm == null || fm.Commands ==null)
            {
                return;
            }
            HandleCommands(fm.Commands);

            GameFrameworkActionExtension.InvokeGracefully(m_Act_GameFrameTurn);
        }
        public override void HandleCommands(List<Command> ls_command)
        {
            DebugHandler.Log(ls_command[0].Command_Id+ ls_command[0].Dict_Param[1].ToString());
        }

        public override void Update(float elapseSeconds)
        {

        }

        public override bool Init()
        {
            //m_record_fm = new Frame();
            return base.Init();
        }
    }
}
