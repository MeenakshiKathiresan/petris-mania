using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimal : MonoBehaviour
{
    bool isDrag = false;

    [SerializeField]
    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isDrag)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            collider.enabled = true;
        }
    }

    private void OnMouseDown()
    {
        isDrag = true;
        Debug.Log("mouse down");
        collider.enabled = false;
    }






}
