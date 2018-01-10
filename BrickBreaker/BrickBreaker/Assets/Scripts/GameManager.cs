using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject ball;
    public GameObject grid;
    public GameObject blockToDisactivate;
    public List<GameObject> disactivesList;
    public List<GameObject> activesList;

	// Use this for initialization
	void Start () {

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        FreezeOrientation(true);

        //BallCollision ballCollisionDetection =ball.GetComponent<BallCollision>();
        //GridCreation createdGrid = grid.GetComponent<GridCreation>();
    }

    void Update()
    {
       // blockToDisactivate = ballCollisionDetection.TouchedBlock;
       // disactivesList = createdGrid.disactivatedBlocks;
       // activesList = createdGrid.activatedBlocks;
       // blockToDisactivate.SetActive(false);
       // blockToDisactivate.tag = "DisactivatedBlock";
       // blockToDisactivate.GetComponent<BoxCollider2D>().enabled = false;
       // activesList.Remove(blockToDisactivate);
       // disactivesList.Add(blockToDisactivate);
    }

    void FreezeOrientation(bool freezeOrnt = true)
    {
        Screen.autorotateToLandscapeLeft = freezeOrnt;
        Screen.autorotateToLandscapeRight = freezeOrnt;
        Screen.autorotateToPortrait = freezeOrnt;
        Screen.autorotateToPortraitUpsideDown = freezeOrnt;
    }

}
