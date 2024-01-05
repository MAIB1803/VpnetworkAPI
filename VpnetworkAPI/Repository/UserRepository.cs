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
                .Include(u => u.UserSettings.LocalProgramSettings)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var existingLocalProgramData = user.UserSettings.LocalProgramSettings.FirstOrDefault(p => p.ProgramName == localProgramData.ProgramName);
            if (existingLocalProgramData == null)
            {
                // Check if a setting with the same name already exists
                var duplicateProgram = user.UserSettings.LocalProgramSettings.FirstOrDefault(p => p.ProgramName == localProgramData.ProgramName);
                if (duplicateProgram != null)
                {
                    // Program setting with the same name already exists, return a conflict response
                    return new ConflictObjectResult("Program setting with the same name already exists.");
                }

                // Add the new local program data
                user.UserSettings.LocalProgramSettings.Add(localProgramData);
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
                .Include(u => u.UserSettings.ThresholdtypeSettings)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var existingThresholdSettings = user.UserSettings.ThresholdtypeSettings.FirstOrDefault(p => p.ProgramName == thresholdSettings.ProgramName);
            if (existingThresholdSettings == null)
            {
                // Check if a setting with the same name already exists
                var duplicateThresholdSetting = user.UserSettings.ThresholdtypeSettings.FirstOrDefault(p => p.ProgramName == thresholdSettings.ProgramName);
                if (duplicateThresholdSetting != null)
                {
                    // Threshold setting with the same name already exists, return a conflict response
                    return new ConflictObjectResult("Threshold setting with the same name already exists.");
                }

                // Add the new threshold setting data
                user.UserSettings.ThresholdtypeSettings.Add(thresholdSettings);
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
                var existingUser = dbContext.Users
                    .FirstOrDefault(u => u.UserId == user.UserId);

                if (existingUser != null)
                {
                    return new ConflictObjectResult("User with the same UserId already exists.");
                }

                // Ensure Programs and UserSettings are initialized
                user.Programs ??= new List<ProgramData>();
                user.UserSettings ??= new User.Settings
                {
                    LocalProgramSettings = new List<LocalProgramData>(),
                    ThresholdtypeSettings = new List<ThresholdSettings>()
                };

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
             .Include(u => u.Programs)
             .FirstOrDefault(u => u.UserId == userId);
                    if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            return new OkObjectResult(user.Programs);
        }

        public ActionResult<ThresholdSettings> GetThresholdTypeSettings(string userId, string programName)
        {
            var user = dbContext.Users
            .Include(u => u.UserSettings.ThresholdtypeSettings)
            .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var thresholdSetting = user.UserSettings.ThresholdtypeSettings
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
            .Include(u => u.Programs)
            .Include(u => u.UserSettings.LocalProgramSettings)
            .Include(u => u.UserSettings.ThresholdtypeSettings)
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
           .Include(u => u.Programs)
           .Include(u => u.UserSettings.LocalProgramSettings)
           .Include(u => u.UserSettings.ThresholdtypeSettings)
           .ToList();

            return new OkObjectResult(users);
        }

        public ActionResult<LocalProgramData> GetLocalProgramNameData(string userId, string programName)
        {
            var user = dbContext.Users
             .Include(u => u.UserSettings.LocalProgramSettings)
             .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var localProgram = user.UserSettings.LocalProgramSettings.FirstOrDefault(p => p.ProgramName == programName);

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

            var existingUser = dbContext.Users
                        .Include(u => u.Programs)
                        .FirstOrDefault(u => u.UserId == userId);
            if (existingUser == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            var existingProgram = existingUser.Programs.FirstOrDefault(p => p.ProgramName == programData.ProgramName);
            if (existingProgram != null)
            {
                // Program already exists, update data
                existingProgram.MemoryUsage = programData.MemoryUsage;
                existingProgram.NetworkUsage = programData.NetworkUsage;
                existingProgram.ProgramBadCount = programData.ProgramBadCount;
                dbContext.SaveChanges();
                return new OkObjectResult("Program data updated.");
            }
            else
            {
                // Program doesn't exist, add new program
                existingUser.Programs.Add(programData);
                dbContext.SaveChanges();
                return new OkObjectResult("New program added for the existing user.");
            }
        }

        public ActionResult<User> UpdateUser(string userId, User user)
        {
            var existingUser = dbContext.Users
             .Include(u => u.Programs)
             .Include(u => u.UserSettings.LocalProgramSettings)
             .Include(u => u.UserSettings.ThresholdtypeSettings)
             .FirstOrDefault(u => u.UserId == userId);

            if (existingUser == null)
            {
                return new NotFoundObjectResult($"Exesting User {userId}");
            }

            existingUser.Programs = user.Programs;
            existingUser.UserSettings = user.UserSettings;
            dbContext.SaveChanges();

            return new OkObjectResult("User Data Updated");
        }

        public ActionResult<User> UpdateUserPrograms(string userId, [FromBody] List<ProgramData> programDataList)
        {
            var user = dbContext.Users
             .Include(u => u.Programs)
             .FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            foreach (var programData in programDataList)
            {

                var existingProgram = user.Programs.FirstOrDefault(p => p.ProgramName == programData.ProgramName);
                if (existingProgram != null)
                {
                    // Update existing program data
                    existingProgram.MemoryUsage = programData.MemoryUsage;
                    existingProgram.NetworkUsage = programData.NetworkUsage;
                    existingProgram.ProgramBadCount += programData.ProgramBadCount;
                    dbContext.SaveChanges();
                }
                else
                {
                    // Add the new program data
                    user.Programs.Add((programData));
                    dbContext.SaveChanges();
                }
            }

            return new OkObjectResult("Program data updated successfully");
        }
    }
}
