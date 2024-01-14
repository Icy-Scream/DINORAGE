
using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    ActionMap _inputActions;
    public event EventHandler OnLeftClickPerformed;
    public event EventHandler OnRightClickPerformed;
    public event EventHandler OnLeftClickCancel;
    private void Awake() {
        _inputActions = new ActionMap();
    }

    private void OnEnable() {
        _inputActions.Enable();
        _inputActions.Player.Enable();
        _inputActions.Player.LifeHeal.performed += LifeHeal_performed;
        _inputActions.Player.LifeSteal.performed += LifeSteal_performed;
        _inputActions.Player.LifeSteal.canceled += LifeSteal_canceled;
    }

    private void LifeSteal_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnLeftClickCancel?.Invoke(this, EventArgs.Empty);
    }

    private void LifeSteal_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnLeftClickPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void LifeHeal_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        
    }


    public Vector2 GetMovementVector() {
        Vector2 movementVector = _inputActions.Player.Movement.ReadValue<Vector2>();
        return movementVector.normalized;
    }

    public void StealingLife() {
        if (_inputActions.Player.LifeSteal.IsInProgress())
            Debug.Log("STEALING");
    }
}
