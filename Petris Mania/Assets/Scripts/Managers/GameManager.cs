using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     Singleton class 
     
     Key Parameters:

     Responsible for:
        - Storing current pet animal
     */

    static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    PetAnimal currentPetAnimal;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        BoardManager.OnPetAnimalDropped += DroppedPetAnimal;
    }
    private void OnDisable()
    {
        BoardManager.OnPetAnimalDropped -= DroppedPetAnimal;
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void SetCurrentPetAnimal(PetAnimal petAnimal)
    {
        currentPetAnimal = petAnimal;
    }

    public void DroppedPetAnimal()
    {
        currentPetAnimal = null;
    }

    public PetAnimal GetCurrentPetAnimal()
    {
        return currentPetAnimal;
    }
}
