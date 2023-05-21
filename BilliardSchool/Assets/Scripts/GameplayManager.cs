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
    [SerializeField] private float ballGroup_zOffset;   //Den Offset, wie tief bzw wie weit vorne die Kugeln beim Start des Spiels sein sollen.
    
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

    private bool checkIfBallFull(int n){
        //Denk dran dass diese Funktion auch einen true/false wert für die weiße Kugel zurückgibt. 
        
        //n < 9 geht auch, die schwarze ist auch eine volle kugel
        if(n < 8){
            return true;
        }
        else{
            return false;
        }
    }

    private bool invertBool(bool b){
        if(b){
            return false;
        }
        else{
            return true;
        }
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
            balls_8ball[i].position = poolTable_tf.position + new Vector3(0, ballOnTableY(), 40);
        }

        switch(gameMode){
            case 0:
                
                int safetyIndex = 0;

                for(int i = 0; i < 16; i++){
                    balls_8ball[i].localScale = ballSize[0];
                }
                Transform balls_8ball_row0;

                Transform[] balls_8ball_row1 = new Transform[2];
                
                Transform[] balls_8ball_row2 = new Transform[3];
                
                Transform[] balls_8ball_row3 = new Transform[4];
                
                Transform[] balls_8ball_row4 = new Transform[5];

                bool isBallFull;
                int rnd;
                int index;
                bool[] ballIsUntaken = new bool[16];
                for(int i = 0; i < 16; i++){
                    ballIsUntaken[i] = true;
                }

                //Fängt mit der ersten Kugel (ganz vorne) an
                while(true){
                    //Die weiße Kugel darf nicht mitgezählt werden
                    rnd = Random.Range(1,16);
                    //Die schwarze auch nicht
                    if(rnd != 8){
                        balls_8ball_row0 = balls_8ball[rnd];
                        ballIsUntaken[rnd] = false;
                        if(rnd < 8){
                            isBallFull = true;
                        }
                        else{
                            isBallFull = false;
                        }
                        break;
                    }
                }

                index = 0;
                while(true){
                    //Die weiße Kugel darf nicht mitgezählt werden
                    rnd = Random.Range(1,16);
                    if((rnd != 8) && (checkIfBallFull(rnd) == isBallFull) && ballIsUntaken[rnd]){
                        balls_8ball_row1[index] = balls_8ball[rnd];
                        ballIsUntaken[rnd] = false;
                        //nächste Ballart (Voll oder Halb) invertieren
                        isBallFull = invertBool(isBallFull);
                        index++;
                        if(index == 2) break;
                    }
                }

                index = 0;
                while(true){
                    //Die weiße Kugel darf nicht mitgezählt werden
                    rnd = Random.Range(1,16);
                    safetyIndex++;
                    if(index == 1){
                        balls_8ball_row2[index] = balls_8ball[8];
                        index++;
                    }
                    else{
                        if((rnd != 8) && (checkIfBallFull(rnd) != isBallFull) && ballIsUntaken[rnd]){
                            balls_8ball_row2[index] = balls_8ball[rnd];
                            ballIsUntaken[rnd] = false;
                            //nächste Ballart (Voll oder Halb) invertieren
                            isBallFull = invertBool(isBallFull);
                            index++;
                            if(index == 3) break;
                        }
                    }
                    
                }
                index = 0;
                while(true){
                    //Die weiße Kugel darf nicht mitgezählt werden
                    rnd = Random.Range(1,16);
                    if((rnd != 8) && (checkIfBallFull(rnd) != isBallFull) && ballIsUntaken[rnd]){
                        balls_8ball_row3[index] = balls_8ball[rnd];
                        ballIsUntaken[rnd] = false;
                        //nächste Ballart (Voll oder Halb) invertieren
                        if((index == 0) || (index == 2)){
                            isBallFull = invertBool(isBallFull);    
                        }
                        index++;
                        if(index == 4) break;
                    }
                }

                bool firstCornerBall_Form = isBallFull;
                index = 0;
                while(true){
                    //Die weiße Kugel darf nicht mitgezählt werden
                    rnd = Random.Range(1,16);
                    if((rnd != 8) && (checkIfBallFull(rnd) != isBallFull) && ballIsUntaken[rnd]){
                        balls_8ball_row4[index] = balls_8ball[rnd];
                        ballIsUntaken[rnd] = false;
                        //Der Bereich zwischen den 5 Kugeln geht "an/aus" (zwischen 0 und 4)
                        if((0 < index) && (index < 4)){
                            isBallFull = invertBool(isBallFull);
                        }
                        else{
                            //Der letzte Ball soll das Gegenteil von dem Ball der anderen Ecke sein
                            if(index != 0)  isBallFull = !firstCornerBall_Form;
                        }
                        index++;
                        if(index == 5) break;
                    }
                
                }


                //Zufällige Rotationen
                for(int i = 0; i < 16; i++){
                    balls_8ball[i].localEulerAngles = new Vector3(Random.Range(0f,360f), Random.Range(0f,360f), Random.Range(0f,360f));
                }

                balls_8ball[0].position = poolTable_tf.position + new Vector3(0, ballOnTableY(), -20);
                balls_8ball_row0.position = poolTable_tf.position + new Vector3(0, ballOnTableY(), ballGroup_zOffset);
                for(int i = 0; i < 2; i++){
                    balls_8ball_row1[i].position = poolTable_tf.position + new Vector3(0.5f*balls_8ball_row3[i].localScale.x-balls_8ball_row3[i].localScale.x*(i), ballOnTableY(), ballGroup_zOffset + 0.5f*balls_8ball_row1[i].localScale.x/Mathf.Tan(30 * Mathf.PI / 180));
                }
                for(int i = 0; i < 3; i++){
                    balls_8ball_row2[i].position = poolTable_tf.position + new Vector3(balls_8ball_row3[i].localScale.x-balls_8ball_row3[i].localScale.x*(i), ballOnTableY(), ballGroup_zOffset + balls_8ball_row2[i].localScale.x/Mathf.Tan(30 * Mathf.PI / 180));
                }
                for(int i = 0; i < 4; i++){
                    balls_8ball_row3[i].position = poolTable_tf.position + new Vector3(1.5f*balls_8ball_row3[i].localScale.x-balls_8ball_row3[i].localScale.x*(i), ballOnTableY(), ballGroup_zOffset + 1.5f*balls_8ball_row3[i].localScale.x/Mathf.Tan(30 * Mathf.PI / 180));
                }
                for(int i = 0; i < 5; i++){
                    balls_8ball_row4[i].position = poolTable_tf.position + new Vector3(2f*balls_8ball_row4[i].localScale.x-balls_8ball_row4[i].localScale.x*(i), ballOnTableY(), ballGroup_zOffset + 2f*balls_8ball_row4[i].localScale.x/Mathf.Tan(30 * Mathf.PI / 180));
                }
                break;
            case 1:

                break;

        }

        //Hier nur weil es noch kein "Launcher" für das spiel gibt
        gameMode = 0;
    }

    void Update()
    {
        
    }
}
