using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int ultraDamage;
    public int healDamage;

    public int maxHp;
    public int currentHP;
   
    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP<=0)
            return true; 
        else 
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHp)
        {
            currentHP = maxHp;
        }
    }

}
