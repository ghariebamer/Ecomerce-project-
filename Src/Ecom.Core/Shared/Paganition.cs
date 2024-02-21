using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Shared
{
    public  class Paganition <T>  where T : class
    {
        public Paganition(IReadOnlyList<T> list, int paNum,int paSiz,int count )
        {
            _List = list;
            PageNumber = paNum;
            PageSize = paSiz;
            TotalCount = count;
        }


        public IReadOnlyList<T> _List {  get; set; }
        
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}
