using System;


public abstract class ModeBase
{
    public abstract void ParseInput(string input);
    public abstract void SendOutput();
    public abstract void DisplayPrompt();
    public abstract void Switch();

    protected void DoSomthing()
    {
        Console.WriteLine("hello");
    }

}
