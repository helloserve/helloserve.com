using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.com.Logger
{
    internal static class Extensions
    {
        public static string AsLogString(this Exception exception)
        {
            StringBuilder blr = new StringBuilder();
            Exception ex = exception;
            blr.AppendLine(ex.Message);
            blr.AppendLine(ex.StackTrace);
            ex = ex.InnerException;
            while (ex != null)
            {
                blr.AppendLine("-- inner exception --");
                blr.AppendLine(ex.Message);
                blr.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
                if (ex == null)
                    blr.AppendLine("-- end of inner exceptions --");
            }

            return blr.ToString();
        }
    }
}
