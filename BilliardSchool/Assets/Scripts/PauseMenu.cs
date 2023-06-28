using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{
    public AudioMixer audio;
    public static bool paused = false;
    public GameObject PauseCanvas;
    public GameObject blackscreen;
    private bool changeHitKeyPressed = false;
    private bool changeCameraKeyPressed = false;
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
        if(changeHitKeyPressed) {
            if(Input.anyKeyDown) {
              if(PlayerControl.getHitKey() != Input.inputString) {
                changeHitKeyPressed = false; 
                PlayerControl.setHitKey(Input.inputString);
                blackscreen.SetActive(false);
              }
            }
        }
        if(changeCameraKeyPressed) {
            if(Input.anyKeyDown) {
               if(PlayerControl.getCameraKey() != Input.inputString) {
               changeCameraKeyPressed = false; 
               PlayerControl.setCameraKey(Input.inputString);
               blackscreen.SetActive(false);
               }
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
    public void SetVolume(float pVolume) {
        audio.SetFloat("Volume", pVolume);
    }
    public void ChangeHitKey() {
        changeHitKeyPressed = true;
    }
    public void ChangeCameraKey() {
        changeCameraKeyPressed = true;
    }
}
