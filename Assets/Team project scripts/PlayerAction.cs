using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    InputAction _moveAction;
    InputAction _damageAction;
    InputAction _attackAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    public bool IsMoving()
    {
        return _moveAction.IsPressed();
    }

    public bool IsHitting()
    {
      return _attackAction.IsPressed();
    }
   
}
