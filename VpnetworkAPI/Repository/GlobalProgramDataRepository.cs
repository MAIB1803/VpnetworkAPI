using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex;
using VpnetworkAPI.Models;
using VpnetworkAPI.Services;

namespace VpnetworkAPI.Repository
{
    public class GlobalProgramDataRepository : InterfaceGlobalProgramData
    {
        private static List<GlobalProgramData> GlobalProgramList = new List<GlobalProgramData>();
        public readonly UserDbContext dbContext;
        public GlobalProgramDataRepository(UserDbContext db)
        {
            dbContext = db;
        }
        public ActionResult<GlobalProgramData> CreateOrUpdateGlobalProgramData([FromBody] GlobalProgramData globalProgramData)
        {
            if (globalProgramData == null)
            {
                return new BadRequestObjectResult("GlobalProgramData is invalid.");
            }

            var existingGlobalProgramData = GlobalProgramList.FirstOrDefault(p => p.ProgramName == globalProgramData.ProgramName);
            if (existingGlobalProgramData == null)
            {
                // Check if a program with the same name already exists
                var duplicateProgram = dbContext.GlobalProgramData.FirstOrDefault(p => p.ProgramName == globalProgramData.ProgramName);
                if (duplicateProgram != null)
                {
                    // Program with the same name already exists, return a conflict response
                    return new ConflictObjectResult("Program with the same name already exists.");
                }
                else
                {
                    // Add the new global program data
                    //GlobalProgramList.Add(globalProgramData);
                    var res = dbContext.Add(globalProgramData);
                    dbContext.SaveChanges();
                    return new OkObjectResult("New global program data added.");
                }

                
            }
            else
            {
                // Update existing global program data
                existingGlobalProgramData.ProgramGLobalMemoryThreshold = globalProgramData.ProgramGLobalMemoryThreshold;
                existingGlobalProgramData.ProgramGlobalNetworkThreshold = globalProgramData.ProgramGlobalNetworkThreshold;
                return new OkObjectResult("Global program data updated.");
            }
        }

        public List<GlobalProgramData> GetGlobalPrograms()
        {
            var globalPrograms = dbContext.GlobalProgramData.ToList();
            return globalPrograms;
        }

        public ActionResult<GlobalProgramData> GetGlobalProgramsData(string programName)
        {
            var globalProgram = dbContext.GlobalProgramData.FirstOrDefault(p => p.ProgramName == programName);

            if (globalProgram == null)
            {
                // throw new Exception($"Global program with name {programName} not found.");
                return new NotFoundObjectResult($"Global program with name {programName} notfound.");
            }

            return new OkObjectResult(globalProgram);
        }

       
    }
}
