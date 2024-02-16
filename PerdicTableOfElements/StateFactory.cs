using System;
namespace ChemUtility.States
{
	public static class StateFactory
	{
		static AbstractChemState MassState;
		public static AbstractChemState CurrentState;
		static AbstractChemState InfoState;
	
		static StateFactory()
		{//thing
			MassState = new MassState();
			InfoState = new InfoState();
			CurrentState = MassState;
		}

		public static void SetCurrentState(ChemState newState)
		{

			switch (newState)
			{
				case ChemState.Mass:
					CurrentState = MassState;
					break;
				case ChemState.Info:
					CurrentState = InfoState;
					break;
				default:
					break;
			}



		}
		public enum ChemState
		{
			Mass,
			Info
			
		}
	}
}
