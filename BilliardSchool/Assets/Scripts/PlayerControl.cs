using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private static string hitKey = "b";
    private static string cameraKey = "a";
    private static int bestOf;
    private static float volume = 0;
    private bool changeHitKeyPressed = false;
    private bool changeCameraKeyPressed = false;
    public static void setHitKey(string s){
        hitKey = s;
    }
    public static void setVolume(float pVolume) {
        volume = pVolume;
    }
    public static float getVolume() {
        return volume;
    }
    public static string getHitKey(){
        return hitKey;
    }

    public static void setCameraKey(string s){
        cameraKey = s;
    }
    public static string getCameraKey(){
        return cameraKey;
    }
    public static void setBestOf(int pBestOf) {
        bestOf = pBestOf;
    }
    public static int getBestOf() {
        return bestOf;
    }
}
