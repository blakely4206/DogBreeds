using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    class Dog
    {
        Breed Breed { get; }
        double doubleProbability { get; }
        public string Breed_Name
        {
            get
            {
                return this.Breed.breed_name;
            }            
        }
    
        public string Probability { get; set; } 
        public Dog(Breed breed, float Probability)
        {
            this.Breed = breed;
            this.doubleProbability = Probability;
            this.Probability = Convert.ToString(Math.Round(Probability * 100, 2)) + "%";
        }

        public string GetBreedKey()
        {
            return Breed.breed_key;
        }

        public double GetProbability()
        {
            return doubleProbability;
        }
    }
}
