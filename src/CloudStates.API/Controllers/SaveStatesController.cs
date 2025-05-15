using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudStates.API.Dtos;
using CloudStates.API.Services;

namespace CloudStates.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaveStatesController(ISaveStateService _saveStateService) : ControllerBase
    {
        [Authorize]
        [HttpGet("upload-url")]
        [ProducesResponseType(typeof(SaveStateUploadUrlResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SaveStateUploadUrlResponse>> GetUploadUrlAsync()
        {
            return Ok(await _saveStateService.GetUploadUrlAsync());
        }
    }
}
