using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DogBreeds
{
    public class Prediction
    {
        DogCompare dCompare = new DogCompare();
        public Prediction(float[] probArray)
        {
            for (int i = 0; i < probArray.Length; i++)
            {
                if (probArray[i] > 0.001)
                {
                    addDog(new Dog(BreedList.Return_Breed(i), probArray[i]));
                }
            }

            dogList.Sort(dCompare);
        }

        public List<Dog> dogList = new List<Dog>();

        public void addDog(Dog d)
        {
            dogList.Add(d);
        }

        public List<Dog> getDogList()
        {
            return dogList;
        }
    }
}
