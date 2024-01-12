using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VpnetworkAPI.Models;
using VpnetworkAPI.Repository;
using VpnetworkAPI.Services;
using VpnetworkAPI.Dto;

namespace VpnetworkAPI.Controllers
{
    public class InsightDataController : Controller
    {
        private readonly InterfaceUser _userRepository;
        public readonly IMapper _map;

        public InsightDataController(InterfaceUser userRepository, IMapper map)
        {
            _map = map;
            _userRepository = userRepository;
        }

        [HttpPost("users")]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = _map.Map<User>(user);
            
            var createdUser = _userRepository.CreateUser(data);
            if (createdUser == null)
            {
                return BadRequest("Unable to create user.");
            }
            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success" });
            //return CreatedAtAction(nameof(GetUserByUserId), new { userId = createdUser}, createdUser);
        }

        [HttpGet("users/{userId}")]
        public ActionResult<UserDto> GetUserByUserId(string userId)
        {
            return _userRepository.GetUserByUserId(userId);
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (users == null)
            {
                return NotFound("No users found.");
            }

            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success" ,data=users.Value });
        }

        [HttpPut("users/{userId}")]
        public IActionResult UpdateUser(string userId, [FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data= _map.Map<User>(user);
            var updatedUser = _userRepository.UpdateUser(userId, data);
            if (updatedUser == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success" ,data= updatedUser.Value});
        }

        [HttpPut("users/{userId}/programs")]
        public IActionResult UpdateUserPrograms(string userId, [FromBody] List<ProgramData> programDataList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.UpdateUserPrograms(userId, programDataList);
            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            return Ok(user);
        }

        [HttpGet("users/{userId}/programs")]
        public ActionResult<ProgramDataDto> GetProgramsByUserId(string userId)
        {
           var data =_userRepository.GetProgramsByUserId(userId);
            return Ok(data);
        }

        [HttpPost("users/{userId}/programs")]
        public ActionResult<ProgramData> PostProgramData(string userId, [FromBody] ProgramDataDto programData)
        {
            var data = _map.Map<ProgramData>(programData);
            return _userRepository.PostProgramData(userId, data);
        }
        [HttpDelete("User/DeleteProgramByUseridOrPName")]
        public ActionResult<ProgramData> PostProgramData(string userId,string programName)
        {
            var data = _userRepository.DeleteProgramByUseridOrPName(userId, programName);
            return data;
        }

        [HttpPost("users/{userId}/thresholdTypeSettings")]
        public ActionResult<ThresholdSettings> CreateOrUpdateThresholdTypeSettings(string userId, [FromBody] ThresholdSettingsDto thresholdSettings)
        {
            return _userRepository.CreateOrUpdateThresholdTypeSettings(userId, thresholdSettings);
        }

        [HttpGet("users/{userId}/thresholdTypeSettings/{programName}")]
        public ActionResult<ThresholdSettingsDto> GetThresholdTypeSettings(string userId, string programName)
        {
             var data = _userRepository.GetThresholdTypeSettings(userId, programName);
            return Ok(new { StatusCode = (int)HttpStatusCode.OK, Message = "Success", data = data.Value });

        }

        [HttpPost("users/{userId}/localProgramData")]
        public ActionResult<LocalProgramData> CreateOrUpdateLocalProgramData(string userId, [FromBody] LocalProgramDataDto localProgramData)
        {
            return _userRepository.CreateOrUpdateLocalProgramData(userId, localProgramData);
        }

        [HttpGet("users/{userId}/localProgramData/{programName}")]
        public ActionResult<LocalProgramDataDto> GetLocalProgramNameData(string userId, string programName)
        {
            return _userRepository.GetLocalProgramNameData(userId, programName);
        }

        [HttpGet("users/{userId}/localProgramData")]
        public ActionResult<List<LocalProgramDataDto>> GetLocalProgramData(string userId)
        {
            return _userRepository.GetLocalProgramData(userId);
        }
    }
}