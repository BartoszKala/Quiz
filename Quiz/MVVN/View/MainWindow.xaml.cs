using Quiz.MVVN.Model;
using Quiz.MVVN.ViewModel;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quiz
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                MainViewModel manager = new MainViewModel();
                DataContext = manager;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during initialization: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
