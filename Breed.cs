using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    public class Breed
    {
        public int breed_index { get; }
        public string breed_key { get; }
        public string breed_name { get; }        

        public Breed(int i, string key, string name)
        {
            this.breed_index = i;
            this.breed_key = key;
            this.breed_name = name.Replace('_', ' ');
        }
    }
}
