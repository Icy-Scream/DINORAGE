using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    ActionMap _inputActions;

    private void Awake() {
        _inputActions = new ActionMap();
    }

    private void OnEnable() {
        _inputActions.Enable();
        _inputActions.Player.Grab.performed += Grab_performed;
    }

    private void Grab_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        throw new System.NotImplementedException();
    }
}
