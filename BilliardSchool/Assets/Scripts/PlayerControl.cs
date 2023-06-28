using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private static string hitKey = "b";
    private static string cameraKey = "a";
    public static void setHitKey(string s){
        hitKey = s;
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

}
