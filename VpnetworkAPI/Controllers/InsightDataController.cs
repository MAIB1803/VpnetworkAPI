using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.Models;
using VpnetworkAPI.Repository;
using VpnetworkAPI.Services;

namespace VpnetworkAPI.Controllers
{
    public class InsightDataController : Controller
    {
        private readonly InterfaceUser _userRepository;

        public InsightDataController(InterfaceUser userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("users")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            return _userRepository.CreateUser(user);
        }

        [HttpGet("users/{userId}")]
        public ActionResult<User> GetUserByUserId(string userId)
        {
            return _userRepository.GetUserByUserId(userId);
        }

        [HttpGet("users")]
        public ActionResult<List<User>> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpPut("users/{userId}")]
        public ActionResult<User> UpdateUser(string userId, [FromBody] User user)
        {
            return _userRepository.UpdateUser(userId, user);
        }

        [HttpPut("users/{userId}/programs")]
        public ActionResult<User> UpdateUserPrograms(string userId, [FromBody] List<ProgramData> programDataList)
        {
            return _userRepository.UpdateUserPrograms(userId, programDataList);
        }

        [HttpGet("users/{userId}/programs")]
        public ActionResult<User> GetProgramsByUserId(string userId)
        {
            return _userRepository.GetProgramsByUserId(userId);
        }

        [HttpPost("users/{userId}/programs")]
        public ActionResult<ProgramData> PostProgramData(string userId, [FromBody] ProgramData programData)
        {
            return _userRepository.PostProgramData(userId, programData);
        }

        [HttpPost("users/{userId}/thresholdTypeSettings")]
        public ActionResult<ThresholdSettings> CreateOrUpdateThresholdTypeSettings(string userId, [FromBody] ThresholdSettings thresholdSettings)
        {
            return _userRepository.CreateOrUpdateThresholdTypeSettings(userId, thresholdSettings);
        }

        [HttpGet("users/{userId}/thresholdTypeSettings/{programName}")]
        public ActionResult<ThresholdSettings> GetThresholdTypeSettings(string userId, string programName)
        {
            return _userRepository.GetThresholdTypeSettings(userId, programName);
        }

        [HttpPost("users/{userId}/localProgramData")]
        public ActionResult<LocalProgramData> CreateOrUpdateLocalProgramData(string userId, [FromBody] LocalProgramData localProgramData)
        {
            return _userRepository.CreateOrUpdateLocalProgramData(userId, localProgramData);
        }

        [HttpGet("users/{userId}/localProgramData/{programName}")]
        public ActionResult<LocalProgramData> GetLocalProgramNameData(string userId, string programName)
        {
            return _userRepository.GetLocalProgramNameData(userId, programName);
        }

        [HttpGet("users/{userId}/localProgramData")]
        public ActionResult<List<LocalProgramData>> GetLocalProgramData(string userId)
        {
            return _userRepository.GetLocalProgramData(userId);
        }
    }
}