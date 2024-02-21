using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Shared
{
    public  class ProductParams
    {
        public int PageNumber { get; set; } = 1;
        private int  MaxPageSize =15;
        private int MinPageSize =3;

        public int PageSize { get => MinPageSize; set => MinPageSize = value> MaxPageSize ? MaxPageSize : value; }
        public string Sort { get; set; }
        public int? CategoryId { get; set; }

        private string _search;
        public string Search { get => _search; set => _search = value.ToLower(); }
    }
}
