using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() {

        SceneManager.LoadScene(1); //Wenn Play gedrückt wird, wird auf die nächste Scene gesprungen
        GameplayManager gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        //0 ist 8ball
        gameplayManager.setGameMode(0);
        gameplayManager.setBestOf(3);

    }

    public string giveScene(){

        return SceneManager.GetActiveScene() + " :";

    }

    public void Quit() {
        Debug.Log("Player left the game!");
        Application.Quit();
    }
}
