using UnityEngine;
using System.Collections;
[System.Serializable]
public class Skill {
	[SerializeField]private string name;
	[SerializeField]private string description;
	[SerializeField]private string skill_slug;
	[SerializeField]private int dmg;

	public string Name{
		get{ return name;}
		set{ name = value;}
	}

	public string Description{
		get{ return description;}
		set{ description = value;}
	}
	public string Skill_Slug{
		get{ return skill_slug;}
		set{ skill_slug = value;}
	}
	public int Damage{
		get{ return dmg;}
		set{ dmg = value;}
	}


}
