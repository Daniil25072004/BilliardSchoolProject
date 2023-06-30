using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public float volume;
    public string hitkey;
    public string cameraKey;
    
    public Data() {
        volume = PlayerControl.getVolume();
        hitkey = PlayerControl.getHitKey();
        cameraKey = PlayerControl.getCameraKey();
        Debug.Log("data wurde erstellt");
    }
}
