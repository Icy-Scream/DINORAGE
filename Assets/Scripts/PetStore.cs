using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetStore : MonoBehaviour {

    [SerializeField] private List<Pet> petStoreList = new List<Pet>();
    [SerializeField] private TMP_Dropdown _dropPetMenu;
    [SerializeField] private Transform _petSpawn;
    public event EventHandler<PetChosenEventArgs> OnMenuSelect;
    int chosenMenu = 0;

    public class PetChosenEventArgs : EventArgs {
        public Pet chosenPet {get;}
        public PetChosenEventArgs(Pet chosenPet) => this.chosenPet = chosenPet;
    }
    private void Update() {
        if(chosenMenu != _dropPetMenu.value) {
            chosenMenu = _dropPetMenu.value;
            OnMenuSelect?.Invoke(this, new PetChosenEventArgs(petStoreList[chosenMenu]));
        }
    }

    public void PurchasePet() {
        chosenMenu = _dropPetMenu.value;
        var pet = Instantiate(petStoreList[chosenMenu],_petSpawn.position, Quaternion.identity);
       
        Player.Instance.AddPet(pet);
    }
}
