namespace P11_Panificator;

public class Meeting(string personName, string location, DateTimeOffset time)
{
    public string PersonName { get; set; } = personName;
    public string Location { get; set; } = location;
    public DateTimeOffset Time { get; set; } = time;

    public bool FirstConfirmed { get; set; } = true;
    public bool SecondConfirmed { get; set; } = false;

    public void WaitConfirmation()
    {
        while (true)
        {
            Console.WriteLine($"Waiting for {PersonName}...");
            if (!InputHelper.TakeInput("confirmation(yes/no)", out var input))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (!ConfirmationInputValidate(input, out var confirmed))
            {
                Console.WriteLine("Please enter 'yes' or 'no'!");
                continue;
            }

            SecondConfirmed = confirmed;
            break;
        }
    }

    private bool CheckConfirmed() => FirstConfirmed && SecondConfirmed;

    private bool ConfirmationInputValidate(string input, out bool confirmed)
    {
        confirmed = false;
        input = input.Trim().ToLowerInvariant();
        if (input is not ("yes" or "no")) return false;

        confirmed = input == "yes";
        return true;
    }

    public override string ToString()
    {
        return CheckConfirmed() ? $"Meeting with {PersonName} at {Location} on {Time}. " : "Meeting not confirmed.";
    }
}