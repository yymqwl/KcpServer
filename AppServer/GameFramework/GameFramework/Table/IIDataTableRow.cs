using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GameFramework.Table
{
    public interface IIDataTableRow
    {
        bool ParseRow(JObject jobj);
    }
}
