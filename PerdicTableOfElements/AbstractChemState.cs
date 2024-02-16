using System;

namespace ChemUtility.States
{
    public abstract class AbstractChemState
    {
        public abstract void ParseInput(string input);
        public abstract void SendOutput();
        public abstract void DisplayPrompt();
        public abstract void EnterState();
        public abstract void ExitState();
        protected void Switch(StateFactory.ChemState state)
        {
            this.ExitState();
            StateFactory.SetCurrentState(state);
            StateFactory.CurrentState.EnterState();
        }

        protected void DoModeParse()
        {

        }


    }
}
