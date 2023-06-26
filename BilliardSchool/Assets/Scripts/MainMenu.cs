using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class MainMenu : MonoBehaviour
{
    private string hitKey = "b";
    private string cameraKey = "a";
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
    public void ChangeHitKey() {
        if(Input.anyKeyDown) {
        hitKey = Input.inputString;
        Debug.Log("Key:"+hitKey+" pressed");
        }
    }
}
