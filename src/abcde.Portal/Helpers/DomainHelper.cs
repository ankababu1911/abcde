using System.Linq;
using System.Text;
using abcde.Model;
using abcde.Model.Identity;
using abcde.Portal.ViewModels;

namespace abcde.Portal.Helpers
{
    public static class DomainHelper
    {
        public static DomainViewModel GetDomainViewModel(Domain domain, List<ApplicationUser> tenantUsers)
        {
            StringBuilder sbDomainHeads = new StringBuilder();
            if (domain.DomainUsers != null)
            {
                foreach (var domainUser in domain.DomainUsers.Where(x => x.IsDomainHead))
                {
                    if (tenantUsers.Any(x => x.Id == domainUser.UserID))
                    {
                        var user = tenantUsers.FirstOrDefault(x => x.Id == domainUser.UserID);
                        var headUserName = !string.IsNullOrWhiteSpace(user.Fullname) ? user.Fullname : user.Email;
                        if (!string.IsNullOrEmpty(headUserName))
                        {
                            sbDomainHeads.Append(headUserName);
                            sbDomainHeads.Append(", ");
                        }
                    }
                }
            }
            var domaiinHeads = sbDomainHeads.ToString().Trim();
            if (domaiinHeads.EndsWith(","))
            {
                //remove end comma
                domaiinHeads = domaiinHeads.Remove(domaiinHeads.Length - 1);
            }
            return new DomainViewModel
            {
                Id = domain.Id,
                Name = domain.Name,
                DomainHead = domaiinHeads,
                UsersCount = domain.DomainUsers != null
                    ? domain.DomainUsers.Count
                    : 0
            };
        }
    }
}