using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    class Dog
    {
        public string Breed { get; set; }
        public string Probability { get; set; } 
        public Dog(string Breed, float Probability)
        {
            this.Breed = BreedList.BreedDict[Breed];
            this.Probability = Convert.ToString(Math.Round(Probability * 100, 2)) + "%";
        }
    }
}
