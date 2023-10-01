using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimal : MonoBehaviour
{
    bool isDrag = false;

    PetState petState = PetState.Inventory; 

    [SerializeField]
    BoxCollider2D collider;

    [SerializeField]
    List<Vector2> movablePositions = new List<Vector2>();

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

    public void ResetToOriginalPosition()
    {
        isDrag = false;
        collider.enabled = true;
        transform.position = startPosition;
    }

    public void SetState(PetState state)
    {
        petState = state;

        if(state == PetState.OnBoard)
        {
            collider.enabled = false;
            isDrag = false;
        }
    }


}

public enum PetState { Inventory, OnBoard}
