using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public class StageDetail
    {
        /// <summary>
        /// The id of a stage.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The description of the stage.
        /// </summary>
        public string Name { get; set; }
    }
}