using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattle : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private animatorscript animscript;

    InputAction _moveAction;
    InputAction _damageAction;
    InputAction _attackAction;

    [SerializeField] private PlayerState playerState, newState;
    [SerializeField] public Animator animator;
    
   

    private void Start()
    {
        
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if (playerState != newState)
        {

            ChangeState(newState);
            UpdateAnimatior();
            Debug.Log(playerState.ToString());
        }


    }
    public PlayerState CurrentState()
    {
        return playerState;
    }

    public void ChangeState(PlayerState newState)
    {
        playerState = newState;
    }

    public void UpdateAnimatior()
    {
        //Exit the current animation
        switch (playerState)
        {

           
            case PlayerState.Hurting:
                animator.SetBool("IsHurting", true);

                break;
            case PlayerState.Hitting:
                animator.SetBool("IsHitting", true);
                break;

        }

        if (playerState != PlayerState.Idle)
        {

            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsHitting", false);
            animator.SetBool("IsHurting", false);
            animator.SetBool("IsIdle", true);

        }

        Debug.Log("checking to see if this works in the update loop");
    }

}
public enum PlayerState
{
    Idle, Hurting, Hitting
}
