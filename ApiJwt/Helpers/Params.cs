using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.StyledXmlParser.Jsoup.Helper;

namespace API.Helpers
{
    public class Params
    {
        const int MaxPageSize = 50;
        private int _pageSize = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize || value <= 0) ? MaxPageSize : value; }
        }
        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = (value <= 0) ? 1 : value; }
        }
        private string _search;
        public string Search
        {
            get { return _search  ; }
            set { _search  = String.IsNullOrEmpty(value) ? "" : value.ToLower(); }
        }
        
        
        
    }
}