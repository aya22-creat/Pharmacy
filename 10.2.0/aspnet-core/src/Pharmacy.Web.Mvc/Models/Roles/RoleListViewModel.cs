using Pharmacy.Roles.Dto;
using System.Collections.Generic;

namespace Pharmacy.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
