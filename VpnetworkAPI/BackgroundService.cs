using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using VpnetworkAPI.Dto;
using VpnetworkAPI.Models;


namespace BackgroundServiceWorker
{
  public class BackgroundServices : BackgroundService
    {
        private readonly ILogger<BackgroundServices> _logger;
        private Timer timer;
        private string apiUrl = "https://localhost:7177/";
        private string userId = "g"; // Replace with dynamic user
        private List<ProgramDataDto> allProgramData;
        private List<Analysis> analysisData;

        private const double DefaultLocalMemoryThreshold = 100000; // Replace with your default value
        private const double DefaultLocalNetworkThreshold = 10000; // Replace with your default value

        // Define memory and network speed threshold

        public BackgroundServices(ILogger<BackgroundServices> logger)
        {
            _logger = logger;
            allProgramData = new List<ProgramDataDto>();
            analysisData = new List<Analysis>();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(TimerElapsed, null, 0, 60000); // Run every 60 seconds 
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

        private async void TimerElapsed(object state)
        {
            try  
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    try
                    {
                        double MemoryThreshold = 0.0;
                        double NetworkThreshold = 0.0;
                        string processName = process.ProcessName;
                        int pid = process.Id;
                        long memoryUsage = process.WorkingSet64 / 1024;
                        double networkSpeed = GetNetworkSpeed(processName);

                        ThresholdSettings thresholdSetting = GetThresholdTypeSettings(userId, processName);
                         
                        if (thresholdSetting != null)
                        {
                            Console.WriteLine("We are at the thresholdSetting");
                            switch (thresholdSetting.ThresholdSetting.ToLower())
                            {
                                case "local":
                                    // Check for local settings
                                    LocalProgramData localSettings = GetLocalThresholdSettings(userId, processName);

                                    if (localSettings == null)
                                    {
                                        // Prompt user to add local setting if not present
                                        _logger.LogWarning($"Local threshold setting not found for {processName}. Please add the setting manually.");
                                    }
                                    else
                                    {

                                        MemoryThreshold = localSettings.ProgramLocalMemoryThreshold;
                                        NetworkThreshold = localSettings.ProgramLocalNetworkThreshold;
                                    }
                                    // Switch to local settings
                                    break;

                                case "global":
                                    // Check for global settings
                                    GlobalProgramData globalDataSetting = GetGlobalThresholdSettings(processName);

                                    if (globalDataSetting == null)
                                    {
                                        AddGlobalThresholdSetting(processName);
                                        globalDataSetting = GetGlobalThresholdSettings(processName);
                                    }

                                    MemoryThreshold = globalDataSetting.ProgramGLobalMemoryThreshold;
                                    NetworkThreshold = globalDataSetting.ProgramGlobalNetworkThreshold;
                                    break;

                                default:
                                    // If no threshold setting found, use default values
                                    MemoryThreshold = DefaultLocalMemoryThreshold;
                                    NetworkThreshold = DefaultLocalNetworkThreshold;
                                    break;
                            }
                        }

                        if (memoryUsage > MemoryThreshold || networkSpeed > NetworkThreshold)
                        {
                            ProgramDataDto existingProgram = allProgramData.Find(p => p.ProgramName == processName);
                            if (existingProgram != null)
                            {
                                existingProgram.ProgramBadCount++;
                                existingProgram.MemoryUsage += memoryUsage;
                                existingProgram.NetworkUsage += networkSpeed;
                                Console.WriteLine($"oiee this fellow {processName} will be added");
                            }
                            else
                            {
                                Console.WriteLine($"oiee this is {processName} the new fellow");
                                ProgramDataDto programData = new ProgramDataDto
                                {
                                    ProgramDataId = new Guid(),
                                    ProgramName = processName,
                                    MemoryUsage = memoryUsage,
                                    NetworkUsage = networkSpeed,
                                    ProgramBadCount = 1
                                };

                                allProgramData.Add(programData);
                            }
                            var analysis = new Analysis
                            {
                                UserId = userId,
                                DateTime = DateTime.Now,
                                ProgramName = processName,
                                MemoryUsage = memoryUsage,
                                NetworkUsage = networkSpeed
                            };
                            // Add the analysis data to the list
                            analysisData.Add(analysis);

                            // Send the analysis to the API
                            SendAnalysisToApi(analysis);

                        }
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError($"Error retrieving information for {process.ProcessName}: {exception.Message}");
                    }
                }

                User existingUser = GetExistingUserFromApi(userId);

                //var existingProgramData = GetProgramDataFromApi(userId);

                List<ProgramDataDto> existingProgramData = GetProgramDataFromApi(userId);

                if (existingProgramData != null)
                {
                    Console.WriteLine("Hey we are checking existing user here");

                    foreach (ProgramDataDto programData in allProgramData)
                    {
                        ProgramDataDto existingProgram = existingProgramData.FirstOrDefault(p => p.ProgramName == programData.ProgramName);

                        if (existingProgram != null)
                        {
                            Console.WriteLine("Hey we are checking existing program here");
                            existingProgram.MemoryUsage = programData.MemoryUsage;
                            existingProgram.ProgramBadCount += programData.ProgramBadCount;
                        }
                        else
                        {
                            Console.WriteLine($"Hey we are Add {programData.ProgramName} existing user here");
                            existingProgramData.Add(programData);
                        }
                    }
                    Console.WriteLine("Hey this is existing p data" + existingProgramData);


                    allProgramData.Clear();
                    allProgramData = existingProgramData;
                    UpdateProgramDataToApi(userId, existingProgramData);
                }
                else
                {
                    UserDto newUser = new UserDto
                    {
                        UserId = userId,
                        //programs = allProgramData
