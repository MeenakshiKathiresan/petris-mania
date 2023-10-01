using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimal : MonoBehaviour
{
    /*
    Key Parameters:
        - pet state
        - movable positions
        - path of movement
        - myTiles - the tiles that pet animal owns
      
     Responsible for:
        - Drag of pet animal - (Disable collider on pick and enable collider if dropped and not placeable)
        - Set current pet animal to the game manager
        - Handle dropped on board and pet state
     */

    bool isDrag = false;

    PetState petState = PetState.Inventory; 

    [SerializeField]
    BoxCollider2D collider;

    [SerializeField]
    Color tileColor;

    [SerializeField]
    List<Vector2> movablePositions = new List<Vector2>();

    List<Tile> tiles = new List<Tile>();

    Vector2 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (petState == PetState.Inventory)
        {
            if (isDrag)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        if (petState == PetState.Inventory)
        {
            isDrag = true;
            collider.enabled = false;
            GameManager.Instance.SetCurrentPetAnimal(this);
        }
    }

    public List<Vector2> GetMovablePositions()
    {
        return movablePositions;
    }

    public void DroppedOnBoard(bool placeable)
    {
        if (placeable)
        {
            SetState(PetState.OnBoard);
        }
        else
        {
            ResetToOriginalPosition();
        }
    }

    void ResetToOriginalPosition()
    {
        isDrag = false;
        collider.enabled = true;
        transform.position = startPosition;
    }

    void SetState(PetState state)
    {
        petState = state;

        if(state == PetState.OnBoard)
        {
            collider.enabled = false;
            isDrag = false;
        }
    }

    public Color GetColor()
    {
        return tileColor;
    }

    private void SetTiles(List<Tile> tilesOccupied)
    {
        tiles = tilesOccupied;
    }

    public List<Tile> GetTiles()
    {
        return tiles;
    }


}

public enum PetState { Inventory, OnBoard}
