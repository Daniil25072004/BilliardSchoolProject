using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public AudioMixer audio;
    public static bool paused = false;
    public GameObject PauseCanvas;
    public GameObject blackscreen;
    private bool changeHitKeyPressed = false;
    private bool changeCameraKeyPressed = false;
    private bool changePositionBallKeyPressed = false;
    public Slider volumeBar;
    void start() {
        Time.timeScale =1f;
    }
    void Update() {
        volumeBar.value = PlayerControl.getVolume();
        audio.SetFloat("Volume", PlayerControl.getVolume());
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(paused) {
                play();
            } else {
                stop();
            }
        }
        if(changeHitKeyPressed) {
            if(Input.anyKeyDown) {
              if(PlayerControl.getHitKey() != Input.inputString && PlayerControl.getPlaceKey() != Input.inputString) {
                changeHitKeyPressed = false; 
                PlayerControl.setHitKey(Input.inputString);
                blackscreen.SetActive(false);
              }
            }
        }
        if(changeCameraKeyPressed) {
            if(Input.anyKeyDown) {
               if(PlayerControl.getCameraKey() != Input.inputString && PlayerControl.getPlaceKey() != Input.inputString) {
               changeCameraKeyPressed = false; 
               PlayerControl.setCameraKey(Input.inputString);
               blackscreen.SetActive(false);
               }
            }
        }
        if(changePositionBallKeyPressed) {
            if(Input.anyKeyDown) {
               if(PlayerControl.getHitKey() != Input.inputString && PlayerControl.getCameraKey() != Input.inputString) {
               changePositionBallKeyPressed = false; 
               PlayerControl.setPlaceKey(Input.inputString);
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
        PlayerControl.setVolume(pVolume);
    }
    public void ChangeHitKey() {
        changeHitKeyPressed = true;
    }
    public void ChangeCameraKey() {
        changeCameraKeyPressed = true;
    }
    public void Save() {
        SaveSystem.SaveData();
    }
    public void changePositionBallKey() {
        changePositionBallKeyPressed = true;
    }
}
