using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Camera;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Monster selectedMonster;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            TryHandleSelection();
        }
    }

    private bool TryHandleSelection() {
       var hitinfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition),Mathf.Infinity, 1 << 8);
        try {
           if(hitinfo.transform.TryGetComponent<Monster>(out Monster selectedUnit)) {
                if (selectedMonster != selectedUnit && selectedMonster != null) { 
                    selectedMonster.DisableSelectionVisual();
                }
                this.selectedMonster = selectedUnit;
                selectedMonster.EnableSelectionVisual();
            }
            return true;
        }
        catch (System.NullReferenceException) {
            Debug.Log("Not a valid unit");
            return false;
        }
    }
}
