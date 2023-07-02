using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private string hitKey = "b";
    private string cameraKey = "a";
    private string placeKey = "space";
    private bool changeHitKeyPressed = false;
    private bool changeCameraKeyPressed = false;
    private bool changePositionBallKeyPressed = false;
    public GameObject blackscreen;
    public AudioMixer audio;
    public Slider volumeBar;
    Resolution[] resolutions;
    int bestOf;
    public TMPro.TMP_Dropdown resolutionDropdown;
    void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
    	List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        Data data = SaveSystem.LoadData();
        PlayerControl.setVolume(data.volume);
        PlayerControl.setCameraKey(data.cameraKey);
        PlayerControl.setHitKey(data.hitkey);
        PlayerControl.setPlaceKey(data.placeKey);
    }
    void Update() {
        volumeBar.value = PlayerControl.getVolume();
        audio.SetFloat("Volume", PlayerControl.getVolume());
        if(changeHitKeyPressed) {
            if(Input.anyKeyDown) {
              if(cameraKey != Input.inputString && placeKey != Input.inputString) {
                hitKey = Input.inputString;
                changeHitKeyPressed = false; 
                PlayerControl.setHitKey(hitKey);
                blackscreen.SetActive(false);
                Debug.Log("Key: " + hitKey + " pressed");
              }
            }
        }
        if(changeCameraKeyPressed) {
            if(Input.anyKeyDown) {
               if(hitKey != Input.inputString && placeKey != Input.inputString) {
               cameraKey = Input.inputString;
               changeCameraKeyPressed = false; 
               PlayerControl.setCameraKey(cameraKey);
               blackscreen.SetActive(false);
               Debug.Log("Key: " + cameraKey + " pressed");
               }
            }
        }
        if(changePositionBallKeyPressed) {
            if(Input.anyKeyDown) {
               if(hitKey != Input.inputString && cameraKey != Input.inputString) {
               placeKey = Input.inputString;
               changePositionBallKeyPressed = false; 
               PlayerControl.setPlaceKey(placeKey);
               blackscreen.SetActive(false);
               Debug.Log("Key: " + placeKey + " pressed");
               }
            }
        }
    }
    public void Play(int pBestOf) {
        PlayerControl.setBestOf(pBestOf);
        Debug.Log(pBestOf.ToString());
        SceneManager.LoadScene(1); //Wenn Play gedrückt wird, wird auf die nächste Scene gesprungen

    }
    public string giveScene(){

        return SceneManager.GetActiveScene() + " :";

    }

    public void Quit() {
        Debug.Log("Player left the game!");
        SaveSystem.SaveData();
        Application.Quit();
    }
    public void ChangeHitKey() {
        changeHitKeyPressed = true;
    }
    public void ChangeCameraKey() {
        changeCameraKeyPressed = true;
    }
    public void ChangePositionBallKey() {
        changePositionBallKeyPressed = true;
    }
    public void SetVolume(float pVolume) {
        audio.SetFloat("Volume", pVolume);
        PlayerControl.setVolume(pVolume);
    }
    public void SetQuality(int pIndex) {
        QualitySettings.SetQualityLevel(pIndex);
    }
    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resIndex) {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    public void Save() {
        SaveSystem.SaveData();
    }
}
