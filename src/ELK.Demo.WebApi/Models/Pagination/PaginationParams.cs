using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELK.Demo.WebApi.Models.Pagination
{
    public class PaginationParams
    {
        /// <summary>
        /// 目前頁面，預設為1
        /// </summary>         
        /// <example>1</example>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 一個頁面有幾筆，預設為10筆，最大1000筆
        /// </summary>
        /// <example>5</example>
        public int PageSize { get; set; } = 10;
    }
}