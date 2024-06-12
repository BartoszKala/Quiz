using System.Collections.ObjectModel;
using Quiz.ViewModel.BaseClass;
using Quiz.MVVN.Model;

namespace Quiz.MVVN.ViewModel
{
    class ResultView : ViewModelBase
    {
        private Manager manager = null;
        //public Points mPoints;
        //MainViewModel mainViewModel = App.Current.MainWindow.DataContext as MainViewModel;

        //public string text;



        //public string MainPoints
        //{
        //    get { return mainViewModel.MainPoints.Show(); }
        //    set
        //    {
        //        text = mainViewModel.MainPoints.Show();
        //        text = value;
        //        onPropertyChanged(nameof(MainPoints));
        //    }
        //}





        public ResultView(Manager manager)
        {
            this.manager = manager;
        }
    }
}