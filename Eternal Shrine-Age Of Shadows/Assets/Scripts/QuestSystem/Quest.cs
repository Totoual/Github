using UnityEngine;
using System.Collections;
[System.Serializable]
public class Quest {

	[SerializeField]	private string title;
	[SerializeField]	private string subText;
	[SerializeField]	private string description;
	[SerializeField]	private int id;
	[SerializeField]	private int xp;
	[SerializeField]	private string itemSlug;
	[SerializeField]	private bool completed;
	[SerializeField]	private int trueObjective;
	[SerializeField]	private int progress;
	[SerializeField]	private bool chain;
	[SerializeField]	private int chainId;
	[SerializeField] 	private int npcId;
	[SerializeField]	private string completeDialog;
	[SerializeField]	private string inProgressDialog;



	public string Title{
		get{ return title;}
		set{ title = value;}
	}
	public string SubText{
		get{ return subText;}
		set{ subText = value;}
	}
	public string Description{
		get{ return description;}
		set{ description = value;}
	}
	public int Id{
		get{ return id;}
		set{ id = value;}
	}
	public int Xp{
		get{return xp; }
		set{xp = value; }
	}
	public string ItemSlug{
		get{return itemSlug;}
		set{itemSlug = value; }
	}
	public bool Completed{
		get{return completed; }
		set{completed = value; }
	}
	public int TrueObjective{
		get{return trueObjective;}
		set{trueObjective = value; }
	}
	public int Progress{
		get{return progress;}
		set{progress = value; }
	}
	public bool Chain{
		get{return chain; }
		set{chain = value; }
	}
	public int ChainId{
		get{return chainId;}
		set{chainId = value; }
	}

	public int NpcId{
		get{return npcId; }
		set{npcId = value; }
	}

	public string CompleteDialog{
		get{return completeDialog; }
		set{completeDialog = value; }
	}

	public string InProgressDialog{
		get{return inProgressDialog; }
		set{inProgressDialog = value; }
	}

	public Quest(int id,string title,string subText,string description,int xp,string itemSlug,int trueObjective,bool chain,int chainId,int npcId, string completeDialog,string inProgressDialog){
		this.Title = title;
		this.SubText = subText;
		this.Description = description;
		this.Xp = xp;
		this.ItemSlug = itemSlug;
		this.Completed = false;
		this.TrueObjective = trueObjective;
		this.Progress = 0;
		this.Chain = chain;
		this.ChainId = chainId;
		this.NpcId = npcId;
		this.CompleteDialog = completeDialog;
		this.InProgressDialog = inProgressDialog;
	}

}
