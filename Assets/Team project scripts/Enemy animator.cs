using Unity.VisualScripting;
using UnityEngine;

public class Enemyanimator : MonoBehaviour
{
    [SerializeField] EnemyAction action;
    [SerializeField] Animator animator;
    public bool Idle;
    public bool IsMoving;
    public bool IsHitting;
    public bool IsHurting;
    public bool syncAnimator = true;
  

    // Update is called once per frame
    void Update()
    {
        if(!animator || !syncAnimator)
        {
            Debug.Log("Sync error");
            return;
        }

        //movement animation
        if(action != null)
        {

        }
    }
}
