using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject PauseCanvas;
    void start() {
        Time.timeScale =1f;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                play();
            } else {
                stop();
            }
        }
    }
    void stop() {
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void play() {
       PauseCanvas.SetActive(false);
       Time.timeScale = 1f;
       paused = false; 
    }

    public void MainMenuButton() {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
