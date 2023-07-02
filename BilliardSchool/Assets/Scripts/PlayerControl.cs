using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private static string hitKey = "b";
    private static string cameraKey = "a";
    private static string placeKey = "space";
    private static int bestOf;
    private static float volume = 0;
    private static bool isInKeyCode = false;
    private static string[] possibleKeyCodes = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
    public static void setHitKey(string s){
        if(getIsInKeyCode(s) == true) {
            hitKey = s;
        }
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
        if(getIsInKeyCode(s) == true) {
            cameraKey = s;
        }
    }
    public static string getCameraKey(){
        return cameraKey;
    }
    public static void setPlaceKey(string s){
        if(getIsInKeyCode(s) == true) {
            placeKey = s;
        } else {
            placeKey = "space";
        }
    }
    public static string getPlaceKey(){
        return placeKey;
    }
    public static void setBestOf(int pBestOf) {
        bestOf = pBestOf;
    }
    public static int getBestOf() {
        return bestOf;
    }
    private static bool getIsInKeyCode(string pKeyCode) {
        for(int i = 0; i < possibleKeyCodes.Length; i++) {
            if(pKeyCode == possibleKeyCodes[i]) {
                isInKeyCode = true;
                break;
            } else {
                isInKeyCode = false;
            }
        }
        return isInKeyCode;
    }
}
