using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELK.Demo.WebApi.Dto;
using ELK.Demo.WebApi.Models;
using ELK.Demo.WebApi.Models.Pagination;
using ELK.Demo.WebApi.Utils;
using Nest;

namespace ELK.Demo.WebApi.Search
{
    public class AccountSearch : IAccountSearch
    {
        private readonly IElasticClient _client;

        public AccountSearch(IElasticClient client)
        {
            _client = client;
        }

        public async Task<PaginationList<AccountDto>> QueryAsync(AccountQuery query)
        {
            int skip = (query.PageNumber - 1) * query.PageSize; // 計算要跳過多少筆資料

            var searchResponse = await _client.SearchAsync<Account>(s => s
                .From(skip)
                .Size(query.PageSize)
                .Sort(s => s.Ascending("account_number"))
            );
            var countResponse = await _client.CountAsync<AccountDto>();

            List<AccountDto> accounts = searchResponse.Documents
                .Select(d => new AccountDto
                {
                    AccountNumber = d.AccountNumber,
                    Name = $"{d.FirstName} {d.LastName}",
                    Email = d.Email,
                    Balance = d.Balance,
                    Age = d.Age,
                    Gender = d.Gender.Equals("M") ? "Male" : "Female"
                })
                .ToList();

            return new PaginationList<AccountDto>(accounts, countResponse.Count, query.PageNumber, query.PageSize);
        }

        public async Task<IEnumerable<AccountLineChartDto>> GetAccountsAsync()
        {
            var response = await _client.SearchAsync<Account>(s => s
                .Size(0)
                .Aggregations(a => a
                    .Range("age_buckets", r => r
                        .Field(f => f.Age)
                        .Ranges(
                            r => r.AgeFrom(0, 10),
                            r => r.AgeFrom(11, 20),
                            r => r.AgeFrom(21, 30),
                            r => r.AgeFrom(31, 40),
                            r => r.AgeFrom(41, 50),
                            r => r.AgeFrom(51, 60),
                            r => r.AgeFrom(61, 70),
                            r => r.AgeFrom(71, 80),
                            r => r.AgeFrom(81, 90),
                            r => r.AgeFrom(91, 100)
                        )
                        .Aggregations(aa => aa
                            .Average("average_balance", avg => avg
                                .Field(f => f.Balance)
                            )
                        )
                    )
                )
            );

            var ageBuckets = response.Aggregations.Range("age_buckets");
            return ageBuckets.Buckets.Select(b =>
            {
                var ageRange = b.Key;
                var averageBalance = b.Average("average_balance").Value;
                return new AccountLineChartDto
                {
                    AgeRange = ageRange,
                    AverageBalance = averageBalance ?? 0
                };
            })
            .OrderBy(d => d.AgeRange)
            .ToList();
        }
    }
}