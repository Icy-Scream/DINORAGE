using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : MonoBehaviour
{
    private Monster monster;
    private void Awake() {
        monster = GetComponent<Monster>();
    }

    private void Update() {
        
    }
}
