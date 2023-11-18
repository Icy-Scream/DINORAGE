using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveDog : MonoBehaviour, IFood {
    [SerializeField] private GameObject _liveDog;
    public string Name => "Live Dog";

    public string Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float Fat => 5f;

    public int Happiness => 2;

    public int HungerPoints => 5;
}

