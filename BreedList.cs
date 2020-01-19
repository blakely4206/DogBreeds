using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DogBreeds
{
    public class BreedList
    {

        public static Dictionary<string, string> BreedDict = new Dictionary<string, string>();
        public static Dictionary<int, string> BreedByIndex = new Dictionary<int, string>();
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
                    BreedDict.Add(parts[0], parts[1].Replace('_', ' '));
                    BreedByIndex.Add(i, parts[0]);
                    i++;
                }
            }
          
        }
    }
}
