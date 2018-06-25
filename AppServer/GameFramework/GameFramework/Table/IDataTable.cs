using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework.Table
{
   public interface IDataTable
    {
        /// <summary>
        /// 数据表的名字
        /// </summary>
        /// 

        string Name
        {
            get;
        }

        /// <summary>
        /// 
        /// 数据资源名字
        /// </summary>
        int AssetId
        {
            get;
        }
        /// <summary>
        /// 
        /// 是否加载
        /// </summary>
        bool IsLoad
        {
            set;
            get;
        }

        bool ParseTable(JArray jay);

    }
}
