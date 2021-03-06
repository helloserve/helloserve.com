﻿using System.Collections.Generic;
using System.Threading.Tasks;
using helloserve.com.Domain.Models;

namespace helloserve.com.Domain
{
    public interface IBlogDatabaseAdaptor
    {
        Task<Blog> Read(string title);
        Task Save(Blog blog);
        Task<IEnumerable<BlogListing>> ReadListings(int page, int count, string blogOwnerKey = null, bool publishedOnly = true);
        Task<IEnumerable<Blog>> ReadLatest(int count);
    }
}
