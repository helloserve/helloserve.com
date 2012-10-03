using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class FeatureRequirementModel
    {
        public Requirement Requirement { get; set; }
        public FeatureRequirement FeatureRequirement { get; set; }
    }

    public class SubmittedMediaItem
    {
        public string filename;
        public string type;
        public byte[] data;

        public void SaveData(string ContentPath)
        {
            if (data == null || data.Length == 0)
                return;
        }
    }
}
