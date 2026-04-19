using UnityEditorInternal;
using UnityEngine;

public class PlayerMoveTrigger : MonoBehaviour
{
    //Public GeneralActions generalaction;
    public Player player;
    public Animator animator;
    public bool Idle;
    public bool Iswalking;
    public bool IsAttacking;

    public bool syncAnimator = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!animator || !syncAnimator)
        {
            Debug.Log("Sync error");
            return;
        }

        /*if(generalaction != null)
        {
            if (Generalaction.IsWalking())
            {
                animator.SetBool("Iswalking", true);
               
            }
        } 
        */
    }
}
