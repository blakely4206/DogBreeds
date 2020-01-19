using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    class ListPopulate
    {
        DogCompare dCompare = new DogCompare();
        public void populateList(float[] probArray, List<Dog> dogList)
        {

            for (int i = 0; i < probArray.Length; i++)
            {
                dogList.Add(new Dog(BreedList.BreedByIndex[i], probArray[i]));
            }

            dogList.Sort(dCompare);
        }
    }
}
