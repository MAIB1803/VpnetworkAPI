using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VpnetworkAPI.DbContex;
using VpnetworkAPI.Models;
using VpnetworkAPI.Services;

namespace VpnetworkAPI.Repository
{
    public class UserRepository : InterfaceUser
    {
        private readonly UserDbContext dbContext;
        public UserRepository(UserDbContext db)
        {
            dbContext = db;
        }

        public ActionResult<LocalProgramData> CreateOrUpdateLocalProgramData(string userId, [FromBody] LocalProgramData localProgramData)
        {
            if (localProgramData == null)
            {
                return new BadRequestObjectResult("LocalProgramData is invalid.");
            }

            var user = dbContext.Users
                .Include(u => u.LocalProgramData)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var existingLocalProgramData = user.LocalProgramData.FirstOrDefault(p => p.ProgramName == localProgramData.ProgramName);
            if (existingLocalProgramData == null)
            {
                // Check if a setting with the same name already exists
                var duplicateProgram = user.LocalProgramData.FirstOrDefault(p => p.ProgramName == localProgramData.ProgramName);
                if (duplicateProgram != null)
                {
                    // Program setting with the same name already exists, return a conflict response
                    return new ConflictObjectResult("Program setting with the same name already exists.");
                }

                // Add the new local program data
                user.LocalProgramData.Add(localProgramData);
                dbContext.SaveChanges();
                return new OkObjectResult("New local program data added for the existing user.");
            }
            else
            {
                // Update existing local program data
                existingLocalProgramData.ProgramLocalMemoryThreshold = localProgramData.ProgramLocalMemoryThreshold;
                existingLocalProgramData.ProgramLocalNetworkThreshold = localProgramData.ProgramLocalNetworkThreshold;
                dbContext.SaveChanges();
                return new OkObjectResult("Local program data updated.");
            }
        }

        public ActionResult<ThresholdSettings> CreateOrUpdateThresholdTypeSettings(string userId, [FromBody] ThresholdSettings thresholdSettings)
        {
            if (thresholdSettings == null)
            {
                return new BadRequestObjectResult("ThresholdSettings data is invalid.");
            }

            var user = dbContext.Users
                .Include(u => u.ThresholdSettings)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var existingThresholdSettings = user.ThresholdSettings.FirstOrDefault(p => p.ProgramName == thresholdSettings.ProgramName);
            if (existingThresholdSettings == null)
            {
                // Check if a setting with the same name already exists
                var duplicateThresholdSetting = user.ThresholdSettings.FirstOrDefault(p => p.ProgramName == thresholdSettings.ProgramName);
                if (duplicateThresholdSetting != null)
                {
                    // Threshold setting with the same name already exists, return a conflict response
                    return new ConflictObjectResult("Threshold setting with the same name already exists.");
                }
                user.ThresholdSettings.Add(thresholdSettings);
                dbContext.SaveChanges();
                return new OkObjectResult("New threshold setting data added for the existing user.");
            }
            else
            {
                // Update existing threshold setting data
                existingThresholdSettings.ThresholdSetting = thresholdSettings.ThresholdSetting;
                dbContext.SaveChanges();
                return new OkObjectResult("Threshold setting data updated.");
            }
        }
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return new BadRequestObjectResult("User data is invalid.");
            }

