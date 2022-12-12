using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    //Loads the game
     public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    //Closes the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
