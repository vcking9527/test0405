using ELK.Demo.WebApi.Models;
using ELK.Demo.WebApi.Models.Pagination;

namespace ELK.Demo.WebApi.Dto
{
    public class AccountQuery : PaginationParams
    {

    }

    public class AccountDto
    {
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}