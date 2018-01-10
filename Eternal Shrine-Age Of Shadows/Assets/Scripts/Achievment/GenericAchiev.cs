using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class GenericAchiev {

	[SerializeField]private int id;
	[SerializeField]private string title;
	[SerializeField]private string description;
	[SerializeField]private string category;
	[SerializeField]private string slug;
	[SerializeField]private int points;
	[SerializeField]private bool unlocked;
	[SerializeField]private int[] dependency;
	[SerializeField]private Sprite icon;


	#region Setters & Getter
	public int Id{
		get{ return id;}
		set{ id = value;}
	}
	public string Title{
		get{ return title;}
		set{ title = value;}
	}

	public string Description{
		get{ return description;}
		set{ description = value;}
	}
	public string Category{
		get{return category;}
		set{ category = value;}
	}
	public string Slug{
		get{ return slug;}
		set{ slug = value;}
	}
	public int Points{
		get{ return points;}
		set{ points = value;}
	}
	public bool Unlocked{
		get{ return unlocked;}
		set{ unlocked = value;}
	}
	public Sprite Icon{
		get{ return icon;}
		set{ icon = value;}
	}
	public int[] Dependency{
		get{ return dependency;}
	}
	#endregion

	public GenericAchiev(int id, string title,string description,string category,string slug,int points,int unlocked, int[]dependency=null){

		this.id = id;
		this.title = title;
		this.description = description;
		this.category = category;
		this.points = points;
		this.unlocked = unlocked == 1 ?true:false;
		this.dependency = dependency;
		this.slug = slug;
		this.icon = Resources.Load<Sprite>("Sprites/Achievments/" + slug);
		
	}

	public bool EarnAchievment(){
		if (!unlocked) {
			return true;
		}
		return false;
	}

}
