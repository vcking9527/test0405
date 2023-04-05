using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELK.Demo.WebApi.Dto;
using ELK.Demo.WebApi.Models;
using ELK.Demo.WebApi.Models.Pagination;

namespace ELK.Demo.WebApi.Search
{
    public interface IAccountSearch
    {
        Task<PaginationList<AccountDto>> QueryAsync(AccountQuery query);

        Task<IEnumerable<AccountLineChartDto>> GetAccountsAsync();
    }
}