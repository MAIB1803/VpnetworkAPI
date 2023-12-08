using Microsoft.AspNetCore.Mvc;
using VpnetworkAPI.Models;
using VpnetworkAPI.Services;

namespace VpnetworkAPI.Controllers
{
    public class GlobalProgramsController : ControllerBase
    {
        private readonly InterfaceGlobalProgramData _globalProgramService;

        public GlobalProgramsController(InterfaceGlobalProgramData globalProgramService)
        {
            _globalProgramService = globalProgramService;
        }
        [HttpGet("globalprograms")]
        public List<GlobalProgramData> GetGlobalPrograms()
        {
            return _globalProgramService.GetGlobalPrograms();
        }

        [HttpGet("globalprograms/{programName}")]
        public ActionResult<GlobalProgramData> GetGlobalProgramsData(string programName)
        {
            return _globalProgramService.GetGlobalProgramsData(programName);
        }
        [HttpPost("globalprograms")]
        public ActionResult<GlobalProgramData> CreateOrUpdateGlobalProgramData([FromBody] GlobalProgramData globalProgramData)
        {
            return _globalProgramService.CreateOrUpdateGlobalProgramData(globalProgramData);
        }
        


        // Other actions...
    }

}
