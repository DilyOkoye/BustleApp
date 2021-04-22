using System;
namespace BustleApp_api.Domain.Utilities
{
    public class PagedResultDto
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public string Search { get; set; }
        public string Sort { get; set; }
        public string SortOrder { get; set; }
    }
}
