using System;
using System.Collections;
using UnityEngine;

public abstract class Monster : MonoBehaviour {

    public class MonsterChosenEventArgs : EventArgs {
        public Monster chosenPet { get; }
        public MonsterChosenEventArgs(Monster chosenMonster) => this.chosenPet = chosenMonster;
    }

    [SerializeField] protected string Name;
    [SerializeField] protected int MaxHappiness;
    [SerializeField] protected int MaxHunger;
    [SerializeField] protected int _currentHappiness;
    [SerializeField] protected int _currentHunger;
    [SerializeField] protected int _stamina;
    [SerializeField] protected int _power;
    [SerializeField] protected int _agility;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _intelligence;
  
    [SerializeField] protected float _timerLength;
    [SerializeField] protected float _weight;
    
    [SerializeField] private MonsterUI _monsterUI;
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected MonsterAnimation _monsterAnimator;
    
    [SerializeField] protected Transform _attackDirection;
    [SerializeField] protected AISTATE _currentState;
    [SerializeField] protected GameObject _selectionVisual;
    


    public event EventHandler<MonsterChosenEventArgs> OnDeathEvent;
    public GoodorEvil goodorEvil { get; set; }
    public int MaxHp { get; protected set; }
    public bool IsDead => _hp < 0;
    public int Happiness { get => _currentHappiness; set => _currentHappiness = Mathf.Clamp(value, int.MinValue, MaxHappiness); }
    public int Hunger { get => _currentHunger; set => _currentHunger = Mathf.Clamp(value, 0, int.MaxValue); }


    protected float _attackRadius = 0.8f;

    public GameObject AttackTarget { get; private set; }
    
    public void Init() {
        _monsterUI.GetSlider(0).value = MaxHunger;
        _monsterUI.GetSlider(1).value = MaxHappiness;
        _currentState = AISTATE.Wandering;
        StartCoroutine(TimerRoutine());
    }

    public abstract void AIStateMachine();

    private IEnumerator TimerRoutine() {
        while (true) {
            yield return new WaitForSecondsRealtime(_timerLength);

        }
    }

    public void Death() {
        if (IsDead) {
            OnDeathEvent?.Invoke(this,new MonsterChosenEventArgs(this));
            if(goodorEvil == GoodorEvil.Good)
             Destroy(_monsterUI.gameObject);
            Destroy(this.gameObject,0.5f);
        }
    }

    public void GetAttackVector(GameObject monsterTarget) {
        AttackTarget =  monsterTarget;
    }

    public int GetIntelligence() {
        return _intelligence;
    }

    public int GetAgility() {
        return _agility;
    }

    public int GetStam() {
        return _stamina;
    }

    public int GetPower() {
        return _power;
    }

    public void EnableSelectionVisual() {
        _selectionVisual.SetActive(true);
    }

    public void DisableSelectionVisual() {
        _selectionVisual.SetActive(false);
    }

    public enum GoodorEvil { Good,Evil }
    public enum AISTATE {Wandering,Chasing,Attacking}
}
