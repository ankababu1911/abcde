namespace abcde.Model.Identity.ViewModels
{
    public class PermissionUsersRolesViewModel
    {
        public string TemplateId { get; set; }

        public List<UserRolePermissionsViewModel> UserClaims { get; set; } = new List<UserRolePermissionsViewModel>();

        public List<UserRolePermissionsViewModel> RoleClaims { get; set; } = new List<UserRolePermissionsViewModel>();
    }

    public class UserRolePermissionsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Access { get; set; }
    }

}
