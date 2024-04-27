using CommunityToolkit.Mvvm.Messaging.Messages;

namespace iMate.Services;

public class UpdateHeaderMessage : ValueChangedMessage<string>
{
    public UpdateHeaderMessage(string update) : base(update)
    {
        
    }
}