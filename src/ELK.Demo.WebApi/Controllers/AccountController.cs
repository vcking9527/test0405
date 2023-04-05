using ELK.Demo.WebApi.Dto;
using ELK.Demo.WebApi.Models;
using ELK.Demo.WebApi.Models.Pagination;
using ELK.Demo.WebApi.Search;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ELK.Demo.WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountSearch _accountSearch;

        public AccountController(ILogger<AccountController> logger, IAccountSearch accountSearch)
        {
            _logger = logger;
            _accountSearch = accountSearch;
        }

        /// <summary>
        /// Query 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse<PaginationList<AccountDto>>> Query([FromQuery] AccountQuery query)
        {
            PaginationList<AccountDto> dto = await _accountSearch.QueryAsync(query);
            return new ApiResponse<PaginationList<AccountDto>>
            {
                Message = "Query Success",
                Data = dto
            };
        }

        [HttpGet("line-chart")]
        public async Task<ApiResponse<IEnumerable<AccountLineChartDto>>> GetLineChart()
        {
            IEnumerable<AccountLineChartDto> dto = await _accountSearch.GetAccountsAsync();
            return new ApiResponse<IEnumerable<AccountLineChartDto>>
            {
                Message = "Query Success",
                Data = dto
            };
        }
    }
}