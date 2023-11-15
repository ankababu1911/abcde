using abcde.Model;
using abcde.Test.Data.Base;

namespace abcde.Test.Data
{
    public class TenantData : BaseData
    {
        public static Tenant Get()
        {
            return new Tenant
            {
                IsActive = true,
                Name = "New Tenant",
                Domain = "New Tenant Domain",
                TenantId = Guid.NewGuid().ToString(),
                Created = DateTime.Now.AddDays(-30),
                Datestamp = DateTime.Now
            };
        }

        public static IEnumerable<Tenant> GetEnumerable()
        {
            return new List<Tenant>()
            {
                new Tenant()
                {
                    TenantId = Guid.NewGuid().ToString(),
                    Name = "The University of A",
                    Domain = tenant1domain,
                    IsActive = true,
                    Created = DateTime.Now.AddDays(-30),
                    Datestamp = DateTime.Now
                },
                new Tenant()
                {
                    TenantId = Guid.NewGuid().ToString(),
                    Name = "The University of B",
                    Domain = tenant2domain,
                    IsActive = true,
                    Created = DateTime.Now.AddDays(-60),
                    Datestamp = DateTime.Now,
                },
                new Tenant()
                {
                    TenantId = Guid.NewGuid().ToString(),
                    Name = "The University with custom DB",
                    Domain = tenant2domain,
                    IsActive = true,
                    Created = DateTime.Now.AddDays(-60),
                    Datestamp = DateTime.Now,
                    ConnectionStringCode = "AB56D4"
                }
            };
        }
    }
}