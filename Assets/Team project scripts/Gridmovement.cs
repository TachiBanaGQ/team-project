using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gridmovement : MonoBehaviour
{
    //Allows you to hold down a key for movement.
    [SerializeField] private bool isRepeatedMovement = false;

    //Time in seconds to move between one grid position  and the next.
    [SerializeField] private float moveDuration = 1.0f;

    //the size of the grid 
    [SerializeField] private float gridSize = 1f;

    [SerializeField] private InputSystem_Actions InputSystem_Actions;

    public InputAction move;
    private bool isMoving = false;

    private void OnEnable()
    {
        move = InputSystem_Actions?.Player.Move;
    }
    private void Update()
    {
        if (!isMoving)
        {
            //tracking two kinds of movements
            System.Func<KeyCode, bool> inputFunction;

            //constant 
            if (isRepeatedMovement)
            {
                inputFunction = Input.GetKey;
            }
            //per key press
            else
            {
                inputFunction = Input.GetKeyDown;
            }

            //input functionality
            if (inputFunction(KeyCode.UpArrow))
            {
                StartCoroutine(Move(Vector2.up));
            }
            else if (inputFunction(KeyCode.DownArrow))
            {
                StartCoroutine(Move(Vector2.down));
            }
            else if (inputFunction(KeyCode.LeftArrow))
            {
                StartCoroutine(Move(Vector2.left));
            }
            else if (inputFunction(KeyCode.RightArrow))
            {
                StartCoroutine(Move(Vector2.right));
            }

        }


    }
    // Smooth movement between grid positions.
    private IEnumerator Move(Vector2 direction)
    {

     //Make a note of where we are and where we are going.
        isMoving = true;

        //Make a note of where we are and whewre we are going.
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        //Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while(elapsedTime < Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        //Make sure we end up excactly where we want.
        transform.position = endPosition;

        //We're no longer moving so we can accept another move input.
        isMoving = false;
    }




}
