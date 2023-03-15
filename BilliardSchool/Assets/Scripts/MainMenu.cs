using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //Wenn Play gedrückt wird, wird auf die nächste Scene gesprungen
    }
    public void Quit() {
        Application.Quit();
        Debug.Log("Player left the game!");
    }
}
