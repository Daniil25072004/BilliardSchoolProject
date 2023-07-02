using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public float volume;
    public string hitkey;
    public string cameraKey;
    public string placeKey;
    
    public Data() {
        volume = PlayerControl.getVolume();
        hitkey = PlayerControl.getHitKey();
        cameraKey = PlayerControl.getCameraKey();
        placeKey = PlayerControl.getPlaceKey();
        Debug.Log("data wurde erstellt");
    }
}
