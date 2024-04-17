using iMate.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using iMate.Models.ApiModels;
using iMate.Services;

namespace iMate.ViewModels
{
    partial class FormViewModel : ViewModelBase
    {
        private static Node root = new DecisionTreeClassifier(999, "Root")
            .WithChild(new DecisionTreeClassifier(0, "Negative")
                .WithChild(new DecisionTreeClassifier(0, "Intensity")
                    .WithChild((new DecisionTreeClassifier(1, "Angry/Disgusted")).Build())
                    .Build()
                )
                .WithChild(new DecisionTreeClassifier(1, "Passivity")
                    .WithChild((new DecisionTreeClassifier(0, "Anxious/Stressed")).Build())
                    .WithChild((new DecisionTreeClassifier(1, "Lonely/Sad/Depressed")).Build())
                    .Build()
                ).Build())
            .WithChild(new DecisionTreeClassifier(1, "Positive")
               .WithChild((new DecisionTreeClassifier(1, "Active")
                                    .WithChild(new DecisionTreeClassifier(0, "Happy/Excited").Build())
                                    .WithChild(new DecisionTreeClassifier(1,"Loved/Grateful").Build())
                 ).Build())
                .WithChild((new DecisionTreeClassifier(0, "Passive")
                        .WithChild(new DecisionTreeClassifier(0, "Relaxed/Bored/Sleepy").Build())
                    ).Build())
             
                
                .Build()
            ).Build();

        [ObservableProperty] 
        public ObservableCollection<FormQuestions> _questions = new ObservableCollection<FormQuestions>();

        [ObservableProperty] private bool _isPositive = false;
        [ObservableProperty] private bool _isNegative = false;
        [ObservableProperty] private bool _isActive = false;
        [ObservableProperty] private bool _isPassive = false;
        [ObservableProperty] private bool _isIntense = false;
        [ObservableProperty] private bool _showSubmit = false;

        [ObservableProperty] private double _intensityValue = 0;
        [ObservableProperty] private double _passivityValue = 0;
        [ObservableProperty] private double _activityValue = 0;
        [ObservableProperty] private double _positiveValue = 0;
        [ObservableProperty] private double _negativeValue = 0;

        [ObservableProperty] private string _leaf = "No Mood";

        public ICommand GetQuestionsCommand { get; }
        public ICommand SubmitForm { get; }

        private Node currentDecisionStep = root;
        
        public FormViewModel(IHttpService httpService) : base(httpService)
        {
            Console.WriteLine("XXXXXXXXFormXXXXXXXXX" + httpService);
            GetQuestionsCommand = new Command(FetchQuestions);
            SubmitForm = new Command(computePad);
        }

        public void Decide(int weight)
        {
            if (currentDecisionStep.Children.Count > 1)
            {
                currentDecisionStep = currentDecisionStep.Children[weight];
            }
            else if (currentDecisionStep.Children.Count > 0)
            {
                currentDecisionStep = currentDecisionStep.Children.First();
            }

            if (currentDecisionStep.Data == "Positive")
            {
                IsPositive = true;
            }
            else if (currentDecisionStep.Data == "Negative")
            {
                IsNegative = true;
            }
            else if (currentDecisionStep.Data == "Passive")
            {
                IsPositive = false;
                IsPassive = true;
            } 
            else if (currentDecisionStep.Data == "Relaxed/Bored/Sleepy")
            {
                Leaf = currentDecisionStep.Data; 
                IsPassive = false;
                FetchQuestions();
            }
            else if (currentDecisionStep.Data == "Active")
            {
                IsPositive = false;
                IsActive = true;
            }
            else if (currentDecisionStep.Data == "Happy/Excited")
            {
                Leaf = currentDecisionStep.Data;
                IsActive = false;
                FetchQuestions();
            }
            else if (currentDecisionStep.Data == "Loved/Grateful")
            {
                Leaf = currentDecisionStep.Data;
                IsActive = false;
                FetchQuestions();
            }
            else if (currentDecisionStep.Data == "Intensity")
            {
                IsNegative = false;
                IsIntense = true;
            }
            else if (currentDecisionStep.Data == "Angry/Disgusted")
            {
                Leaf = currentDecisionStep.Data;
                IsIntense = false;
                FetchQuestions();
            }
            else if (currentDecisionStep.Data == "Passivity")
            {
                IsPassive = true;
                IsNegative = false;
            }
            else if (currentDecisionStep.Data == "Anxious/Stressed")
            {
                Leaf = currentDecisionStep.Data;
                IsPassive = false;
                FetchQuestions();
            }
            else if (currentDecisionStep.Data == "Lonely/Sad/Depressed")
            {
                Leaf = currentDecisionStep.Data;
                IsPassive = false;
                FetchQuestions();
            }
        }

