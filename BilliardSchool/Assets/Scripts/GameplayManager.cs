using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] GameObject whiteBall;
    [SerializeField] GameObject ball1;
    [SerializeField] GameObject ball2;
    [SerializeField] GameObject ball3;
    [SerializeField] GameObject ball4;
    [SerializeField] GameObject ball5;
    [SerializeField] GameObject ball6;
    [SerializeField] GameObject ball7;
    [SerializeField] GameObject ball8;
    [SerializeField] GameObject ball1_half;
    [SerializeField] GameObject ball2_half;
    [SerializeField] GameObject ball3_half;
    [SerializeField] GameObject ball4_half;
    [SerializeField] GameObject ball5_half;
    [SerializeField] GameObject ball6_half;
    [SerializeField] GameObject ball7_half;
    [SerializeField] GameObject poolTable;
    private Transform[] balls_8ball = new Transform[16];
    private int gameMode = 0;   //0 = 8ball, 1 = snooker
    private Vector3[] ballSize = new Vector3[2];
    
    private float[,] table_sizes = new float[2, 3]{{112f, 150f, 224f},{2.0f, 2.0f, 2.0f}};  
    //0 = 8pool ball, 1 = snooker
    //Es geht bei den Table Sizes um die innere Spielflächen und sie sind in cm angegeben. X ist die width und Z die Length
    private Vector3 table_vonInsideZuGesScale = new Vector3(1.194458f, 1f, 1.09678f);

    private float ballOnTableY(){

        Pooltable_script poolTable_script = poolTable.GetComponent<Pooltable_script>();
        return poolTable_script.getGameFieldHeight() + ballSize[0].y/2;

    }
    public void startGame(){    //Funktion startet ein Spiel. Spielmodus etc in den Parameter eingeben


    }

    public int getMode(){

        return gameMode;

    }

    void Start()
    {
        ballSize[0] = new Vector3(5.7f, 5.7f, 5.7f);

        Transform whiteBall_tf  = whiteBall.GetComponent<Transform>();
        Transform ball1_tf      = ball1.GetComponent<Transform>();
        Transform ball2_tf      = ball2.GetComponent<Transform>();
        Transform ball3_tf      = ball3.GetComponent<Transform>();
        Transform ball4_tf      = ball4.GetComponent<Transform>();
        Transform ball5_tf      = ball5.GetComponent<Transform>();
        Transform ball6_tf      = ball6.GetComponent<Transform>();
        Transform ball7_tf      = ball7.GetComponent<Transform>();
        Transform ball8_tf      = ball8.GetComponent<Transform>();
        Transform ball1_half_tf = ball1_half.GetComponent<Transform>();
        Transform ball2_half_tf = ball2_half.GetComponent<Transform>();
        Transform ball3_half_tf = ball3_half.GetComponent<Transform>();
        Transform ball4_half_tf = ball4_half.GetComponent<Transform>();
        Transform ball5_half_tf = ball5_half.GetComponent<Transform>();
        Transform ball6_half_tf = ball6_half.GetComponent<Transform>();
        Transform ball7_half_tf = ball7_half.GetComponent<Transform>();
        Transform poolTable_tf  = poolTable.GetComponent<Transform>();

        balls_8ball[0]  = whiteBall_tf;
        balls_8ball[1]  = ball1_tf;
        balls_8ball[2]  = ball2_tf;
        balls_8ball[3]  = ball3_tf;
        balls_8ball[4]  = ball4_tf;
        balls_8ball[5]  = ball5_tf;
        balls_8ball[6]  = ball6_tf;
        balls_8ball[7]  = ball7_tf;
        balls_8ball[8]  = ball8_tf;
        balls_8ball[9]  = ball1_half_tf;
        balls_8ball[10] = ball2_half_tf;
        balls_8ball[11] = ball3_half_tf;
        balls_8ball[12] = ball4_half_tf;
        balls_8ball[13] = ball5_half_tf;
        balls_8ball[14] = ball6_half_tf;
        balls_8ball[15] = ball7_half_tf;

        Pooltable_script poolTable_script = poolTable.GetComponent<Pooltable_script>(); //Assoziation auf den Script des Pooltables.
                                                                                        //Brauchen wir, um die Funktion "setSize" zu verwenden, die die Grösse des Tisches setzt.
        poolTable_script.setSize(0);

        for(int i = 0; i < 16; i++){
            balls_8ball[i].localScale = ballSize[0];
            balls_8ball[i].position = poolTable_tf.position + new Vector3(0, ballOnTableY(), 0);
        }
    }

    void Update()
    {
        
    }
}
