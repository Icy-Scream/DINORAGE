using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewWindow : MonoBehaviour
{
    [SerializeField] private PetStore _petStore;
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private TMP_Text _stamina;
    [SerializeField] private TMP_Text _power;
    [SerializeField] private TMP_Text _agility;
    [SerializeField] private TMP_Text _intelligence;
    [SerializeField] private TMP_Text _health;
    private Pet _showPet;
    private void OnEnable() {
        _petStore.OnMenuSelect += _petStore_OnMenuSelect;
    }

    private void _petStore_OnMenuSelect(object sender, PetStore.PetChosenEventArgs sentOverPet) { 
        if(_showPet != null) {
                Destroy(_showPet.gameObject);
                _showPet = null;
            }

            _showPet = Instantiate(sentOverPet.chosenPet, _spawnLocation.transform.position,Quaternion.identity);
        
        if(_showPet != null) {
            _stamina.text = "STAM: " + _showPet.Stamina.ToString();
            _power.text = "POW: " + _showPet.Power.ToString();
            _agility.text = "SPD: " +  _showPet.Agility.ToString();
            _intelligence.text = "INT: " + _showPet.Intelligence.ToString();
            _health.text = "HP: " + _showPet.Health.ToString();
            _showPet.GetComponent<Pet>().enabled = false;
            _showPet.GetComponent<Rigidbody2D>().gravityScale = 0;
            _showPet.GetComponent<Transform>().localScale = new Vector3(3,3,3);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
