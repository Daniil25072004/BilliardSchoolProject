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
    private bool changeHitKeyPressed = false;
    private bool changeCameraKeyPressed = false;
    public AudioMixer audio;
    Resolution[] resolutions;
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
    }
    void Update() {
        if(changeHitKeyPressed) {
            if(Input.anyKeyDown) {
              if(cameraKey != Input.inputString) {
                hitKey = Input.inputString;
                changeHitKeyPressed = false; 
                Debug.Log("Key:"+hitKey+" pressed");
              }
            }
        }
        if(changeCameraKeyPressed) {
            if(Input.anyKeyDown) {
               if(hitKey != Input.inputString) {
               cameraKey = Input.inputString;
               changeCameraKeyPressed = false; 
               Debug.Log("Key:"+cameraKey+" pressed");
               }
            }
        }
    }
    public string getHitKey() {
        return hitKey;
    }
    public string getCameraKey() {
        return cameraKey;
    }
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
        changeHitKeyPressed = true;
    }
    public void ChangeCameraKey() {
        changeCameraKeyPressed = true;
    }
    public void SetVolume(float pVolume) {
        audio.SetFloat("Volume", pVolume);
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
}
