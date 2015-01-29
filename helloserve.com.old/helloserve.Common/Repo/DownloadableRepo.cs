using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace helloserve.Common
{
    public class DownloadableRepo : BaseRepo<Downloadable>
    {
        public static Downloadable GetByID(int id)
        {
            return DB.DownloadableItems.Where(d => d.DownloadableID == id).Single();
        }

        public static Downloadable GetByFilename(string name)
        {
            return DB.DownloadableItems.Where(d => d.Name == name).SingleOrDefault();
        }

        public static Downloadable GetByLocation(string location)
        {
            return DB.DownloadableItems.Where(d => d.Location == location).SingleOrDefault();
        }

        public static IEnumerable<Downloadable> GetForFeature(int featureID)
        {
            return DB.DownloadableItems.Where(d => d.FeatureID == featureID);
        }

        public static IEnumerable<Downloadable> ScanFor(string path)
        {
            if (!Directory.Exists(path))
                return new List<Downloadable>();

            DirectoryInfo dir = new DirectoryInfo(path);
            FileSystemInfo[] fileInfos = dir.GetFileSystemInfos("*.*", SearchOption.AllDirectories);
            
            foreach (FileSystemInfo info in fileInfos)
            {
                Downloadable dl = GetByLocation(info.FullName);
                if (dl == null)
                {
                    dl = new Downloadable()
                    {
                        Name = info.Name,
                        Location = info.FullName,
                        ModifiedDate = info.LastWriteTimeUtc,
                    };
                }
                else
                {
                    dl.ModifiedDate = info.LastWriteTimeUtc;
                }

                dl.Save();
            }

            return GetAll();
        }
    }
}
