using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class StudentGridViewModel
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public List<Student> Students { get; set; }

        public int TotalPages
        {
            //Why not Math.Ceiling((double)Count / PageSize); ?
            get { return (Count + PageSize - 1) / PageSize; }
        }

        public object QueryStringPaging(int page)
        {
            return new
            {
                Search = Search,
                SortBy = SortBy,
                SortOrder = SortOrder,
                PageSize = PageSize,
                Page = page
            };
        }

        public object QueryStringSorting(string field)
        {
            var sortOrder = SortBy == field ? SortOrder == "ASC" ? "DESC" : "ASC" : "ASC";
            return new {
                Search = Search,
                SortBy = field,
                SortOrder = sortOrder,
                PageSize = PageSize
            };
        }
    }
}