using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    int width = 5, height = 5;

    [SerializeField]
    Tile tilePrefab;

    List<List<Tile>> boardTiles = new List<List<Tile>>();


    // Start is called before the first frame update
    void Start()
    {
     
        CreateBoard();
    }

    void CreateBoard()
    {
        float widthOffset = width / 2;
        float heightOffset = height / 2;

        for (int i = 0; i < height; i++)
        {
            float posY = i - heightOffset;

            List<Tile> newBoardRow = new List<Tile>();

            for (int j = 0; j < width; j++)
            {
                
                float posX = j - widthOffset;

                // create new tile
                Tile newTile = Instantiate(tilePrefab, new Vector2(posX, posY), Quaternion.identity);
                newTile.transform.SetParent(transform);
                newTile.name = $"Tile {i} {j}";


                // tint alternate ones
                bool tinted = (i % 2 == 0 && j % 2 != 0) || (j % 2 == 0 && i % 2 != 0);
                newTile.OnCreate(tinted);

                // store the new tile
                newBoardRow.Add(newTile);
            }

            boardTiles.Add(newBoardRow);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
      //  DetectBoardInteraction();
    }

    //void DetectBoardInteraction()
    //{
    //    Tile currentTile = null;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D[] hits;

    //    hits = Physics2D.RaycastAll(ray.origin, ray.direction);

    //    foreach (RaycastHit2D hit in hits)
    //    {
    //        currentTile = hit.transform.GetComponent<Tile>();
    //        if (currentTile)
    //        {
    //            currentTile.MouseOver();
    //        }
    //        else
    //        {
    //            currentTile = null;
    //        }
    //    }

    //    foreach(List<Tile> row in boardTiles)
    //    {
    //        foreach(Tile tile in row)
    //        {
              
    //            if (currentTile != null && tile != currentTile)
    //            {
    //                tile.MouseExit();
    //            }
    //        }
    //    }
    //}
}
