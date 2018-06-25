using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFramework;

namespace GameFramework.Table
{
    public   class TableManager : GameFrameworkModule
    {

        /// <summary>
        /// 
        /// 名字数据表
        /// </summary>
        private Dictionary<string, IDataTable> m_dict_table=new Dictionary<string, IDataTable>();

        public Dictionary<string, IDataTable> Dict_Table
        {
            get
            {
                return m_dict_table;
            }
        }
        public override int Priority
        {
            get
            {
                return base.Priority;
            }
        }
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            
        }

        public override bool BeforeInit()
        {
            m_dict_table.Clear();
            return base.BeforeInit();
        }

        public override bool Init()
        {
            return base.Init();
        }

        public override bool AfterInit()
        {
            return base.AfterInit();
        }

        public override bool BeforeShutdown()
        {
            return base.BeforeShutdown();
        }

        public override bool Shutdown()
        {
            return base.Shutdown();
        }

        public override bool AfterShutdown()
        {
            return base.AfterShutdown();
        }

        public  void AddTable(IDataTable  idt)
        {
            if (idt == null)
            {
                throw new GameFrameworkException(string.Format("null IDataTable"));
            }
            string name = idt.Name;
           IDataTable tmpdt = null;
           if ( m_dict_table.TryGetValue(name,out tmpdt) )
           {
              throw new GameFrameworkException( string.Format("readly have {0}", name));
           }
            m_dict_table.Add(name, idt);
        }
        public  bool RemoveTable(string name)
        {
            return m_dict_table.Remove(name);

        }
        /*public void LoadAllTable()
        {
            foreach(IDataTable idt in m_dict_table)
            {
                
            }
        }*/

    }
}
