using Microsoft.AspNetCore.Mvc;
using VpnetworkAPI.Dto;
using VpnetworkAPI.Models;

namespace VpnetworkAPI.Services
{
    public interface InterfaceUser
    {
        ActionResult<UserDto> GetUserByUserId(string userId);
        ActionResult <List<UserDto>> GetUsers();

        ActionResult<User> CreateUser([FromBody] User user);

        ActionResult<User> UpdateUser(string userId, User user);

        ActionResult<List<ProgramDataDto>> GetProgramsByUserId(string userId);

        ActionResult<ProgramData>  DeleteProgramByUseridOrPName(string userId,string programName);
        ActionResult<User> UpdateUserPrograms(string userId, [FromBody] List<ProgramData> programDataList);

        ActionResult<ProgramData> PostProgramData(string userId, [FromBody] ProgramData programData);

        ActionResult<List<LocalProgramData>> GetLocalProgramData(string userId);

        ActionResult<LocalProgramData> GetLocalProgramNameData(string userId, string programName);

        ActionResult<LocalProgramData> CreateOrUpdateLocalProgramData(string userId, [FromBody] LocalProgramData localProgramData);

        ActionResult<ThresholdSettings> GetThresholdTypeSettings(string userId, string programName);
        ActionResult<ThresholdSettings> CreateOrUpdateThresholdTypeSettings(string userId, [FromBody] ThresholdSettingsDto thresholdSettings);


    }
}
