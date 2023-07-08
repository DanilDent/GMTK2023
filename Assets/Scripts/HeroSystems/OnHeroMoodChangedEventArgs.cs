using System;

public class OnHeroMoodChangedEventArgs : EventArgs
{
	public Hero Hero{get; private set;}
	public HeroMood HeroMood{get; private set;}
	public OnHeroMoodChangedEventArgs(Hero hero, HeroMood heroMood)
	{
		Hero = hero;
		HeroMood = heroMood;
	}
}