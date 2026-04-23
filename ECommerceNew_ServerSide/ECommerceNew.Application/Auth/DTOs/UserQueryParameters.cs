using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceNew.Application.Auth.DTOs
{
    public class UserQueryParameters
    {
        public string? Search { get; set; }
        public bool? IsActive { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
