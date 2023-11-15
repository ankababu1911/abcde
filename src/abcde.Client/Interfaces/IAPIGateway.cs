using abcde.Client.Services.Interfaces;

namespace abcde.Client.Interfaces
{
    public interface IAPIGateway
    {
        public IIdentityService IdentityService { get; }

        public INoteService NoteService { get; }

        public IWorkItemService WorkItemService { get; }

        public IDomainService DomainService { get; }
        string Token { set; }

        void SetToken(string token);

        void SetConnectionStringCode(string value);

        void SetHeaders();
    }
}