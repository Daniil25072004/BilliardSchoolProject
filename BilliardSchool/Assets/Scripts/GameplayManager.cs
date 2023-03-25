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
    private Transform[] balls_8ball = new Transform[16];
    private int gameMode = 0;   //0 = 8ball, 1 = snooker
    private Vector3[] ballSize = new Vector3[2];
    
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

        balls_8ball[0] = whiteBall_tf;
        balls_8ball[2] = ball2_tf;
        balls_8ball[3] = ball3_tf;
        balls_8ball[4] = ball4_tf;
        balls_8ball[5] = ball5_tf;
        balls_8ball[6] = ball6_tf;
        balls_8ball[7] = ball7_tf;
        balls_8ball[8] = ball8_tf;
        balls_8ball[9] = ball1_half_tf;
        balls_8ball[10] = ball2_half_tf;
        balls_8ball[11] = ball3_half_tf;
        balls_8ball[12] = ball4_half_tf;
        balls_8ball[13] = ball5_half_tf;
        balls_8ball[14] = ball6_half_tf;
        balls_8ball[15] = ball7_half_tf;

        for(int i = 0; i < 15; i++){
            balls_8ball[i].localScale = ballSize[0];
        }

    }

    void Update()
    {
        
    }
}