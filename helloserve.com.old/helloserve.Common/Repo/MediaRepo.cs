using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace helloserve.Common
{
    public class MediaRepo : BaseRepo<Media>
    {
        public static Media GetByID(int mediaID)
        {
            var media = (from m in DB.MediaItems
                         where m.MediaID == mediaID
                         select m).SingleOrDefault();

            return media;
        }

        public static Media GetByFilename(string name)
        {
            var media = (from m in DB.MediaItems
                         where m.FileName == name
                         select m).SingleOrDefault();

            return media;
        }

        public static Media GetByLocation(string location)
        {
            var media = (from m in DB.MediaItems
                         where m.Location == location
                         select m).SingleOrDefault();

            return media;
        }

        public static IEnumerable<Media> GetInLocation(string location)
        {
            if (!location.EndsWith("\\"))
                location += "\\";

            var media = (from m in DB.MediaItems
                         where m.Location.Replace(location, "").IndexOf("\\") < 0 
                         select m);

            return media;
        }

        public static IEnumerable<Media> GetMediaForFeature(int featureID)
        {
            var media = (from m in DB.MediaItems
                         join fm in DB.FeatureMediaItems on m.MediaID equals fm.MediaID
                         where fm.FeatureID == featureID
                         select m);

            return media;
        }

        public static void Remove(Media item)
        {
            var links = from l in DB.FeatureMediaItems
                        where l.MediaID == item.MediaID
                        select l;

            foreach (var link in links)
            {
                link.Delete();
            }

            item.Delete();
        }

        public static int Scan(string path)
        {
            List<Media> mediaItems = new List<Media>();

            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                dir.Create();

            ScanStep(dir, mediaItems);

            int newCount = mediaItems.Count;
            foreach (Media item in mediaItems)
                item.Save();

            Dictionary<string, int> features = FeatureRepo.GetMediaFolders();
            List<FeatureMedia> linkedFeatures = new List<FeatureMedia>();

            string lPath;
            foreach (string key in features.Keys)
            {
                lPath = Path.Combine(path, key);
                foreach (Media item in MediaRepo.GetInLocation(lPath).ToList())
                {
                    FeatureMediaRepo.Link(features[key], item.MediaID);
                }
            }

            return newCount;
        }

        private static void ScanStep(DirectoryInfo dir, List<Media> mediaItems)
        {
            FileInfo[] files = dir.GetFiles();

            Media item;
            foreach (FileInfo file in files)
            {
                item = GetByLocation(file.FullName);

                if (item == null)
                {
                    using (Image image = Image.FromFile(file.FullName))
                    {
                        item = new Media()
                        {
                            FileName = file.Name,
                            Width = image.Width,
                            Height = image.Height,
                            Location = file.FullName,
                            MediaType = (int)MediaType.Image,
                            DateAdded = DateTime.Today
                        };

                        mediaItems.Add(item);
                    }
                }
            }

            DirectoryInfo[] subDirectories = dir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirectories)
            {
                ScanStep(subDir, mediaItems);
            }
        }
    }
}
