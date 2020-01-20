using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace DogBreeds
{
    public class BreedList
    {
        static List<Breed> ListOfBreeds = new List<Breed>();
        static BreedList()
        {
            int i = 0;
            string path = AppDomain.CurrentDomain.BaseDirectory + "Breeds.csv";           
                
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    i++;
                    ListOfBreeds.Add(new Breed(i, parts[0], parts[1]));
                }
            }
        }

        public static Breed Return_Breed(int index)
        {            
            return ListOfBreeds[index];
        }

        public static Breed Return_Breed(string key)
        {
            foreach (Breed breedObj in ListOfBreeds)
            {
                if (breedObj.breed_key == key)
                {
                    return breedObj;
                }                
            }
            return null;
        }
    }
}
