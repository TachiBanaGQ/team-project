using UnityEditorInternal;
using UnityEngine;

public class PlayerMoveTrigger : MonoBehaviour
{
    //Public GeneralActions generalaction;
    [SerializeField] public Player player;
    [SerializeField] public Animator animator;
    [SerializeField] public bool Idle;
    [SerializeField] public bool Iswalking;
    [SerializeField] public bool IsAttacking;

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
