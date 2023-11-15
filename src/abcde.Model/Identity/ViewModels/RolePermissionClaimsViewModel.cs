namespace abcde.Model.Identity.ViewModels
{
    public class RolePermissionClaimsViewModel
    {
        public string RoleId { get; set; }
        public List<PermissionClaimViewModel> RoleClaims { get; set; } = new List<PermissionClaimViewModel>();
    }
}
