using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    // The render mode i went with is a rather soft and simple look where the text looked rather bold
   public void PlayGame()
    {
        //Use to call the in game scene from the title screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //use to activate the Quit function on the loading screen tho as a pseudo test function
        Debug.Log("Quit");
        Application.Quit();
    }
}
