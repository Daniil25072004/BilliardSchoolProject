using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionDetection : MonoBehaviour
{
    [SerializeField] GameObject gameplayManager;
    private GameplayManager gameplayManager_script;
    void Start(){
        //Verbindung zum GameplayManager
        gameplayManager_script = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
    }
    //Wenn Kontakt mit der weißen Kugel entsteht, wird das dem GameplayManager mitgeteilt.
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name == "WhiteBall") gameplayManager_script.registerBallCollision(gameObject.name);
    }
}
