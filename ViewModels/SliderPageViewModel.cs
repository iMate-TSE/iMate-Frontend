using System.ComponentModel;
using System.Windows.Input;

namespace iMate.ViewModels;

// ViewModel class
public partial class SliderPageViewModel : ObservableObject
{ 
    
    // Slider values
    [ObservableProperty]
    private double _firstslidervalue = 10; 

    private double _secondslidervalue = 50;
    
            // Commands
    public ICommand RedButtonCommand { get; private set; }
    public ICommand BlueButtonCommand { get; private set; }
    public ICommand YellowButtonCommand { get; private set; }

    public SliderPageViewModel()
    {
        InitializeCommands();
    }

    private void InitializeCommands()
    {
        RedButtonCommand = new Command(ExecuteRedButtonCommand);
        BlueButtonCommand = new Command(ExecuteBlueButtonCommand);
        YellowButtonCommand = new Command(ExecuteYellowButtonCommand);
    }

    private void ExecuteRedButtonCommand(object obj)
    {
                
    }

    private void ExecuteBlueButtonCommand(object obj)
    {
                
    }

    private void ExecuteYellowButtonCommand(object obj) 
    {
               
    }
            
            
    /*
        in the execute button commands bit i didn't know exactly what to put as this is wehere i assume we feed data into the database and 2i wasn't woking on that so i don't know how you want to do it
        i assume something like:
    
        private void Execute(colour)ButtonCommand(object obj)
       {
            // Save data to the database
            var databaseService = new DatabaseService();
            var ( database ) = new databaseentry
            database.insertmoodedate (firstslidervalue, secondslidervalue, "(colour)");   ?????
        }
    */
        }