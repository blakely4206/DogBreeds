using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DogBreeds
{
    public class ImageSetter
    {
        public void image_setter(Image imgPicture, string path)
        {
            imgPicture.Source = new BitmapImage(new Uri(path));
        }

        public void image_setter(ModelInput input, string path)
        {
            input.ImageSource = path;
        }
    }
}
