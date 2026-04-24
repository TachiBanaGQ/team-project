using UnityEngine;

public class animatorscript : MonoBehaviour
{
    public PlayerAction playeraction;
    public Player Guy;
    public Animator animator;
    public bool Idle;
    public bool IsMoving;
    public bool IsHitting;
    public bool IsHurting;
    public bool syncAnimator = true;


    // Update is called once per frame
    void Update()
    {
        if (!animator || !syncAnimator)
        {
            Debug.Log("Sync error");
            return;
        }

        //movement animation
        if (playeraction != null)
        {
            if (playeraction.IsMoving())
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving",false);
            }
        }

    }
}