            try
            {
                // Check if a user with the same UserId already exists
                var existingUser = dbContext.Users.FirstOrDefault(u => u.UserId == user.UserId);

                if (existingUser != null)
                {
                    return new ConflictObjectResult("User with the same UserId already exists.");
                }

                // Add the new user
                dbContext.Users.Add(user);
                dbContext.SaveChanges();

                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                // Log the exception or return the details in the response for debugging
                return new BadRequestObjectResult($"Failed to create user. Error: {ex.Message}");
            }
        }


        public ActionResult<List<LocalProgramData>> GetLocalProgramData(string userId)
        {
            throw new NotImplementedException();
        }

        public ActionResult<User> GetProgramsByUserId(string userId)
        {
            var user = dbContext.Users
             .Include(u => u.ProgramData)
             .FirstOrDefault(u => u.UserId == userId);
                    if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            return new OkObjectResult(user.ProgramData);
        }

        public ActionResult<ThresholdSettings> GetThresholdTypeSettings(string userId, string programName)
        {
            var user = dbContext.Users
            .Include(u => u.ThresholdSettings)
            .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var thresholdSetting = user.ThresholdSettings
                .FirstOrDefault(t => t.ProgramName == programName);

            if (thresholdSetting == null)
            {
                return new NotFoundObjectResult($"Threshold setting not found for program: {programName}");
            }

            return new OkObjectResult(thresholdSetting);
        }

        public ActionResult<User> GetUserByUserId(string userId)
        {
            var user = dbContext.Users
            .Include(u => u.ProgramData)
            .Include(u => u.LocalProgramData)
            .Include(u => u.ThresholdSettings)
            .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            return new OkObjectResult(user);
        }

        public ActionResult<List<User>> GetUsers()
        {
            var users = dbContext.Users
           .Include(u => u.ProgramData)
           .Include(u => u.LocalProgramData)
           .Include(u => u.ThresholdSettings)
           .ToList();

            return users;
        }

        public ActionResult<LocalProgramData> GetLocalProgramNameData(string userId, string programName)
        {
            var user = dbContext.Users
                .Include(u => u.LocalProgramData)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var localProgram = user.LocalProgramData.FirstOrDefault(p => p.ProgramName == programName);
            if (localProgram == null)
            {
                return new NotFoundObjectResult($"Local program with name {programName} not found for user {userId}");
            }

            return new OkObjectResult(localProgram);
        }


        public ActionResult<ProgramData> PostProgramData(string userId, [FromBody] ProgramData programData)
        {
            if (programData == null)
            {
                return new BadRequestObjectResult("ProgramData is invalid.");
            }

            var user = dbContext.Users
                .Include(u => u.ProgramData)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var existingProgram = user.ProgramData.FirstOrDefault(p => p.ProgramName == programData.ProgramName);
            if (existingProgram != null)
            {
                // Update existing program data
                existingProgram.MemoryUsage = programData.MemoryUsage;
                existingProgram.NetworkUsage = programData.NetworkUsage;
                existingProgram.ProgramBadCount = programData.ProgramBadCount;
                dbContext.SaveChanges();
                return new OkObjectResult("Program data updated.");
            }
            else
            {
                // Add new program data
                dbContext.ProgramData.Add(programData);
                dbContext.SaveChanges();
                return new OkObjectResult("New program added for the existing user.");
            }
        }


        public ActionResult<User> UpdateUser([FromBody] string userId,  User updatedUser)
        {
            var existingUser = dbContext.Users
                .Include(u => u.ProgramData)
                .Include(u => u.LocalProgramData)
                .Include(u => u.ThresholdSettings)
                .FirstOrDefault(u => u.UserId == userId);

            if (existingUser == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            dbContext.SaveChanges();
            return new OkObjectResult(existingUser);
        }


        public ActionResult<User> UpdateUserPrograms(string userId, [FromBody] List<ProgramData> programDataList)
        {
            var user = dbContext.Users
                .Include(u => u.ProgramData)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            foreach (var programData in programDataList)
            {
                var existingProgram = user.ProgramData.FirstOrDefault(p => p.ProgramName == programData.ProgramName);
                if (existingProgram != null)
                {
                    // Update existing program data
                    existingProgram.MemoryUsage = programData.MemoryUsage;
                    existingProgram.NetworkUsage = programData.NetworkUsage;
                    existingProgram.ProgramBadCount += programData.ProgramBadCount;
                }
                else
                {
                    // Add the new program data
                    user.ProgramData.Add(programData);
                }
            }

            dbContext.SaveChanges();
            return new OkObjectResult("Program data updated successfully");
        }
       
    }
}
