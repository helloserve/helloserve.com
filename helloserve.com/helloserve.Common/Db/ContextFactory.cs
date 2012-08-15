using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace helloserve.Common
{
    public static class ContextFactory<T> where T: DbContext
    {
        /// <summary>
        /// Hook up the delegate to external dababase context. Used internally in assembly
        /// </summary>
        public static Func<T> GetContextHandler { internal get; set; }

        /// <summary>
        /// Used interally in assembly to access the database context provided from externally e.g. Web Site
        /// </summary>
        /// <returns></returns>
        internal static T GetContext()
        {
            return GetContextHandler();
        }
    }
}
