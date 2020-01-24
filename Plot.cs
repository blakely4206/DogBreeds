using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    public class Plot
    {
        public double probability { get; }
        public DateTime when { get; }

        public Plot(double prob, DateTime date)
        {
            this.probability = prob;
            this.when = date;
        }
    }
}
