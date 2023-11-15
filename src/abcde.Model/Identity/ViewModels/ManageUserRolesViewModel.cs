namespace abcde.Model.Identity.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public string TenantId { get; set; }
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
