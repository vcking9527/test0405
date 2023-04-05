using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELK.Demo.WebApi.Models.Pagination
{
     public class PaginationList<T>
    {
        /// <summary>
        /// 目前頁面的編號
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// 每個頁面的大小
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 頁面總數量
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 項目的總數量
        /// </summary>
        public long TotalItems { get; private set; }

        /// <summary>
        /// 目前分頁面上的項目
        /// </summary>
        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        /// 是否有上一頁
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// 是否有下一頁
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// PageList建構子
        /// </summary>
        /// <param name="items">目前頁面的項目</param>
        /// <param name="count">所有項目的總數量</param>
        /// <param name="currentPage">目前頁面</param>
        /// <param name="pageSize">一個頁面有幾筆</param>
        public PaginationList(IEnumerable<T> items, long count, int currentPage, int pageSize)
        {
            Items = items;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;
        }

        public PaginationList(int currentPage, int pageSize)
            : this(new List<T>(), 0, currentPage, pageSize)
        {
        }

        public static PaginationList<T> Empty(int pageNumber, int pageSize)
        {
            return new PaginationList<T>(new List<T>(), 0, pageNumber, pageSize);
        }

        public PaginationList<TDist> Convert<TDist>(Func<T, TDist> convert)
        {
            var newItems = Items.Select(i => convert(i));
            return new PaginationList<TDist>(newItems, TotalItems, CurrentPage, PageSize);
        }
    }
}