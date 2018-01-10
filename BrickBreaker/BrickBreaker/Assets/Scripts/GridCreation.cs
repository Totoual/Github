using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreation : MonoBehaviour
{

    [SerializeField]
    private int rows;
    [SerializeField]
    private int cols;
    [SerializeField]
    private Vector2 gridSize;
    [SerializeField]
    private Vector2 gridOffset;

    [SerializeField]
    private Sprite cellSprite;
    private Vector2 cellSize;
    private Vector2 cellScale;

    public List<GameObject> edgeBlocks = new List<GameObject>();
    public List<GameObject> activatedBlocks = new List<GameObject>();
    public List<GameObject> disactivatedBlocks = new List<GameObject>();

    public Vector2 GridOffset {
        get{
            return gridOffset;
        }
    }


    // Use this for initialization
    void Start()
    {

        InitCells();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitCells()
    {

        GameObject cellObject = new GameObject();
        cellObject.AddComponent<SpriteRenderer>().sprite = cellSprite;
        cellSize = cellSprite.bounds.size;
        Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.y /  (float)rows);

        cellScale.x = newCellSize.x / cellSize.x;
        cellScale.y = newCellSize.y / cellSize.y;

        cellSize = newCellSize;

        cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x + transform.position.x, row * cellSize.y + gridOffset.y + transform.position.y);
                GameObject cO = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                cO.transform.parent = transform;                

                if ((row == 0) || (row == rows-1) || (col == 0) || col == cols-1) {
               // if ((cO.transform.position.x==gridOffset.x) ||  (cO.transform.position.y == gridOffset.y) || (cO.transform.position.x == -gridOffset.x) || (cO.transform.position.y == -gridOffset.y)) {
                    cO.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1.0f);
                    cO.SetActive(true);
                    cO.AddComponent<BoxCollider2D>();
                    cO.AddComponent<Rigidbody2D>();
                    cO.GetComponent<Rigidbody2D>().isKinematic=true;
                    cO.GetComponent<Rigidbody2D>().mass=10;
                    cO.tag = "EdgeBlock";
                    edgeBlocks.Add(cO);

                }
                else { 
                    cO.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value, 1.0f);
                    
                    if (Vector2.Distance(cO.transform.position, transform.position) <= 2) //new Vector2(1.0f, 1.0f)) ;
                    {
                        activatedBlocks.Add(cO);
                        cO.SetActive(true);
                        cO.AddComponent<BoxCollider2D>();
                        cO.tag = "ActivatedBlock";

                    }
                    else
                    {
                        cO.SetActive(false);
                        disactivatedBlocks.Add(cO);
                        cO.tag = "DisactivatedBlock";
                    }
                        
                }
               
            }

        }

        for (int i = 0; i < 15; i++)
        {
            int randomIndex = Random.Range(0, activatedBlocks.Count-1);
            activatedBlocks[randomIndex].SetActive(false);
            activatedBlocks[randomIndex].tag = "DisactivatedBlock";
            activatedBlocks[randomIndex].GetComponent<BoxCollider2D>().enabled = false;
            activatedBlocks.RemoveAt(randomIndex);
            disactivatedBlocks.Add(activatedBlocks[randomIndex]);
        }


        Destroy(cellObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}

