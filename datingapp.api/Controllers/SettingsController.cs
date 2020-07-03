using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using datingapp.api.Data;
using datingapp.api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace datingapp.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
         private readonly ISettingsRepository _repo;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
       [Authorize(Policy = "RequireAdminRole")]
       [HttpGet("emailtemplates")]
        public async Task<IActionResult> GetEmailTemplates()
        {
           var emailTemplates = await _repo.GetEmailTemplatesAsync();
           var emailTemplatesToReturn = _mapper.Map<IEnumerable<EmailTemplatesForListDto>>(emailTemplates); 


            return Ok(emailTemplatesToReturn);
        }
       
    }
}