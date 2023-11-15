namespace abcde.Model.Identity.ViewModels
{
    public class UserPermissionClaimsViewModel
    {
        public string UserId { get; set; }
        public List<PermissionClaimViewModel> UserClaims { get; set; } = new List<PermissionClaimViewModel>();
    }

}
