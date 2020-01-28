using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    public class BarColumn
    {
        public string breedLabel { get; set; }
        public int breedFreq { get; set; }

        public BarColumn(string label)
        {
            this.breedLabel = label;
            this.breedFreq = 1;
        }
    }
}
