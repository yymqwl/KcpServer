using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameFramework;

namespace GameMain
{
    /// <summary>
    /// 任务调度类
    /// </summary>
    public class TaskDispatch
    {
        private static object threadLock = new object();
        private static TaskDispatch _TaskDispatch = null;

        /// <summary>
        /// 
        /// </summary>
        public bool Running
        {
            get { return _running; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static TaskDispatch StartTask()
        {
            if (_TaskDispatch == null)
            {
                lock (threadLock)
                {
                    if (_TaskDispatch == null)
                    {
                        _TaskDispatch = new TaskDispatch();
                    }
                }
            }

            _TaskDispatch.Start();

            return _TaskDispatch;
        }

        private Timer thread;
        private long intevalTicks = 1000;
        private bool _running;
        private List<BaseTask> taskList = new List<BaseTask>();

        private TaskDispatch()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public void Add(BaseTask task)
        {
            taskList.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            taskList.Clear();
        }

        private void ThreadProcess(object state)
        {
            try
            {
                if (Monitor.TryEnter(threadLock, 100))
                {
                    try
                    {
                        long currTime = DateTime.Now.Ticks;
                        foreach (BaseTask task in taskList)
                        {
                            if (task == null) continue;

                            if(!task.Check(ref currTime))
                            {
                                continue;
                            }
                            ThreadPool.QueueUserWorkItem(new WaitCallback(task.Proccess), task.TaskName);
                        }
                    }
                    finally
                    {
                        Monitor.Exit(threadLock);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new GameFrameworkException("TaskDispatch process error:" + ex.ToString());
                //AppModuleManager.Instance.LogModule.Log.Error("TaskDispatch process error:" + ex.ToString());

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (thread == null)
            {
                thread = new Timer(ThreadProcess, null, 100, intevalTicks);
                _running = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            thread.Change(-1, Timeout.Infinite);
            _running = false;
        }
        /// <summary>
        /// 
        /// </summary>
        ~TaskDispatch()
        {
            _running = false;
            thread.Dispose();
        }




        
    }
}
