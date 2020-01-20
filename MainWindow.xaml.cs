using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using OxyPlot;
using OxyPlot.Series;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot.Wpf;

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
            LoadBarGraph();

            DataContext = this;

            var s1 = new OxyPlot.Wpf.LineSeries

            {
                StrokeThickness = 0,
                MarkerSize = 3,
               // MarkerStroke = OxyColors.ForestGreen,
                MarkerType = MarkerType.Plus
            };

            //          foreach (var pt in Fern.Generate(2000))
            //         {
            //             s1.Points.Add(new DataPoint(pt.X, -pt.Y));
            //         }

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
            LoadBarGraph();
            LogWriter.Log(dList);

        }        

        private void LoadBarGraph()
        {
            ///OxyPlot.Series.BarItem item1 = new OxyPlot.Series.BarItem(50);
            //OxyPlot.Series.BarItem item2 = new OxyPlot.Series.BarItem(20);
          //  OxyPlot.Series.BarItem item3 = new OxyPlot.Series.BarItem(12);


            OxyPlot.Wpf.BarSeries bar_graph = new OxyPlot.Wpf.BarSeries();

            CategoryAxis axis1 = new CategoryAxis { Position = OxyPlot.Axes.AxisPosition.Left };
            axis1.MajorGridlineColor = Colors.Blue;
            var valueAxis = new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.2, AbsoluteMinimum = 0 };

            foreach(Dog d in dList)
            {
                axis1.Labels.Add(d.Breed_Name);
                bar_graph.Items.Add(new OxyPlot.Series.BarItem(d.GetProbability()*100));
                bar_graph_plot.Axes.Add(axis1);
            }
            
            bar_graph_plot.Axes.Add(valueAxis);
            bar_graph_plot.Series.Add(bar_graph);
        }
    }
}
