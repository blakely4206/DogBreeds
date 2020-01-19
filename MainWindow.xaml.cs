using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DogBreeds
{
    public partial class MainWindow : Window
    {
        List<Dog> dList = new List<Dog>();
        DogCompare dCompare = new DogCompare();
        ListPopulate lPopulate = new ListPopulate();

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void btnPredict_Click(object sender, RoutedEventArgs e)
        {            
            var input = new ModelInput();
            string path = FileNameGetter.return_filename();
            input.ImageSource = path;
            imgPicture.Source = new BitmapImage(new Uri(path));

            ModelOutput prediction = ConsumeModel.Predict(input);

            lPopulate.populateList(prediction.Score, dList);

            theGrid.ItemsSource = dList;
        }        
    }
}
