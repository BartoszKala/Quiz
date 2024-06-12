using System.Configuration;
using System.Data;
using System.Windows;

namespace Quiz
{
    using Quiz.MVVN.ViewModel;
    using System.Diagnostics;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
        }



    }

}
