using HealthSysHub.Web.API.CustomFilters;
using HealthSysHub.Web.DBConfiguration.Models;
using HealthSysHub.Web.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSysHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [HealthSysHubAutherize]
    public class WardController : ControllerBase
    {
        private readonly IWardManager _wardManager;

        public WardController(IWardManager wardManager)
        {
            _wardManager = wardManager;
        }

        [HttpGet]
        [Route("GetAllWardsAsync")]
        public async Task<IActionResult> GetAllWardsAsync()
        {
            try
            {
                var response = await _wardManager.GetAllWardsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetActiveWardsAsync")]
        public async Task<IActionResult> GetActiveWardsAsync()
        {
            try
            {
                var response = await _wardManager.GetActiveWardsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetWardByIdAsync/{wardId}")]
        public async Task<IActionResult> GetWardByIdAsync(Guid wardId)
        {
            try
            {
                var response = await _wardManager.GetWardByIdAsync(wardId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetWardsByHospitalIdAsync/{hospitalId}")]
        public async Task<IActionResult> GetWardsByHospitalIdAsync(Guid hospitalId)
        {
            try
            {
                var response = await _wardManager.GetWardsByHospitalIdAsync(hospitalId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("InsertOrUpdateWardAsync")]
        public async Task<IActionResult> InsertOrUpdateWardAsync([FromBody] Ward ward)
        {
            try
            {
                var response = await _wardManager.InsertOrUpdateWardAsync(ward);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteWardAsync/{wardId}")]
        public async Task<IActionResult> DeleteWardAsync(Guid wardId)
        {
            try
            {
                var result = await _wardManager.DeleteWardAsync(wardId);
                if (result)
                {
                    return NoContent(); // 204 No Content
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
