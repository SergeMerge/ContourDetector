using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
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
using ContourDetector.Controls;
using ContourDetector.Converter;
using ContourDetector.ViewModel;
using DataGrid2DLibrary;
using DetectingAlgorithms;
using Microsoft.Win32;

namespace ContourDetector
{
    public partial class MainWindow : Window
    {
        private BitmapImage _source = null;

        public MainWindow()
        {
            InitializeComponent();

            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler) CutContractKeyDown);
            List<AbstractOperator> list = new List<AbstractOperator>()
            {
                new SobelOperator(),
                new PrewittOperator(),
                new KirshOperator(),
                //new CannyOperator()
            };
            AlgorithmViewModel vm = new AlgorithmViewModel(list);
            DataContext = vm;
        }

//        private void BindGausianHorizontal()
//        {
//            MatrixDataView<int> dataView = new MatrixDataView<int>(this._gX);
//            CDataGridHorizontal.ItemsSource = dataView.BindArrayToDataView<int>();            
//        }
//
//        private void BindGuasianVertical()
//        {            
//            MatrixDataView<int> dataView = new MatrixDataView<int>(this._gY);
//            CDataGridVertical.ItemsSource = dataView.BindArrayToDataView<int>();
//        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ImgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                this._source = new BitmapImage(new Uri(op.FileName));
            }
            else
            {
                MessageBox.Show("Please select a file to proccess");
            }
        }

        private void BtnProccess_OnClick(object sender, RoutedEventArgs e)
        {
            int limit = 1;
            IDetectingAlgorithm algrorithm = (IDetectingAlgorithm) SelectedAlgorithm.SelectedItem;
            if (Limit.Text != "" && Int32.TryParse(Limit.Text, out limit) && algrorithm != null && this._source != null)
            {
                Bitmap bitmap = BitmapConverter.BitmapImage2Bitmap(this._source);
                Bitmap edges = algrorithm.DetecteEdges(bitmap, limit);
                BitmapImage imageEdges = BitmapConverter.Bitmap2ImageSource(edges);
                FilteredImage.Source = imageEdges;
            }
            else
            {
                MessageBox.Show(
                    "Please input a limit for filtering. \nYou may not select file. \nYou have to select algorithm for filtering");
            }
        }

        private void c_dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn column = e.Column as DataGridTextColumn;
            Binding binding = column.Binding as Binding;
            binding.Path = new PropertyPath(binding.Path.Path + ".Value");
        }

        private void CutContractKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                SaveFileDialog op = new SaveFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    string filePath = op.FileName;
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource) FilteredImage.Source));
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        encoder.Save(stream);
                }
            }
        }
    }
}