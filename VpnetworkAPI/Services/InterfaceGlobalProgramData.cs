using Microsoft.AspNetCore.Mvc;
using VpnetworkAPI.Models;

namespace VpnetworkAPI.Services
{
    public interface InterfaceGlobalProgramData
    {
        List<GlobalProgramData>  GetGlobalPrograms();
        ActionResult<GlobalProgramData> CreateOrUpdateGlobalProgramData([FromBody] GlobalProgramData globalProgramData);

        ActionResult<GlobalProgramData> GetGlobalProgramsData(string programName);
    }
}
