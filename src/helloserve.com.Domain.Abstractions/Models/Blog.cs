﻿using System;

namespace helloserve.com.Domain.Models
{
    public class Blog : BlogListing
    {
        public bool IsPublished { get; set; }
        public string Content { get; set; }
    }
}
