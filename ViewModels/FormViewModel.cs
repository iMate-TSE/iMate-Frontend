using iMate.Models;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using iMate.Models.ApiModels;
using iMate.Models.FormModels;
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
                    .WithChild(new DecisionTreeClassifier(0, "Relaxed/Sleepy/Stressed").Build())
                    ).Build()
                ).Build()
            ).Build();

        private Dictionary<string, string> moodDescriptors = new Dictionary<string, string>()
        {
            {"Anxious",
                "Anxiety is what we feel when we are worried, tense or afraid – particularly about things that are about to happen, or which we think could happen in the future. Anxiety is a natural human response when we feel that we are under threat. It can be experienced through our thoughts, feelings and physical sensations."},
            {"Angry" , "Anger is a normal, healthy emotion, which we all feel sometimes. We often feel angry when we're frustrated, we don't like a situation or we have been treated badly. But we may also feel angry without knowing why, and that's okay – as long as we find a way to express our feelings safely."},
            {"Disgust", "Disgusted is feeling like you want to turn your nose up and walk away. It's a strong rejection of something you find offensive, dirty, or unpleasant. Your face might scrunch up, and you might feel a wave of nausea. Rotten food, bad smells, or rude behavior can all trigger disgust."},
            {"Stressed","Stress is how we react when we feel under pressure or threatened. It usually happens when we are in a situation that we don't feel we can manage or control."},
            {"Lonely", "Many of us feel lonely from time to time. Feelings of loneliness are personal, so everyone's experience will be different. Some people describe loneliness as the feeling we have when our need for social contact and relationships isn’t met. But loneliness isn’t the same as being alone."},
            {"Sad", "Sadness is an emotional state characterized by feelings of unhappiness and low mood. It is a normal response to situations that are upsetting, painful, or disappointing. Sometimes these feelings can feel more intense, while in other cases they might be fairly mild."},
            {"Depressed","Depression often involves having a low mood or losing interest and enjoyment in things. It can also cause a range of other changes to how you feel or behave."},
            {"Happy", "Happiness is something that people seek to find, yet what defines happiness can vary from one person to the next. Typically, happiness is an emotional state characterized by feelings of joy, satisfaction, contentment, and fulfillment. While happiness has many different definitions, it is often described as involving positive emotions and life satisfaction. "},
            {"Excited", "Excited feels like a fizzing feeling in your chest, like butterflies are having a party.  Everything seems brighter, and you might have a burst of energy. You can't wait for something awesome to happen!"},
            {"Loved/ Grateful", "Loved feels like a warm hug from the inside. It's a sense of security and deep connection with others. You feel cherished, valued, and important. Grateful feels like a sunshine spreading through your chest.  You appreciate the good things in your life, big or small.  It brings a sense of contentment and happiness."},
            {"Relaxed", "Relaxed feels like a weight lifting off your shoulders. You're calm and at ease, with your mind and body feeling loose and unburdened. It's a peaceful state of being."},
            {"Bored", "Bored feels like a slow leak in your energy. Time seems to drag, and you can't find anything interesting to do. You might feel restless or irritable."},
            {"Sleepy", "Sleepy feels like your eyelids are getting heavy and your body is craving rest. You might yawn a lot and feel sluggish. It's time to wind down and recharge."},
        };

        [ObservableProperty] 
        public ObservableCollection<FormQuestions> _questions = new ObservableCollection<FormQuestions>();

        [ObservableProperty] private bool _isPositive = false;
        [ObservableProperty] private bool _isNegative = false;
        [ObservableProperty] private bool _isActive = false;
        [ObservableProperty] private bool _isPassive = false;
        [ObservableProperty] private bool _isIntense = false;
        [ObservableProperty] private bool _showSubmit = false;

        [ObservableProperty] private bool _isPADForm = false;
        [ObservableProperty] private bool _showPADSubmit = false;

        [ObservableProperty] private bool _formIncomplete = true;
        [ObservableProperty] private bool _showOutputScreen = false;

        [ObservableProperty] private double _intensityValue = 0;
        [ObservableProperty] private double _passivityValue = 0;
        [ObservableProperty] private double _activityValue = 0;
        [ObservableProperty] private double _positiveValue = 0;
        [ObservableProperty] private double _negativeValue = 0;

        [ObservableProperty] private string _leaf = "No Mood";
        [ObservableProperty] private string _descriptor;

        public ICommand GetQuestionsCommand { get; }
        public ICommand SubmitForm { get; }

        private Node currentDecisionStep = root;
        
        public FormViewModel(IHttpService httpService) : base(httpService)
        {
            GetQuestionsCommand = new Command(FetchQuestions);
            SubmitForm = new Command(computePad);
        }

        public void Decide(int weight)
        {
            currentDecisionStep = currentDecisionStep.Children.Count switch
            {
                > 1 => currentDecisionStep.Children[weight],
                > 0 => currentDecisionStep.Children.First(),
                _ => currentDecisionStep
            };

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

        private async Task SaveMoodEntry(string mood)
        {
            string currentToken = await SecureStorage.Default.GetAsync("auth_token");
            var MoodEntryData = new SaveMoodDataModel
            {
                mood = mood,
                token = currentToken,
            };

            string content = JsonSerializer.Serialize(MoodEntryData);
            await HttpService.SaveMoodEntry(content);
        }

        public async void computePad()
        {
            int intensityQuestions = _questionResponses.Values.Sum();

            string moodOutput = "";

            switch (Leaf)
            {
                case "Angry/Disgusted":
                    if (intensityQuestions > 5) moodOutput = "Angry";
                    else moodOutput = "Disgust";
                    break;
                case "Anxious/Stressed":
                    if (intensityQuestions > 5) moodOutput = "Stressed";
                    else moodOutput = "Anxious";
                    break;
                case "Lonely/Sad/Depressed":
                    if (intensityQuestions >= 3) moodOutput = "Sad";
                    else if (intensityQuestions > 3 && intensityQuestions <= 7) moodOutput = "Lonely";
                    else moodOutput = "Depressed";
                    break;
                case "Happy/Excited":
                    if (intensityQuestions > 5) moodOutput = "Excited";
                    else moodOutput = "Happy";
                    break;
                case "Loved/Grateful":
                    if (intensityQuestions > 5) moodOutput = "Loved/ Grateful";
                    else moodOutput = "Loved/ Grateful";
                    break;
                case "Relaxed/Bored/Sleepy":
                    if (intensityQuestions >= 3) moodOutput = "Relaxed";
                    else if (intensityQuestions > 3 && intensityQuestions <= 7) moodOutput = "Bored";
                    else moodOutput = "Sleepy";
                    break;
                default:
                    break;
            }
            
            WeakReferenceMessenger.Default.Send<SetMoodMessage>(new SetMoodMessage(moodOutput));
            Leaf = moodOutput;
            FormIncomplete = false;
            ShowSubmit = false;
            ShowOutputScreen = true;
            Descriptor = moodDescriptors[Leaf];
            await SaveMoodEntry(moodOutput);
        }

        public async void PADRegression(int p, int a, int d)
        {
            string mood = await HttpService.FetchMood(p, a, d);
            WeakReferenceMessenger.Default.Send<SetMoodMessage>(new SetMoodMessage(mood));
            Leaf = mood;
            FormIncomplete = false;
            IsPADForm = false;
            ShowPADSubmit = false;
            ShowOutputScreen = true;
            Descriptor = moodDescriptors[Leaf];
            SaveMoodEntry(mood);
        }

        public async void FetchQuestions()
        {
            List<FormQuestions> allQuestions = await HttpService.GetQuestions(Leaf);
            ShowSubmit = true;
            List<FormQuestions> randomFour;
            if (allQuestions.Count > 4)
            {
                randomFour = allQuestions.OrderBy(c => Guid.NewGuid()).Take(3).ToList();
            }
            else
            {
                randomFour = allQuestions;
            }
            
            foreach (var question in randomFour)
            {
                Questions.Add(question);
                _questionResponses.Add(question.formQuestionID, -1);
            }
        }
    }

}
