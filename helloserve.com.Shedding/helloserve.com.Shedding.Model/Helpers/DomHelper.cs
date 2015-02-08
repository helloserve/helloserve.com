using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public static class DomHelper
    {
        public static string FixTag(this string line, string tag)
        {
            string startMatch = string.Format("<{0}", tag);
            string endMatch = "/>";
            if (line.StartsWith(startMatch) && !line.EndsWith(endMatch))
                line = string.Format("{0}</{1}>", line, tag);

            return line;
        }
    }
}
