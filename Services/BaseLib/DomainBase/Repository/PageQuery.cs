using System.Collections.Generic;

namespace DomainBase
{
    public class PageQuery<T>
    {
        public int PageIndex { get; set; }
        public int Total { get; set; }
        public List<T> Data { get; set; }
    }
}