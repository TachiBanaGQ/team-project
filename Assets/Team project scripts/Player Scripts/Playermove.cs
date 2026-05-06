using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    PlayerAction _pa;
    public string _currentState;
    Vector2 _direction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _pa = GetComponent<PlayerAction>();
        _currentState = "Idle";

    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState == "Idle")
        {
            Idle();
        }
        else if (_currentState == "Moving")
        {
            Moving();
        }
        else if (_currentState == "Attacking")
        {
            Hitting();
        }
        else if(_currentState == "Hurting")
        {
            Hurting();
        }
        void Idle()
        {
            if(_direction != Vector2.zero)
            {
                _currentState = "Moving";
                return;
            }
        }
        void Moving()
        {
            if(_direction != Vector2.zero)
            {
                _currentState = "Idle";
                return ;
            }
            if (_pa.IsMoving())
            {
                _currentState = "Moving";
                return;
            }
        }
        void Hitting()
        {
            if(_direction != Vector2.zero)
            {
                _currentState = "Idle";
            }
            if (_pa.IsHitting())
            {
                _currentState = "Hitting";

                return;
            }
        }
        void Hurting()
        {
           /* if (_direction != Vector2.zero)
            {
                _currentState = "Idle";
            }
            if (_pa.IsHitting())
            {

            }*/
        }
        
    }

   
}
