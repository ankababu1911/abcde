namespace abcde.Model.Identity
{
    public class AddTmUserRequest
    {
        public Guid TmPortalUserId { get; set; }
        public Guid OrganisationId { get; set; }
        public string TmUserName { get; set; }
    }
}