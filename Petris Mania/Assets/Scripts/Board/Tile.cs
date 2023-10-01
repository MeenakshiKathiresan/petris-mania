using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    Color baseColor, tintedColor;

    Color originalColor;

    [SerializeField]
    Color placeableColor, nonPlaceableColor;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    GameObject currentHighlighter;

    bool isAvailable = true;

    int _x = 0, _y = 0;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void ResetToDefault()
    {
        sprite.color = originalColor;
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

    private void OnMouseExit()
    {
        currentHighlighter.SetActive(false);
        BoardManager.Instance.ResetTileColor();
    }

    public void SetAvailable(bool available)
    {
        isAvailable = available;
    }


    public bool IsAvailable()
    {
        return isAvailable;
    }


}
