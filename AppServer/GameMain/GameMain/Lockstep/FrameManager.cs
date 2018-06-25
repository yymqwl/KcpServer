using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using GameFramework;
using GameMain;

namespace GameMain.Lockstep
{
    public  class FrameManager
    {
        private const int StartCapacity = 20*60*LockstepManagerBase.m_FrameRate;//20分钟
        List<Frame> m_frames = new List<Frame>(StartCapacity);//游戏总帧

        Frame m_start_fm;
        Frame m_end_fm;

        int m_index_end;
        public void Setup()
        {

            ResetFrame();
            ResetIndex();
        }
        void ResetFrame()
        {
            m_frames.Clear();
            m_start_fm = new Frame();
            m_start_fm.FrameId = 0;
            m_frames.Add(m_start_fm);

            m_end_fm = new Frame();
            m_end_fm.FrameId = 0;

            m_frames.Add(m_end_fm);

        }

        
        void ResetIndex()
        {
            m_index_end = 1;
        }
        
        public void PushBackFrame(Frame fm)
        {
            if(m_end_fm.FrameId < fm.FrameId)//加入
            {
                m_end_fm.FrameId = fm.FrameId;
                if (fm.Commands == null)
                {
                    //m_end_fm.FrameId = fm.FrameId;
                }
                else
                {

                    m_index_end++;
                    m_frames.Add(m_end_fm);
                    //m_frames[m_index_end] = m_end_fm;
                    m_frames[m_index_end - 1] = fm;

                }
            }
            else
            {
                DebugHandler.Log("fmframeid"+fm.FrameId);
            }
            
        }

        public uint GetMaxFrameId()
        {
            return m_end_fm.FrameId;
        }

        public Frame GetFrameById(uint id)
        {
           return m_frames.Find((Frame fm) =>
            {
                if (fm.FrameId == id)
                    return true;
                return false;
            }
            );

        }

       public void SetReplay(Replay rp)
       {
            m_frames = rp.m_ls_frame;
            if(m_frames.Count <2)
            {
                DebugHandler.LogError("error Lenght");
            }
            m_index_end = m_frames.Count - 1;
            m_start_fm = m_frames[0];
            m_end_fm = m_frames[m_index_end];

       }
       public Replay GetReplay()
        {
            Replay rp = new Replay();
            rp.m_ls_frame= m_frames.ToList();
            rp.m_Date = DateTime.Now;
            rp.m_Name = DateTime.Now.ToString();
            return rp;

        }

    }

    [ProtoContract]
    public class Frame
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// 

        [ProtoMember(1)]
        public uint FrameId
        { get; set; }
        [ProtoMember(2)]
        public List<Command> Commands;
        public void AddCommand(Command com)
        {
            if (Commands == null)
            {
                Commands = new List<Command>();
            }
            Commands.Add(com);
        }
        public static Frame Create(uint id)
        {
            Frame fm = new Frame();
            fm.FrameId = id;
            return fm;
        }


    }

}
