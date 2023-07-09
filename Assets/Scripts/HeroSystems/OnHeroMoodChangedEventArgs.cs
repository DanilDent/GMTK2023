using System;

public class OnHeroMoodChangedEventArgs : EventArgs
{
	public Hero Hero{get; private set;}
	public OnHeroMoodChangedEventArgs(Hero hero)
	{
		Hero = hero;
	}
}