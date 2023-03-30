using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    private GameObject net;
    private HitboxNetwork_script network;
    private int number = 0;
    // Start is called before the first frame update
    public void setNumber(int n){

        number = n;

    }

    void Start(){

        net = GameObject.Find("HitboxNetwork");
        network = net.GetComponent<HitboxNetwork_script>();

    }

    private void registerHitboxCollision(string s){

        network.registerHitboxCollision(s, number);

    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.name){
            case "WhiteBall":
                registerHitboxCollision("WhiteBall");
                break;
            case "Ball1":
                registerHitboxCollision("Ball1");
                break;
            case "Ball2":
                registerHitboxCollision("Ball2");
                break;
            case "Ball3":
                registerHitboxCollision("Ball3");
                break;
            case "Ball4":
                registerHitboxCollision("Ball4");
                break;
            case "Ball5":
                registerHitboxCollision("Ball5");
                break;
            case "Ball6":
                registerHitboxCollision("Ball6");
                break;
            case "Ball7":
                registerHitboxCollision("Ball7");
                break;
            case "Ball8":
                registerHitboxCollision("Ball8");
                break;
            case "Ball1_Half":
                registerHitboxCollision("Ball1_Half");
                break;
            case "Ball2_Half":
                registerHitboxCollision("Ball2_Half");
                break;
            case "Ball3_Half":
                registerHitboxCollision("Ball3_Half");
                break;
            case "Ball4_Half":
                registerHitboxCollision("Ball4_Half");
                break;
            case "Ball5_Half":
                registerHitboxCollision("Ball5_Half");
                break;
            case "Ball6_Half":
                registerHitboxCollision("Ball6_Half");
                break;
            case "Ball7_Half":
                registerHitboxCollision("Ball7_Half");
                break;
            default:
                Debug.Log("Kontakt mit Hitbox + " + number + ", aber unbekannt mit welchem Ball.");
                break;


        }
    }
}
