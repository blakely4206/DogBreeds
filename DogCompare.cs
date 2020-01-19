using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreeds
{
    public class DogCompare : IComparer<Dog>
    {
        int IComparer<Dog>.Compare(Dog x, Dog y)
        {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (y == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = y.Probability.CompareTo(x.Probability);

                        if (retval != 0)
                        {
                            return retval;
                        }
                        else
                        {
                            return y.Probability.CompareTo(x.Probability);
                        }
                    }
                }
            }
        }
    }
    

