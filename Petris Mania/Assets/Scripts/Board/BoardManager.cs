using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    int width = 5, height = 5;

    [SerializeField]
    Tile tilePrefab;

    bool isPlaceable;

    int mouseOverTileX, mouseOverTileY;

    List<List<Tile>> boardTiles = new List<List<Tile>>();

    static BoardManager _instance;

    public static BoardManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

    }

    void Start()
    { 
        CreateBoard();
        Debug.Log(boardTiles.Count +" "+ boardTiles[0].Count);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DropCurrentPetOnBoard();
        }
    }

    private void DropCurrentPetOnBoard()
    {
        if (GameManager.Instance.GetCurrentPetAnimal() != null)
        {
            PetAnimal currentAnimal = GameManager.Instance.GetCurrentPetAnimal();
            if (IsPlaceable(mouseOverTileX, mouseOverTileY))
            {
                // disable
                foreach (Vector2 moveablePos in currentAnimal.GetMovablePositions())
                {
                    int newPosX = (int)moveablePos.x + mouseOverTileX;
                    int newPosY = (int)moveablePos.y + mouseOverTileY;

                 
                    boardTiles[newPosY][newPosX].SetAvailable(false);
                    currentAnimal.SetState(PetState.OnBoard);
                    Debug.Log("already set! " + currentAnimal);
                }

            }
            else
            {
                Debug.Log("reset");
                currentAnimal.ResetToOriginalPosition();
            }
        }
        GameManager.Instance.DroppedPetAnimal();
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
                newTile.OnCreate(j,i,tinted);

                // store the new tile
                newBoardRow.Add(newTile);
            }

            boardTiles.Add(newBoardRow);
        }
       
    }

    public void ResetTileColor()
    {
        foreach (List<Tile> row in boardTiles)
        {
            foreach (Tile tile in row)
            {
                tile.ResetToDefault();
            }
        }
    }

    public void SetMouseOverTile(int x, int y)
    {
        // mouse over tile set here
        // based on current animal
        // find if all the movable tiles are available
        // get placeable or not
        // highlight them based on color of placeable


        ResetTileColor();

        if (GameManager.Instance.GetCurrentPetAnimal() != null)
        {
            isPlaceable = IsPlaceable(x, y);
            PetAnimal currentAnimal = GameManager.Instance.GetCurrentPetAnimal();
            foreach (Vector2 moveablePos in currentAnimal.GetMovablePositions())
            {
                int newPosX = (int)moveablePos.x + x;
                int newPosY = (int)moveablePos.y + y;
                if (newPosX < boardTiles[0].Count && newPosX >= 0 && newPosY < boardTiles.Count && newPosY >= 0)
                {
                    boardTiles[newPosY][newPosX].HighlightTile(isPlaceable);
                }
            }
        }
        mouseOverTileX = x;
        mouseOverTileY = y;


    }

    bool IsPlaceable(int x, int y)
    {

        if (GameManager.Instance.GetCurrentPetAnimal() != null)
        {
            PetAnimal currentAnimal = GameManager.Instance.GetCurrentPetAnimal();
            foreach (Vector2 moveablePos in currentAnimal.GetMovablePositions())
            {
                int newPosX = (int)moveablePos.x + x;
                int newPosY = (int)moveablePos.y + y;

                // check if new pos within bounds and new pos is available tile
                if (newPosX >= boardTiles[0].Count || newPosX < 0 || newPosY >= boardTiles.Count || newPosY < 0)
                {
                    return false;
                }
                if (!boardTiles[newPosY][newPosX].IsAvailable())
                {
                    return false;
                }

            }
        }

        return true;
    }
}
