// using Microsoft.ML;
//using Microsoft.ML.Data;
//using Microsoft.ML.Trainers.FastTree;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using VpnetworkAPI.DbContex;
//using Microsoft.EntityFrameworkCore;
//using VpnetworkAPI.Models;
//using System.Collections.Generic; // Needed for List<T>
//using System.Linq; // Needed for LINQ operations

//namespace ModelTrainingServiceNamespace
//{
//    public class ModelTrainingService : IHostedService, IDisposable
//    {
//        private Timer _trainingTimer;
//        private readonly IServiceScopeFactory _scopeFactory;
//        private readonly MLContext _mlContext; // Initialize MLContext

//        public ModelTrainingService(IServiceScopeFactory scopeFactory)
//        {
//            _scopeFactory = scopeFactory;
//            _mlContext = new MLContext(seed: 0); // Instantiate MLContext
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            _trainingTimer = new Timer(TrainModel, null, TimeSpan.Zero, TimeSpan.FromHours(24));
//            return Task.CompletedTask;
//        }

//        private void TrainModel(object state)
//        {
//            using (var scope = _scopeFactory.CreateScope())
//            {
//                var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
//                // Load and transform data from your database
//                var users = dbContext.Users.Include(u => u.Programs).AsNoTracking().ToList();
//                var transformedData = TransformData(users);

//                // Load transformed data into IDataView
//                IDataView dataView = _mlContext.Data.LoadFromEnumerable(transformedData);

//                // Define data transformations
//                var MemoryUsagePipeline = _mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(ProgramData.MemoryUsage))
//                    .Append(_mlContext.Transforms.Concatenate("Features", nameof(ProgramData.MemoryUsage)))
//                    .AppendCacheCheckpoint(_mlContext);

//                // Regression for Memory Usage
//                var usageTrainer = _mlContext.Regression.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features");
//                var memoryModel = MemoryUsagePipeline.Fit(dataView);
//                _mlContext.Model.Save(memoryModel, dataView.Schema, "memoryModel.zip");
//            }
//        }

//        private List<ProgramData> TransformData(IEnumerable<User> users)
//        {
//            var transformedData = new List<ProgramData>();

//            foreach (var user in users)
//            {
//                foreach (var program in user.Programs)
//                {
//                    var model = new ProgramData
//                    {
//                        // Map properties from program to model
//                        MemoryUsage = program.MemoryUsage,
//                        // Add other relevant mappings
//                    };
//                    transformedData.Add(model);
//                }
//            }

//            return transformedData;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _trainingTimer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _trainingTimer?.Dispose();
//        }
//    }
//}
