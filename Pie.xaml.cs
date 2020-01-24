using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DogBreeds
{
    /// <summary>
    /// Interaction logic for Pie.xaml
    /// </summary>
    public partial class Pie : Window
    {
        public Pie()
        {
            InitializeComponent();

        }

        public PlotModel PieModel { get; set; }

        public ObservableCollection<ContinentItem> Continents { get; private set; }
    }

    public class ContinentItem
    {
        public string Name { get; set; }

        public double PopulationInMillions { get; set; }

        public bool IsExploded { get; set; }

    }

}

    

