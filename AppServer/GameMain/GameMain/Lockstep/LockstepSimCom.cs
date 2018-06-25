using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;
using GameMain;
using UnityEngine;
using GameMain.Lockstep;
namespace GameMain.Lockstep
{

    public class LockstepSimCom : MonoBehaviour
    {

        LockstepGameManager lockstep_gm;
        LockstepReplayManager lockreplay_gm;
        private void Awake()
        {
            lockstep_gm = GameMainEntry.GetModule<LockstepGameManager>();
            lockreplay_gm = new LockstepReplayManager();
        }

        private void Update()
        {
            lockstep_gm.Update(Time.deltaTime, Time.unscaledDeltaTime);

            lockreplay_gm.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }
        private void FixedUpdate()
        {

            if (!lockstep_gm.LockstepStarted)
                return;
            Frame fm = new Frame();
           
            if (Input.GetKeyDown(KeyCode.A))
            {
                fm.Commands = new List<Command>();
                Command cmd = new Command();
                cmd.Dict_Param.Add(1,new ProtoObject("A"));
                fm.Commands.Add(cmd);
                //DebugHandler.Log("a");
            }


            fm.FrameId = lockstep_gm.FrameManager.GetMaxFrameId()+1;

            lockstep_gm.FrameManager.PushBackFrame(fm);
            /*
             * 
            if (lockstep_gm.FrameManager.PushBackFrame())

                if (Input.GetKeyDown(KeyCode.A))
                {
                    //DebugHandler.Log("a");
                }
            */

        }
        [ContextMenu("StartRecord")]
        void StartRecord()
        {
            lockstep_gm.Start();
        }

        [ContextMenu("StopRecord")]
        void StopRecord()
        {
            lockstep_gm.Stop();
            
        }
        [ContextMenu("RecordData")]
        void RecordData()
        {
            Replay rp = lockstep_gm.FrameManager.GetReplay();
            Byte[]  bufs = ProtoBufUtils.ProtobufSerialize(rp);
            rp = ProtoBufUtils.ProtobufDeserialize<Replay>(bufs);

            lockreplay_gm.Setup();
            lockreplay_gm.FrameManager.SetReplay(rp);
            lockreplay_gm.Start();
  
            //DebugHandler.Log(bufs.Length);
        }
    }
}