        private Dictionary<int, int> _questionResponses = new Dictionary<int, int>();

        public void UpdateResponses(int questionId, int value)
        {
            _questionResponses[questionId] = value;
        }

        public async void computePad()
        {
            int intensityQuestions = _questionResponses.Values.Sum();
            
            int arousal = 2;
            int pleasure = 10;
            int dominance = 2;

            double feelingWeight = 0.6;
            double intensityWeight = 0.4;

            switch (Leaf)
            {
                case "Angry/Disgusted":
                    arousal = ((int)(IntensityValue * feelingWeight) + (int)(intensityQuestions * intensityWeight));
                    pleasure -= (int)IntensityValue;
                    dominance += (int)IntensityValue;
                    break;
                case "Anxious/Stressed":
                    arousal = ((int)(PassivityValue * feelingWeight) + (int)(intensityQuestions * intensityWeight));
                    pleasure -= (int)(PassivityValue * feelingWeight);
                    dominance = Math.Min(dominance + (int)(PassivityValue * feelingWeight), 10); // Limit dominance to 10
                    break;
                case "Lonely/Sad/Depressed":
                    arousal = 1;
                    pleasure -= (int)(IntensityValue * feelingWeight);
                    dominance = Math.Max(dominance - (int)(IntensityValue * feelingWeight), 1); // Limit dominance to 1 (minimum)
                    break;
                case "Happy/Excited":
                    arousal = Math.Min(arousal + (int)(ActivityValue * feelingWeight), 10); // Limit arousal to 10
                    pleasure += Math.Min((int)(ActivityValue * feelingWeight), 10);
                    dominance = Math.Min(dominance + (int)(ActivityValue * feelingWeight), 10); // Limit dominance to 10
                    break;
                case "Loved/Grateful":
                    // Arousal can vary depending on intensity
                    arousal = ((int)(ActivityValue * feelingWeight) + (int)(intensityQuestions * intensityWeight)); 
                    // Feeling weight directly contributes to pleasure
                    pleasure += (int)(ActivityValue * feelingWeight);
                    // Loved/Grateful might not involve high dominance
                    dominance = Math.Min(dominance + (int)(ActivityValue * feelingWeight), 10); // Limit dominance to 10
                    break;
                case "Relaxed/Bored/Sleepy":
                    // Low activation
                    arousal = 1;
                    // Adjust pleasure based on specific emotion (relaxed vs bored/sleepy)
                    pleasure = Math.Max(pleasure - (int)(PassivityValue * feelingWeight / 2), 1); // Reduce pleasure slightly, limit to 1 (minimum)
                    // Relaxed/Bored/Sleepy might not involve strong dominance
                    dominance = Math.Max(dominance - (int)(PassivityValue * feelingWeight / 5), 1); // Reduce dominance slightly, limit to 1 (minimum)
                    break;
                default:
                    break;
            }

            Console.WriteLine($"P = {pleasure} A = {arousal} D = {dominance}");
            string mood = await HttpService.FetchMood(pleasure, arousal, dominance);
            WeakReferenceMessenger.Default.Send<SetMoodMessage>(new SetMoodMessage(mood));
            Leaf = mood;
        }

        public async void FetchQuestions()
        {
            List<FormQuestions> allQuestions = await HttpService.GetQuestions(Leaf);
            ShowSubmit = true;        
            //List<FormQuestions> randomFour = allQuestions.OrderBy(c => Guid.NewGuid()).ToList();
            
            foreach (var question in allQuestions)
            {
                Questions.Add(question);
                _questionResponses.Add(question.formQuestionID, -1);
            }
        }
    }

}
