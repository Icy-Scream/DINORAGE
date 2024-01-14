using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PetStore : MonoBehaviour {
    public class MonsterChosenEventArgs : EventArgs {
        public Monster chosenPet { get; }
        public MonsterChosenEventArgs(Monster chosenMonster) => this.chosenPet = chosenMonster;
    }

    [SerializeField] private List<Monster> monsterStoreList = new List<Monster>();
    [SerializeField] private TMP_Dropdown _dropMonsterMenu;
    [SerializeField] private Transform _monsterSpawn;
    public event EventHandler<MonsterChosenEventArgs> OnMenuSelect;
    int chosenMenu = 0;

    private void Update() {
        if(chosenMenu != _dropMonsterMenu.value) {
            chosenMenu = _dropMonsterMenu.value;
            OnMenuSelect?.Invoke(this, new MonsterChosenEventArgs(monsterStoreList[chosenMenu]));
        }
    }

    public void PurchasePet() {
        chosenMenu = _dropMonsterMenu.value;
        var monster = Instantiate(monsterStoreList[chosenMenu], _monsterSpawn.position, Quaternion.identity);
        Player.Instance.AddPet(monster);
    }
}
