using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Pet : MonoBehaviour
{
  [SerializeField] protected string? Name;
  [SerializeField] protected int MaxHappiness;    
  [SerializeField] protected int MaxHunger;
  [SerializeField] protected int _currentHappiness;
  [SerializeField] protected int _currentHunger;
  [SerializeField] protected int _stamina;
  [SerializeField] protected int _power;
  [SerializeField] protected int _agility;
  [SerializeField] protected int _health;
  [SerializeField] protected int _intelligence;
  [SerializeField] protected float _timerLength;
  [SerializeField] protected float _weight;
  [SerializeField] protected Sprite _icon;
  [SerializeField] private SpriteRenderer _sprite;
    public int Stamina { get => _stamina; set => _stamina = value;}
    public int Power { get => _power; set => _power = value;}
    public int Agility { get => _agility; set => _agility = value;}
    public int Health { get => _health; set => _health = value;}
    public int Intelligence { get => _intelligence; set => _intelligence = value; }

    public float Weight { get =>_weight; set => _weight = value;}

    public float TimerLength { get => _timerLength; set => _timerLength = value; }
    public int Happiness { get => _currentHappiness; set => _currentHappiness = Mathf.Clamp(value, int.MinValue, MaxHappiness); }
    public int Hunger { get => _currentHunger; set => _currentHunger = Mathf.Clamp(value, 0, int.MaxValue); }
    

    public abstract void DoInteractions(IInteractions interactions);

    public Pet(int maxHappiness, int maxHunger, int startingHappiness, int startingHunger, float timerLength, int power, int agility, int health, int intelligence,int stamina, float weight) {
        MaxHappiness = maxHappiness;
        Happiness = startingHappiness;
        MaxHunger = maxHunger;
        Hunger = startingHunger;
        TimerLength = timerLength;
        Power = power;
        Agility = agility;
        Health = health;
        Intelligence = intelligence;
        Stamina = stamina;
        Weight = weight;
    }

    public virtual void Update() {}
    public enum AIStates {Idle}

   // public abstract void Consume(IFood food) { }
   // public abstract void Interact(IInteractions interactions) { }
}
