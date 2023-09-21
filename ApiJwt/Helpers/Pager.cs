using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class Pager<T> where T : class
    {
        public string Search { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Registers {get; set;}
        public Pager(IEnumerable<T> registers, int total, int pageIndex, int pageSize, string search){
            this.Search = search;
            this.Total = total;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Registers = registers;
        }
        public int TotalPages { get{
            if (PageSize <= 0){
                PageSize = 1;
            }
            return (int)Math.Ceiling((decimal)Total/PageSize);
        } }
        public bool HasPreviousPage {get {
            return PageIndex > 1;
        }}
        public bool HasNextPage { get{
            return PageIndex < TotalPages;
        } }
    }
}