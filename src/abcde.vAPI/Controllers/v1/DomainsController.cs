using abcde.Model;
using abcde.vAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using abcde.Model.Base;
using abcde.Data.Interfaces;
using Serilog;
using FluentValidation;
using abcde.vAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace abcde.vAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class DomainsController : GenericsController<Domain, BaseSummary, BaseFilter>
    {
        private readonly IValidator<Domain> _validator;
        private readonly IDomainService _domainService;

        public DomainsController(IValidator<Domain> validator, IDomainRepository repository, IDomainService domainService) : base(repository)
        {
            _validator = validator;
            _domainService = domainService;
        }

        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] Domain entity)
        {
            try
            {                
                var result = await _validator.ValidateAsync(entity);                

                if(string.IsNullOrEmpty(entity.TenantId) || Guid.Parse(entity.TenantId)==Guid.Empty)
                {
                    return BadRequest("TenantIdIsMissing");
                }

                if (result.IsValid)
                {
                    var domain = await _domainService.GetDomainByNameAsync(entity.Name, Guid.Parse(entity.TenantId));
                    if (domain != null)
                    {
                        return BadRequest("DomainAlreadyExists");
                    }
                    if (!entity.ParentId.HasValue)
                    {
                        var rootDomain = await _domainService.GetRootDomainAsync(Guid.Parse(entity.TenantId));
                        if (rootDomain != null)
                        {
                            entity.ParentId = rootDomain.Id;
                        }
                    }

                    return await base.Post(entity);
                }

                return BadRequest("DomainIsMissingInformationPleaseUpdate");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        public override async Task<IActionResult> GetAll()
        {
            try
            {
                var loggedInUser = await GetClaimValue();
                var data = await _domainService.GetAllDomains(Guid.Parse(loggedInUser));
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        public override async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _domainService.GetDomainAsync(id);
                if (result == null)
                {
                    return BadRequest("DomainNotFound");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
