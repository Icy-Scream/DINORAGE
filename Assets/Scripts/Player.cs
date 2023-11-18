using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerLog log;
   [SerializeField] private List<Pet> petList = new List<Pet>();

    void Start()
    {
        log = FindObjectOfType<PlayerLog>().GetComponent<PlayerLog>();
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPet(Pet pet) {
        petList.Add(pet);
        log.AddEvent($"Player purchased a new Pet: {pet}");
    }
}
