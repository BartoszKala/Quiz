using Quiz.MVVN.Model;
using Quiz.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Quiz.MVVN.ViewModel
{
    using MVVN.Model;
    using Quiz.Model;
    using System.Collections;

    class QuestionsView : ViewModelBase
    {
        private Manager manager = null;
        private ObservableCollection<Question> quiz = null;
        private bool nextQuestionEnable = false;
        private int QuestionIndex = 0;
        private DispatcherTimer timer;
        private int time;
       
        public RelayCommand FinishQuizCommand { get; set; }

        #region Text
        public string QText
        {
            get { return CurrentQuestion.QuestionText; }
            set
            {
                CurrentQuestion.QuestionText = value;
                onPropertyChanged(nameof(QText));
            }
        }

       

        public int Time
        {
            get { return time; }
            set
            {
                time = value;
                onPropertyChanged(nameof(Time));
            }
        }

        public string A
        {
            get { return CurrentQuestion.Answers[0].AnswerText; }
            set
            {
                CurrentQuestion.Answers[0].AnswerText = value;
                onPropertyChanged(nameof(A));
            }
        }

        public string D
        {
            get { return CurrentQuestion.Answers[3].AnswerText; }
            set
            {
                CurrentQuestion.Answers[3].AnswerText = value;
                onPropertyChanged(nameof(D));
            }
        }

        public string C
        {
            get { return CurrentQuestion.Answers[2].AnswerText; }
            set
            {
                CurrentQuestion.Answers[2].AnswerText = value;
                onPropertyChanged(nameof(C));
            }
        }

        public string B
        {
            get { return CurrentQuestion.Answers[1].AnswerText; }
            set
            {
                CurrentQuestion.Answers[1].AnswerText = value;
                onPropertyChanged(nameof(B));
            }
        }
        #endregion

        #region Konstruktory
        public QuestionsView(Manager manager)
        {
            this.manager = manager;
            quiz = manager.Questions;
            this.CurrentQuestion = quiz[QuestionIndex];
            InitializeTimer();
            StartTimer();
            FinishQuizCommand = new RelayCommand(ExecuteFinishQuiz);
            selectedAnswersList = new List<List<Answer>>(quiz.Count);
            for (int i = 0; i < quiz.Count; i++)
            {
                selectedAnswersList.Add(new List<Answer>());
            }
        }

        private Question CurrentQuestion { get; set; }

        public ObservableCollection<Question> Questions
        {
            get { return quiz; }
            set { quiz = value; onPropertyChanged(nameof(Questions)); }
        }
        #endregion

        #region Next Question
        public RelayCommand NextQuestion => new RelayCommand(execute => Next(), canExecute => { return true; });

        private void Next()
        {
            if (QuestionIndex < quiz.Count - 1)
            {
                if (QuestionIndex == 0 || currentSelectedAnswers.Count > 0 || Time <= 0)
                {
                    QuestionIndex++;
                    CurrentQuestion = quiz[QuestionIndex];

                    onPropertyChanged(nameof(QText));
                    onPropertyChanged(nameof(A));
                    onPropertyChanged(nameof(B));
                    onPropertyChanged(nameof(C));
                    onPropertyChanged(nameof(D));
                    Time = CurrentQuestion.Time;
                    RestartTimer();
                    currentSelectedAnswers.Clear(); 
                    onPropertyChanged(nameof(NextQuestionEnable));
                    AIsClicked = false;
                    BIsClicked = false;
                    CIsClicked = false;
                    DIsClicked = false;
                }
            }
            else
            {
                FinishQuizCommand.Execute(null);
            }
        }


        #endregion

        #region Click
        public RelayCommand ButtonClick => new RelayCommand(execute => Click((Answer)execute), canExecute => { return true; });

        private void Click(Answer answer)
        {
            if (currentSelectedAnswers.Contains(answer))
            {
                currentSelectedAnswers.Remove(answer);
            }
            else
            {
                currentSelectedAnswers.Add(answer);
            }
            UpdateSelectedAnswers();
            onPropertyChanged(nameof(NextQuestionEnable));
        }

        public RelayCommand Confirm => new RelayCommand(execute => ConfirmAnswers(), canExecute => { return selectedAnswersList.Any(list => list.Count > 0); });

        private void ConfirmAnswers()
        {
            points = 0;


            foreach (var answerList in selectedAnswersList)
            {
                if (answerList != null && answerList.Count > 0 && answerList.All(a =>(bool) a.IsCorrect))
                {
                    points++;
                }
            }

            selectedAnswersList.Clear();
            for (int i = 0; i < quiz.Count; i++)
            {
                selectedAnswersList.Add(new List<Answer>());
            }
        }

        private void UpdateSelectedAnswers()
        {
            selectedAnswersList[QuestionIndex] = new List<Answer>(currentSelectedAnswers);

        }

        

        private bool _aIsClicked;
        private List<Answer> currentSelectedAnswers = new List<Answer>();
        private List<List<Answer>> selectedAnswersList;

        public bool AIsClicked
        {
            get => _aIsClicked;
            set
            {
                if (_aIsClicked != value)
                {
                    _aIsClicked = value;
                    if (value)
                    {
                        currentSelectedAnswers.Add(CurrentQuestion.Answers[0]);
                    }
                    else
                    {
                        currentSelectedAnswers.Remove(CurrentQuestion.Answers[0]);
                    }
                    UpdateSelectedAnswers();
                    onPropertyChanged(nameof(AIsClicked));
                    onPropertyChanged(nameof(NextQuestionEnable));
                }
            }
        }

        public bool BIsClicked
        {
            get => _bIsClicked;
            set
            {
                if (_bIsClicked != value)
                {
                    _bIsClicked = value;
                    if (value)
                    {
                        currentSelectedAnswers.Add(CurrentQuestion.Answers[1]);
                    }
                    else
                    {
                        currentSelectedAnswers.Remove(CurrentQuestion.Answers[1]);
                    }
                    UpdateSelectedAnswers();
                    onPropertyChanged(nameof(BIsClicked));
                    onPropertyChanged(nameof(NextQuestionEnable));
                }
            }
        }

        public bool CIsClicked
        {
            get => _cIsClicked;
            set
            {
                if (_cIsClicked != value)
                {
                    _cIsClicked = value;
                    if (value)
                    {
                        currentSelectedAnswers.Add(CurrentQuestion.Answers[2]);
                    }
                    else
                    {
                        currentSelectedAnswers.Remove(CurrentQuestion.Answers[2]);
                    }
                    UpdateSelectedAnswers();
                    onPropertyChanged(nameof(CIsClicked));
                    onPropertyChanged(nameof(NextQuestionEnable));
                }
            }
        }

        private bool _dIsClicked;
        public bool DIsClicked
        {
            get => _dIsClicked;
            set
            {
                if (_dIsClicked != value)
                {
                    _dIsClicked = value;
                    if (value)
                    {
                        currentSelectedAnswers.Add(CurrentQuestion.Answers[3]);
                    }
                    else
                    {
                        currentSelectedAnswers.Remove(CurrentQuestion.Answers[3]);
                    }
                    UpdateSelectedAnswers();
                    onPropertyChanged(nameof(DIsClicked));
                    onPropertyChanged(nameof(NextQuestionEnable));
                }
            }
        }


        public int points = 0;
        public bool NextQuestionEnable => currentSelectedAnswers.Count > 0;

        private bool _isButtonEnabled = true;
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                onPropertyChanged(nameof(IsButtonEnabled));
            }
        }
        #endregion

        public RelayCommand FinishQuiz => new RelayCommand(execute => Finish(), canExecute => { return true; });

        private void Finish()
        {
            ShowFinishView = false;
        }


        public Points MainPoints { get; private set; }
        private void ExecuteFinishQuiz(object parameter)
        {
            ConfirmAnswers();
            MainPoints = new Points(points, quiz.Count - 1);
            MainViewModel mainViewModel = App.Current.MainWindow.DataContext as MainViewModel;
            mainViewModel.MainPoints = MainPoints;
            mainViewModel.SwitchToResultView();
        }

        private bool _showFinishView = false;
        private bool _cIsClicked;
        private bool _bIsClicked;

        public bool ShowFinishView
        {
            get { return _showFinishView; }
            set
            {
                _showFinishView = value;
                onPropertyChanged(nameof(ShowFinishView));
            }
        }

        #region Timer
        public RelayCommand TimeCount => new RelayCommand(execute => StartTimer(), canExecute => { return true; });


        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void StartTimer()
        {
            if (timer != null && !timer.IsEnabled)
            {
                timer.Start();
            }
        }

        private void StopTimer()
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Time > 0)
            {
                Time--;
            }
            else
            {
                StopTimer();
                Next();
            }
        }

        private void RestartTimer()
        {
            StopTimer();
            Time = CurrentQuestion.Time;
            StartTimer();
        }

        #endregion
    }
}