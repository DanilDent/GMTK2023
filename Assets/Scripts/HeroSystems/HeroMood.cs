using System.Collections.Generic;

public struct HeroMood
{
	public int FromScore;
	public int ToScore;
	public string MoodName;
	public List<Hero.AvatarPart> AvatarParts;
	public HeroBehaviour HeroBehaviour;
}