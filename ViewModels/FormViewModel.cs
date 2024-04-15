using iMate.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using iMate.Models.ApiModels;
using iMate.Services;

namespace iMate.ViewModels
{
    partial class FormViewModel : ViewModelBase
    {
        private static Node root = new DecisionTreeClassifier(999, "Root")
            .WithChild(new DecisionTreeClassifier(0, "Negative")
                .WithChild(new DecisionTreeClassifier(0, "Intensity")
                    .WithChild((new DecisionTreeClassifier(1, "Anger/Disgust")).Build())
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
                        .WithChild(new DecisionTreeClassifier(0, "Relaxed/Sleepy/Stressed").Build())
                    ).Build())
             
                
                .Build()
            ).Build();

        [ObservableProperty] public ObservableCollection<FormQuestions> _questions;

        [ObservableProperty] private bool _isPositive = false;
        [ObservableProperty] private bool _isNegative = false;
        [ObservableProperty] private bool _isActive = false;
        [ObservableProperty] private bool _isPassive = false;
        [ObservableProperty] private bool _isIntense = false;

        [ObservableProperty] private string _leaf = "No Mood";

        public ICommand GetQuestionsCommand { get; }

        private Node currentDecisionStep = root;
        
        public FormViewModel(IHttpService httpService) : base(httpService)
        {
            GetQuestionsCommand = new Command(fetchQuestions);
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
            else if (currentDecisionStep.Data == "Relaxed/Sleepy/Stressed")
            {
                Leaf = currentDecisionStep.Data; 
            }
            else if (currentDecisionStep.Data == "Active")
            {
                IsPositive = false;
                IsActive = true;
            }
            else if (currentDecisionStep.Data == "Happy/Excited")
            {
                Leaf = currentDecisionStep.Data;
                // Happy
                SecureStorage.Default.SetAsync("current_mood", "Happy");
            }
            else if (currentDecisionStep.Data == "Loved/Grateful")
            {
                Leaf = currentDecisionStep.Data;
                // Loved/ Grateful
                SecureStorage.Default.SetAsync("current_mood", "Loved/ Grateful");
            }
            else if (currentDecisionStep.Data == "Intensity")
            {
                IsNegative = false;
                IsIntense = true;
            }
            else if (currentDecisionStep.Data == "Anger/Disgust")
            {
                Leaf = currentDecisionStep.Data; 
                // Angry
                SecureStorage.Default.SetAsync("current_mood", "Angry");
            }
            else if (currentDecisionStep.Data == "Passivity")
            {
                IsPassive = true;
                IsNegative = false;
            }
            else if (currentDecisionStep.Data == "Anxious/Stressed")
            {
                Leaf = currentDecisionStep.Data;
                // Anxious
                SecureStorage.Default.SetAsync("current_mood", "Anxious");
            }
            else if (currentDecisionStep.Data == "Lonely/Sad/Depressed")
            {
                Leaf = currentDecisionStep.Data;
                // Sad
                SecureStorage.Default.SetAsync("current_mood", "Sad");
            }
        }

        public async void fetchQuestions()
        {
            //List<FormQuestions> allQuestions = await HttpService.GetQuestions(Leaf);
            //List<FormQuestions> randomFour = allQuestions.OrderBy(c => Guid.NewGuid()).ToList();
            
            //foreach (var question in randomFour)
            //{
            //    Console.WriteLine("==========" + question.Question);
            //    Questions.Add(question);
            //}
        }
    }

}
