using Pharmacy.Roles.Dto;
using System.Collections.Generic;

namespace Pharmacy.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
