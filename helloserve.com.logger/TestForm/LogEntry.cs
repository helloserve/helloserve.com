using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALog;
using System.Runtime.InteropServices;

namespace TestForm
{
    public class LogEntry : LogElement
    {
        public override void FillParams(Dictionary<string, object> parameters)
        {
            parameters["Message"] = "This is a custom modified log. " + parameters["Message"];
        }
    }
}
