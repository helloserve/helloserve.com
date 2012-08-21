using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace helloserve.Common
{
    public class helloserveContext : DbContext
    {
        public DbSet<Feature> Features { get; set; }
        public DbSet<News> NewsItems { get; set; }
        public DbSet<Media> MediaItems { get; set; }
        public DbSet<Downloadable> DownloadableItems { get; set; }
        public DbSet<SellingPoint> SellingPoints { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<FeatureRequirement> FeatureRequirements { get; set; }
        public DbSet<FeatureMedia> FeatureMediaItems { get; set; }
        public DbSet<RelatedLink> RealtedLinks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
