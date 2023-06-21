using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() {

        SceneManager.LoadScene(1); //Wenn Play gedrückt wird, wird auf die nächste Scene gesprungen

    }

    public string giveScene(){

        return SceneManager.GetActiveScene() + " :";

    }

    public void Quit() {
        Debug.Log("Player left the game!");
        Application.Quit();
    }
}
