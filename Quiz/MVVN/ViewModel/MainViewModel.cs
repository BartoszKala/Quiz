using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using Quiz.Commands;
using Quiz.MVVN.Model;
using Quiz.ViewModel.BaseClass;

namespace Quiz.MVVN.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly Manager _manager;

        private Points? _mainPoints;
        public Points? MainPoints
        {
            get => _mainPoints;
            set
            {
                _mainPoints = value;
                onPropertyChanged(nameof(MainPoints));
                onPropertyChanged(nameof(Points));
                onPropertyChanged(nameof(Scale));
            }
        }

        public int Points => MainPoints?.getP() ?? 0;
        public int Scale => MainPoints?.getS() ?? 0;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                onPropertyChanged(nameof(CurrentView));
            }
        }

        private bool _showQuestionsView;
        public bool ShowQuestionsView
        {
            get => _showQuestionsView;
            set
            {
                _showQuestionsView = value;
                onPropertyChanged(nameof(ShowQuestionsView));
            }
        }

        private bool _isStartViewVisible = true;
        public bool IsStartViewVisible
        {
            get => _isStartViewVisible;
            set
            {
                _isStartViewVisible = value;
                onPropertyChanged(nameof(IsStartViewVisible));
            }
        }

        public MainViewModel()
        {
            try
            {
                _manager = new Manager();
                QView = new QuestionsView(_manager);
                RView = new ResultView(_manager);

                CurrentView = QView;
                ShowQuestionsView = false;

                MainPoints = new Points(0, 0); // Initialize with default values
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during initialization: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SwitchViews()
        {
            try
            {
                CurrentView = ShowQuestionsView ? QView : RView;
                ShowQuestionsView = !ShowQuestionsView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while switching views: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public RelayCommand SwitchViewsCommand => new RelayCommand(o => SwitchViews());

        public QuestionsView QView { get; }
        public ResultView RView { get; }

        public void SwitchToResultView()
        {
            try
            {
                if (MainPoints != null)
                {

                    T = MainPoints.Show();

                }

                CurrentView = RView;
                ShowQuestionsView = false;
                IsStartViewVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while switching to result view: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void StartQuiz()
        {
            try
            {
                if (MainPoints == null)
                {
                    MainPoints = new Points(0, 10);
                }

                onPropertyChanged(nameof(MainPoints));
                onPropertyChanged(nameof(Points));
                onPropertyChanged(nameof(Scale));

                IsStartViewVisible = false;
                ShowQuestionsView = true;
                CurrentView = QView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while starting the quiz: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OpenFileAndSwitchToQuizView()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Database files (*.db)|*.db|All files (*.*)|*.*",
                    DefaultExt = ".db"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    StartQuiz();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public RelayCommand OpenFileCommand => new RelayCommand(o => OpenFileAndSwitchToQuizView());
        public RelayCommand StartQuizCommand => new RelayCommand(o => StartQuiz());

        private string t;

        public string T
        {
            get => t;
            set
            {
                t = value;
                onPropertyChanged(nameof(T));
            }
        }
    }
}

