using Microsoft.ML.Data;

namespace PredictorTP.Entidades
{
    public class PrediccionPolaridad
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
