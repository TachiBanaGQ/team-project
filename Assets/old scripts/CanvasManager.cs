using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject PipPrefab;
    public GameObject[] myArray;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void healthUpdate()
    {
        for (int i = 0; i < myArray.Length; i++)
        {
            if (i < player.health)
            {


                myArray[i].SetActive(true);

            }

            else
            {
                myArray[i].SetActive(false);
            }

        }



    }
}
