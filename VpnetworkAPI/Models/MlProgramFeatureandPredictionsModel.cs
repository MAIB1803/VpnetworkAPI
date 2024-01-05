using Microsoft.ML.Data;

namespace VpnetworkAPI.Models
{
    public class ProgramPrediction
    {
        public string ProgramName { get; set; }

        [ColumnName("PredictedLabel")]
        public bool IsHarmful { get; set; }
        [ColumnName("PredictedLabel")]
        public float MemoryUsage { get; set; }
        [ColumnName("PredictedLabel")]
        public float NetworkUsage { get; set; }

        [ColumnName("PredictedLabel")]
        public float SuggestedMemoryThreshold { get; set; }

        [ColumnName("PredictedLabel")]
        public float SuggestedNetworkThreshold { get; set; }
    }

}
