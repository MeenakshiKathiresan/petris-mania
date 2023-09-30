using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    Color baseColor, tintedColor;

    [SerializeField]
    SpriteRenderer sprite;

    bool isAvailable;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreate(bool tinted)
    {
        if (tinted)
        {
            sprite.color = tintedColor;
        }
        else
        {
            sprite.color = baseColor;
        }
    }


    private void OnMouseOver()
    {
        Color fainted = sprite.color;
        fainted.a = 0.2f;
        sprite.color = fainted;
    }

    private void OnMouseExit()
    {
        Color unfainted = sprite.color;
        unfainted.a = 1f;
        sprite.color = unfainted;
    }
}
