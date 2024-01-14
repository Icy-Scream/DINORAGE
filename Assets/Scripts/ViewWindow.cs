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
    private Monster _monsterStoreView;
    private void OnEnable() {
        _petStore.OnMenuSelect += _petStore_OnMenuSelect;
    }

    private void _petStore_OnMenuSelect(object sender, PetStore.MonsterChosenEventArgs sentOverPet) { 
        if(_monsterStoreView != null) {
                Destroy(_monsterStoreView.gameObject);
                _monsterStoreView = null;
            }

            _monsterStoreView = Instantiate(sentOverPet.chosenPet, _spawnLocation.transform.position,Quaternion.identity);
        
        if(_monsterStoreView != null) {
            _stamina.text = "STAM: " + _monsterStoreView.GetStam().ToString();
            _power.text = "POW: " + _monsterStoreView.GetPower().ToString();
            _agility.text = "SPD: " +  _monsterStoreView.GetAgility().ToString();
            _intelligence.text = "INT: " + _monsterStoreView.GetIntelligence().ToString();
            _health.text = "HP: " + _monsterStoreView.MaxHp.ToString();
            _monsterStoreView.GetComponent<Monster>().enabled = false;
            _monsterStoreView.GetComponent<Monster>().GetComponentInChildren<MonsterUI>().enabled = false;
            _monsterStoreView.GetComponent<Rigidbody2D>().gravityScale = 0;
            _monsterStoreView.GetComponent<Transform>().localScale = new Vector3(3,3,3);
        }
    }
}
