using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudStates.API.Dtos;
using CloudStates.API.Extensions;
using CloudStates.API.Services;

namespace CloudStates.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaveStatesController(
        ISaveStateService _saveStateService
        ) : ControllerBase
    {
        [Authorize]
        [HttpGet("upload-url")]
        [ProducesResponseType(typeof(SaveStateUploadUrlResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SaveStateUploadUrlResponse>> GetUploadUrlAsync()
        {
            return Ok(await _saveStateService.GetUploadUrlAsync());
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(SaveStateStoreResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SaveStateStoreResponse>> StoreAsync([FromBody] SaveStateStoreRequest request)
        {
            return Ok(await _saveStateService.StoreAsync(request, User.GetUserId()));
        }

        [Authorize]
        [HttpGet("latest/download-url")]
        [ProducesResponseType(typeof(SaveStateDownloadUrlResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SaveStateDownloadUrlResponse>> GetLatestDownloadUrlAsync([FromQuery] SaveStateLatestRequest request)
        {
            return Ok(await _saveStateService.GetLatestDownloadUrlAsync(request, User.GetUserId()));
        }

        [Authorize]
        [HttpGet("latest")]
        public async Task<ActionResult<SaveStateResponse>> GetLatestAsync([FromQuery] SaveStateLatestRequest request)
        {
            return Ok(await _saveStateService.GetLatestAsync(request, User.GetUserId()));
        }
    }
}
