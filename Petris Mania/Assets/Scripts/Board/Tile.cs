using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    /*
     Key Parameters:
        - owner - pet animal that owns the tile

     Responsible for:
        - create tinted or base color tile
        - on mouse over -> report to board manager
        - on mouse exit -> report to board manager
        - Highlight on hover based on placeable (highlighted even if it is part of movable positions in pet Animal)
        - Handle if tile is available or not
        - Reset to default
     */

    [SerializeField]
    Color baseColor, tintedColor;

    Color originalColor;

    [SerializeField]
    Color placeableColor, nonPlaceableColor;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    GameObject currentHighlighter, occupiedTile;

    PetAnimal owner;

    bool isAvailable = true;

    int _x = 0, _y = 0;

    public void OnCreate(int x, int y, bool tinted)
    {
        if (tinted)
        {
            sprite.color = tintedColor;
        }
        else
        {
            sprite.color = baseColor;
        }
        _x = x;
        _y = y;

        originalColor = sprite.color;
    }


    private void OnMouseOver()
    {
        BoardManager.Instance.SetMouseOverTile(_x, _y);
        currentHighlighter.SetActive(true);
    }

    private void OnMouseExit()
    {
        // this shoule be changed
        BoardManager.Instance.ResetTileColor();

        BoardManager.Instance.RemoveMouseOverTile(_x, _y);
        currentHighlighter.SetActive(false);
    }


    public void HighlightTile(bool placeable)
    {
        if (placeable)
        {
            sprite.color = placeableColor;
        }
        else
        {
            sprite.color = nonPlaceableColor;
        }
    }

    public void ResetToDefault()
    {
        sprite.color = originalColor;
    }

    void SetAvailable(bool available)
    {
        isAvailable = available;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void SetTileOccupied(PetAnimal petAnimal)
    {
        owner = petAnimal;
        SetAsOccupiedTile();
        SetAvailable(false);
    }

    void SetAsOccupiedTile()
    {
        occupiedTile.GetComponent<SpriteRenderer>().color = owner.GetColor();   
        //occupiedTile.SetActive(true);
    }



    public void SetOwner(PetAnimal petAnimal)
    {
        owner = petAnimal;
    }


}
