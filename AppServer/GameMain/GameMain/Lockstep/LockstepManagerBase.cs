using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameMain;
using GameFramework;
using UnityEngine;
namespace GameMain.Lockstep
{
    public class LockstepManagerBase: GameFrameworkModule //
    {
        public const int m_FrameRate = 40;//逻辑帧刷新率

        public const float m_DeltaTimeF = MathUtils.OneNub / m_FrameRate;//间隔
        public uint FrameCount { get;  set; }


        

        public bool LockstepStarted { get; set; }

       


        FrameManager m_FrameManager;
        public FrameManager FrameManager
        {
            get
            {
                return m_FrameManager;
            }
        }


        public virtual  void Setup()
        {
            LockstepStarted = false;

            FrameCount = 0;
            m_FrameManager = new FrameManager();
            m_FrameManager.Setup();
        }

        public virtual void Start()
        {
            LockstepStarted = true;
        }
        public virtual void Stop()
        {
            LockstepStarted = false;
        }

        public virtual void HandleCommands(List<Command> ls_command)
        {

        }
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {

        }
        public virtual void Update(float elapseSeconds)
        {

        }

        public override bool BeforeInit()
        {
            return true;
        }

        public override bool Init()
        {
            Setup();
            return true;
        }

        public override bool AfterInit()
        {
            return true;
        }

        public override bool BeforeShutdown()
        {
            return true;
        }

        public override bool Shutdown()
        {
            return true; 
        }

        public override bool AfterShutdown()
        {
            return true;
        }
    }
}
