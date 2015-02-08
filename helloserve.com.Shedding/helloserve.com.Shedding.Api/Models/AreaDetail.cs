using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    /// <summary>
    /// The entity detailing a specific area.
    /// </summary>
    public class AreaDetail
    {
        /// <summary>
        /// The id of the area.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name or description of the area.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The code of the area.
        /// </summary>
        public string Code { get; set; }
    }
}