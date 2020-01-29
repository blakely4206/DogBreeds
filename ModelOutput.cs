using System;
using System.Collections.Generic;
using DogBreeds;
using Microsoft.ML.Data;

namespace DogBreeds
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
