using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    InputAction _moveAction;
    InputAction _damageAction;
    InputAction _attackAction;

    [SerializeField] private BattleManager bm;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Encounter"))
        {
            bm.LaunchBattle(player, collision.transform.parent.gameObject);
        }
    }

}
