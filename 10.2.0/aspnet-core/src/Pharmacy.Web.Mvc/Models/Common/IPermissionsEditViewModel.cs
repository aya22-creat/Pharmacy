using Pharmacy.Roles.Dto;
using System.Collections.Generic;

namespace Pharmacy.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}