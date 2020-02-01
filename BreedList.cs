using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace DogBreeds
{
    public class BreedList
    {
        static List<Breed> ListOfBreeds = new List<Breed>();
        static BreedList()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Breeds.csv";
            
            if(File.Exists(path))
            {
                loadList(path);
            }
            else
            {
               MessageBoxResult x = MessageBox.Show("Select Breed.csv ", "Missing Breed List File", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                if(x == MessageBoxResult.OK)
                {
                    loadList(FileNameGetter.returnCSV_filename());

                    
                }
                else if(x == MessageBoxResult.Cancel)
                {
                    System.Environment.Exit(0);
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

        private static void loadList(string path)
        {
            int i = 0;
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
    }
}
