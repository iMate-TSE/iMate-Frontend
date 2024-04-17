using CommunityToolkit.Mvvm.Messaging.Messages;

namespace iMate.Services;

public class SetMoodMessage: ValueChangedMessage<string>
{
    public SetMoodMessage(string mood) : base(mood)
    {
        
    }
    
}