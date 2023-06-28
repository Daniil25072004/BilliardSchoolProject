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
    [SerializeField] private float ballGroup_zOffset;   //Den Offset, wie tief bzw wie weit vorne die Kugeln beim Start des Spiels sein sollen.
    [SerializeField] private float maxQueueForce;
    public float getMaxQueueForce(){
        return maxQueueForce;
    }
    [SerializeField] private float timeForMaxQueueForce;    //In Sekunden
    public float getTimeForMaxQueueForce(){
        return timeForMaxQueueForce;
    }
    private Transform[] balls_8ball = new Transform[16];
    private Rigidbody[] balls_8ball_rb = new Rigidbody[16];
    private GameObject firstBallHit;
    private Vector3[] ballSize = new Vector3[2];
    private int gameMode;               //0 = 8ball, 1 = snooker
    private int[] playerPoints;         //[0] = Punkte von Spieler 1        [1] = Punkte von Spieler 2
    private int playerTurn;             //idx 0 = Spieler 1         idx 1 = Spieler 2           (Wer dran ist)
    private int[] playerBall_form;      //idx 0 = Spieler 1         idx 1 = Spieler 2           Ballform:  0 = null; 1 = half; 2 = full
    private int[,] playerBall_holed;    //idx 0,? = Spieler 1       idx 1,? = Spieler 2         Der Wert ist die Nummer der Kugel. 0 bedeutet keine Kugel
    private int[] temp_holedBalls;
    private int bestOf;         //Wie oft muss ein Spieler gewinnen, dass ein Spieler komplett gewonnen hat?
    private int round;          //Welche Runde wird gerade gespielt
    private bool playerIsAllowedToMove; //darf ein Spieler die Kugel schießen
    private float[,] table_sizes = new float[2, 3] { { 112f, 150f, 224f }, { 2.0f, 2.0f, 2.0f } };
    //0 = 8pool ball, 1 = snooker
    //Es geht bei den Table Sizes um die innere Spielflächen und sie sind in cm angegeben. X ist die width und Z die Length
    private Vector3 table_vonInsideZuGesScale = new Vector3(1.194458f, 1f, 1.09678f);

    private void resetTemp_holedBalls(){
        for(int j = 0; j < 16; j++){
            temp_holedBalls[j] = -1;                //Leere Werte im index sollen -1 sein
        }
    }
    private bool areTheBallsStandingStill(){
        bool standingStill = true;
        for(int i = 0; i < 16; i++){
            //Berechnet die Geschwindigkeit aller Kugeln. Wenn sie alle unter 1 ist dann "bleiben alle stehen"
            if( Mathf.Sqrt(Mathf.Pow(balls_8ball_rb[i].velocity.x,2f) + Mathf.Pow(balls_8ball_rb[i].velocity.z,2f)) > 1f){
                standingStill = false;
            }
        }
        return standingStill;
    }

    public bool getPlayerIsAllowedToMove(){
        return playerIsAllowedToMove;
    }
    public void shootWhiteBall(Vector3 vel){
        if(playerIsAllowedToMove){
            balls_8ball_rb[0].velocity = vel;
            resetTemp_holedBalls();
            playerIsAllowedToMove = false;
        }
    }

    private float ballOnTableY()
    {

        Pooltable_script poolTable_script = poolTable.GetComponent<Pooltable_script>();
        return poolTable_script.getGameFieldHeight() + ballSize[0].y / 2;

    }
    public void startGame()
    {    //Funktion startet ein Spiel, abhängig von Gamemode
        switch(gameMode){
            case 0:
                eightBall_resetBallPosition();
                eightBall_resetGameLogic();
                break;
            case 1:
                break;   
        }
    }

    public void setGameMode(int m){
        gameMode = m;
    }
    public void setBestOf(int n){
        bestOf = n;
    }
    public int getMode()
    {
        return gameMode;
    }

    private int getBallIdx(string s){
        GameObject obj = GameObject.Find(s);
        int idx = 0;
        for(int i = 0; i < balls_8ball.Length; i++){
            if(balls_8ball[i].name == obj.name){
                idx = i;
                break;
            }
        }
        return idx;
    }
    private bool checkIfBallFull(int n)
    {
        //Denk dran dass diese Funktion auch einen true wert für die weiße Kugel zurückgibt. 

        //n < 9 geht auch, die schwarze ist auch eine volle kugel
        if (n < 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool invertBool(bool b)
    {
        if (b)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private int getOtherPlayerIdx(int pPlayerTurn){
        if(pPlayerTurn == 0){
            return 1;
        }
        else{
            return 0;
        }
    }

    //Gibt zurück, welche Form der Ball von idx in balls_8ball hat.
    private int getBallForm(int idx){
        int form = 0;
        //Nicht die weiße weder noch die schwarze Kugel. Mindestens eine Volle Kugel
        if(idx > 0 && idx != 8){
            //Eine volle Kugel
            form = 2;
        }
        //Die Kugeln im Array nach der schwarzen Kugel sind alle halbe Kugeln
        if(idx > 8){    
            //Eine halbe Kugel
            form = 1;
        }

        return form;
    }
    private bool isHoledFormCorrect(){
        bool correctForm = true;
        //Halbe Kugeln
        if(playerBall_form[playerTurn] == 1){
            for(int i = 0; i < 7; i++){
                //Es geht hier nur darum, ob keine Kugel der anderen Form enthalten ist!    Weiß oder Schwarz ist hier erstmal egal
                if(((7 < playerBall_holed[playerTurn,i]) == false) && playerBall_holed[playerTurn,i] != 0 && playerBall_holed[playerTurn,i] != -1){
                    correctForm = false;
                }
            }
        }
        else{   //Volle Kugeln
            for(int i = 0; i < 7; i++){
                //Es geht hier nur darum, ob keine Kugel der anderen Form enthalten ist!    Weiß oder Schwarz ist hier erstmal egal
                if((playerBall_holed[playerTurn,i] < 9) == false && playerBall_holed[playerTurn,i] != 0){
                    correctForm = false;
                }
            }
        }
        //Wenn die Formen noch undefiniert (0) sind, gibt die Funktion bei einlochung von der weißen und schwarzen Kugel ein true wieder. ansonsten immer ein false
        return correctForm;
    }
    //idx 0 = white; idx 1 = black; Der Wert -1 bedeutet, dass die Kugel garnicht drinnen ist. Ansonsten ist der rauskommende Wert ein index für die Position im playerBall_holed der Kugel.
    private int[] getWhiteAndBlackIdxInArray(int pPlayerTurn){
        int[] whiteAndBlack = new int[2];
        int whiteIdx = -1;
        int blackIdx = -1;
        for(int i = 0; i < getAmountOfHoledBalls(playerTurn); i++){
            if(playerBall_holed[pPlayerTurn, i] == 0){
                whiteIdx = i;
            }
            if(playerBall_holed[pPlayerTurn, i] == 8){
                blackIdx = i;
            }
        }
        whiteAndBlack[0] = whiteIdx;
        whiteAndBlack[1] = blackIdx;
        return whiteAndBlack;
    }

    private void removeBallFromTable(int idx){
        //Freeze Ball
        balls_8ball_rb[idx].constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        balls_8ball[idx].localPosition = new Vector3(0, ballOnTableY(), -20);
    }

    //Diese Funktion wird unendlich fortfahren, wenn der Array voll mit Kugeln ist !!!
    private int getAmountOfHoledBalls(int pPlayerTurn){
        int idxOfArray = 0;
        while(playerBall_holed[pPlayerTurn, idxOfArray] != -1 && idxOfArray < 7){
            idxOfArray++;
        }
        return idxOfArray;
    }
    private void addHoledBallToTempArray(int idx){
        playerBall_holed[playerTurn, getAmountOfHoledBalls(playerTurn)] = idx;
    }
    private void addHoledBall(int idx){
        playerBall_holed[playerTurn, getAmountOfHoledBalls(playerTurn)] = idx;
    }

    private void removeHoledBall(){ //Löscht den zuletzt hinzugefügten Ball von playerBall_holed
        playerBall_holed[playerTurn, getAmountOfHoledBalls(playerTurn)-1] = -1;
    }

    private void switchPlayerTurn(){
        if(playerTurn == 1){
            playerTurn = 0;
        }
        else{
            playerTurn = 1;
        }
    }

    private void moveWhiteBall(){
    
    }
    private void addPoint(int player){
        playerPoints[player]++;
    }
    //Eine Hitbox registriert, dass eine Kugel in ein Loch gefallen ist
    public void registerHitboxCollision(string s, int hitbox)
    {
        GameObject ball = GameObject.Find(s);
        if(firstBallHit == null){
            firstBallHit = ball;
        }
        switch(gameMode){
            case 0:
                eightBall_registerHitboxCollision(s, hitbox);
                break;
            case 1:
                break;
        }
    }
    private void eightBall_registerHitboxCollision(string s, int hitbox){

        //Merke dir die gefallenen Kugeln
        addHoledBallToTempArray(getBallIdx(s));
        //Entferne die Kugel vom Tisch
        removeBallFromTable(getBallIdx(s));
        
        /*  
        if(playerBall_form[playerTurn] == 0){
            if(0 < getBallIdx(s) && getBallIdx(s) < 8){
                    playerBall_form[playerTurn] = 2;
                    playerBall_form[getOtherPlayerIdx(playerTurn)] = 1;
                }
                else{
                    playerBall_form[playerTurn] = 1;
                    playerBall_form[getOtherPlayerIdx(playerTurn)] = 2;
            }
        }
        */

    }

    private void eightBall_resetGameLogic(){
        //Wer fängt an?
        if(Random.Range(0,2) == 0){
            playerTurn = 0;
        }
        else{
            playerTurn = 1;
        }
        playerPoints = new int[2];
        playerBall_form = new int[2];
        playerBall_holed = new int[2,8];
        temp_holedBalls = new int[16];
        for(int i = 0; i < 2; i++){
            playerPoints[i] = 0;
            playerBall_form[i] = 0;
            for(int j = 0; j < 8; j++){
                playerBall_holed[i, j] = -1;            //Leere Werte im index sollen -1 sein
            }
            resetTemp_holedBalls();
        }
        firstBallHit = null;
        bestOf = 3;
        round = 1;
        playerIsAllowedToMove = true;
    }
    private void eightBall_resetBallPosition()
    {
        Transform poolTable_tf = poolTable.GetComponent<Transform>();
        int safetyIndex = 0;

        for (int i = 0; i < 16; i++)
        {
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
        for (int i = 0; i < 16; i++)
        {
            ballIsUntaken[i] = true;
        }

        //Fängt mit der ersten Kugel (ganz vorne) an
        while (true)
        {
            //Die weiße Kugel darf nicht mitgezählt werden
            rnd = Random.Range(1, 16);
            //Die schwarze auch nicht
            if (rnd != 8)
            {
                balls_8ball_row0 = balls_8ball[rnd];
                ballIsUntaken[rnd] = false;
                if (rnd < 8)
                {
                    isBallFull = true;
                }
                else
                {
                    isBallFull = false;
                }
                break;
            }
        }

        index = 0;
        while (true)
        {
            //Die weiße Kugel darf nicht mitgezählt werden
            rnd = Random.Range(1, 16);
            if ((rnd != 8) && (checkIfBallFull(rnd) == isBallFull) && ballIsUntaken[rnd])
            {
                balls_8ball_row1[index] = balls_8ball[rnd];
                ballIsUntaken[rnd] = false;
                //nächste Ballart (Voll oder Halb) invertieren
                isBallFull = invertBool(isBallFull);
                index++;
                if (index == 2) break;
            }
        }

        index = 0;
        while (true)
        {
            //Die weiße Kugel darf nicht mitgezählt werden
            rnd = Random.Range(1, 16);
            safetyIndex++;
            if (index == 1)
            {
                balls_8ball_row2[index] = balls_8ball[8];
                index++;
            }
            else
            {
                if ((rnd != 8) && (checkIfBallFull(rnd) != isBallFull) && ballIsUntaken[rnd])
                {
                    balls_8ball_row2[index] = balls_8ball[rnd];
                    ballIsUntaken[rnd] = false;
                    //nächste Ballart (Voll oder Halb) invertieren
                    isBallFull = invertBool(isBallFull);
                    index++;
                    if (index == 3) break;
                }
            }

        }
        index = 0;
        while (true)
        {
            //Die weiße Kugel darf nicht mitgezählt werden
            rnd = Random.Range(1, 16);
            if ((rnd != 8) && (checkIfBallFull(rnd) != isBallFull) && ballIsUntaken[rnd])
            {
                balls_8ball_row3[index] = balls_8ball[rnd];
                ballIsUntaken[rnd] = false;
                //nächste Ballart (Voll oder Halb) invertieren
                if ((index == 0) || (index == 2))
                {
                    isBallFull = invertBool(isBallFull);
                }
                index++;
                if (index == 4) break;
            }
        }

        bool firstCornerBall_Form = isBallFull;
        index = 0;
        while (true)
        {
            //Die weiße Kugel darf nicht mitgezählt werden
            rnd = Random.Range(1, 16);
            if ((rnd != 8) && (checkIfBallFull(rnd) != isBallFull) && ballIsUntaken[rnd])
            {
                balls_8ball_row4[index] = balls_8ball[rnd];
                ballIsUntaken[rnd] = false;
                //Der Bereich zwischen den 5 Kugeln geht "an/aus" (zwischen 0 und 4)
                if ((0 < index) && (index < 4))
                {
                    isBallFull = invertBool(isBallFull);
                }
                else
                {
                    //Der letzte Ball soll das Gegenteil von dem Ball der anderen Ecke sein
                    if (index != 0) isBallFull = !firstCornerBall_Form;
                }
                index++;
                if (index == 5) break;
            }

        }

        //Zufällige Rotationen
        for (int i = 0; i < 16; i++)
        {
            balls_8ball[i].localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        }
        //Postition der Weißen Kugel
        balls_8ball[0].position = poolTable_tf.position + new Vector3(0, ballOnTableY(), -20);
        //Position der Kugel ganz vorne
        balls_8ball_row0.position = poolTable_tf.position + new Vector3(0, ballOnTableY(), ballGroup_zOffset);
        for (int i = 0; i < 2; i++)
        {
            balls_8ball_row1[i].position = poolTable_tf.position + new Vector3(0.5f * balls_8ball_row3[i].localScale.x - balls_8ball_row3[i].localScale.x * (i), ballOnTableY(), ballGroup_zOffset + 0.5f * balls_8ball_row1[i].localScale.x / Mathf.Tan(30 * Mathf.PI / 180));
        }
        for (int i = 0; i < 3; i++)
        {
            balls_8ball_row2[i].position = poolTable_tf.position + new Vector3(balls_8ball_row3[i].localScale.x - balls_8ball_row3[i].localScale.x * (i), ballOnTableY(), ballGroup_zOffset + balls_8ball_row2[i].localScale.x / Mathf.Tan(30 * Mathf.PI / 180));
        }
        for (int i = 0; i < 4; i++)
        {
            balls_8ball_row3[i].position = poolTable_tf.position + new Vector3(1.5f * balls_8ball_row3[i].localScale.x - balls_8ball_row3[i].localScale.x * (i), ballOnTableY(), ballGroup_zOffset + 1.5f * balls_8ball_row3[i].localScale.x / Mathf.Tan(30 * Mathf.PI / 180));
        }
        for (int i = 0; i < 5; i++)
        {
            balls_8ball_row4[i].position = poolTable_tf.position + new Vector3(2f * balls_8ball_row4[i].localScale.x - balls_8ball_row4[i].localScale.x * (i), ballOnTableY(), ballGroup_zOffset + 2f * balls_8ball_row4[i].localScale.x / Mathf.Tan(30 * Mathf.PI / 180));
        }

        for(int i = 0; i < 16; i++){
            //Alle Freezes entfernen
            balls_8ball_rb[i].constraints = RigidbodyConstraints.None;
        }

        
    }

    void Start()
    {
        ballSize[0] = new Vector3(5.7f, 5.7f, 5.7f);

        Transform whiteBall_tf = whiteBall.GetComponent<Transform>();
        Transform ball1_tf = ball1.GetComponent<Transform>();
        Transform ball2_tf = ball2.GetComponent<Transform>();
        Transform ball3_tf = ball3.GetComponent<Transform>();
        Transform ball4_tf = ball4.GetComponent<Transform>();
        Transform ball5_tf = ball5.GetComponent<Transform>();
        Transform ball6_tf = ball6.GetComponent<Transform>();
        Transform ball7_tf = ball7.GetComponent<Transform>();
        Transform ball8_tf = ball8.GetComponent<Transform>();
        Transform ball1_half_tf = ball1_half.GetComponent<Transform>();
        Transform ball2_half_tf = ball2_half.GetComponent<Transform>();
        Transform ball3_half_tf = ball3_half.GetComponent<Transform>();
        Transform ball4_half_tf = ball4_half.GetComponent<Transform>();
        Transform ball5_half_tf = ball5_half.GetComponent<Transform>();
        Transform ball6_half_tf = ball6_half.GetComponent<Transform>();
        Transform ball7_half_tf = ball7_half.GetComponent<Transform>();
        Transform poolTable_tf = poolTable.GetComponent<Transform>();

        balls_8ball[0] = whiteBall_tf;
        balls_8ball[1] = ball1_tf;
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

        balls_8ball_rb[0] = whiteBall.GetComponent<Rigidbody>();
        balls_8ball_rb[1] = ball1.GetComponent<Rigidbody>();
        balls_8ball_rb[2] = ball2.GetComponent<Rigidbody>();
        balls_8ball_rb[3] = ball3.GetComponent<Rigidbody>();
        balls_8ball_rb[4] = ball4.GetComponent<Rigidbody>();
        balls_8ball_rb[5] = ball5.GetComponent<Rigidbody>();
        balls_8ball_rb[6] = ball6.GetComponent<Rigidbody>();
        balls_8ball_rb[7] = ball7.GetComponent<Rigidbody>();
        balls_8ball_rb[8] = ball8.GetComponent<Rigidbody>();
        balls_8ball_rb[9] = ball1_half.GetComponent<Rigidbody>();
        balls_8ball_rb[10] = ball2_half.GetComponent<Rigidbody>();
        balls_8ball_rb[11] = ball3_half.GetComponent<Rigidbody>();
        balls_8ball_rb[12] = ball4_half.GetComponent<Rigidbody>();
        balls_8ball_rb[13] = ball5_half.GetComponent<Rigidbody>();
        balls_8ball_rb[14] = ball6_half.GetComponent<Rigidbody>();
        balls_8ball_rb[15] = ball7_half.GetComponent<Rigidbody>();

        Pooltable_script poolTable_script = poolTable.GetComponent<Pooltable_script>(); //Assoziation auf den Script des Pooltables.
                                                                                        //Brauchen wir, um die Funktion "setSize" zu verwenden, die die Grösse des Tisches setzt.
        poolTable_script.setSize(0);


        for (int i = 0; i < 16; i++)
        {
            balls_8ball[i].localScale = ballSize[0];
            balls_8ball[i].position = poolTable_tf.position + new Vector3(0, ballOnTableY(), 40);
        }
        startGame();
    }

    void eightBall_Wingame(int player){

    }
    void Update()
    {
        //Nachdem eine Kugel geschossen wurde, 
        if(playerIsAllowedToMove == false){
        // warte bis alle Kugeln stehenbleiben
            if(areTheBallsStandingStill()){
                playerIsAllowedToMove = true;
                for(int i = 0; i < 16; i++){    //Die Kugeln werden 100% zum stehen gebracht
                    balls_8ball_rb[i].velocity = new Vector3(0f,0f,0f);
                    balls_8ball_rb[i].angularVelocity = new Vector3(0f,0f,0f);
                }

                //Wurde bereits festgelegt, wer Welche Kugeln anspielt?
                if(playerBall_form[0] != 0){//Ja

                    int oldPlayerTurn = playerTurn; //Das Speichern des jetztigen Spielers ist wichtig für die Bewertung

                    //Ist die erste getroffene Kugel eine mit der korrekten Form?
                    if(playerBall_form[playerTurn] != getBallForm(getBallIdx(firstBallHit.name))){
                        //Wenn nicht erfolgt ein Spielerwechsel und die Position der weißen Kugel darf von dem neuen Spieler bestimmt werden
                        switchPlayerTurn();
                    }

                    //Wurde die schwarze Kugel eingelocht?
                    if(getWhiteAndBlackIdxInArray(oldPlayerTurn)[1] != -1){
                        //Hat der Spieler der dran ist, alle seine Kugeln eingelocht?
                        if(getAmountOfHoledBalls(oldPlayerTurn) == 7){
                            addPoint(oldPlayerTurn);
                        }
                        //Wenn nicht, bekommt der andere Spieler einen Punkt
                        else{
                            addPoint(getOtherPlayerIdx(oldPlayerTurn));
                        }

                        if(playerPoints[0] == bestOf){
                            eightBall_Wingame(oldPlayerTurn);
                        }
                        if(playerPoints[1] == bestOf){
                            eightBall_Wingame(oldPlayerTurn);
                        }

                        //Hat ein Spieler die volle Punktzahl noch nicht erreicht, geht das Spiel hier weiter.
                        playerIsAllowedToMove = true;
                    }
                    else{
                        //Wurde eine Kugel mit der richtigen Form eingelocht?
                        int correctForm = playerBall_form[playerTurn];
                        for(int i = 0; i < 16; i++){

                        }
                    }
                }
                else{                       //Nein, kein Spieler hat eine Form zugewiesen bekommen.
                    bool containsColoredBall = false;
                    int idxOfFirstColoredBall = -1;
                    bool containsWhiteBall = false;
                    for(int i = 0; i < 16; i++){
                        //Die Situation, wenn die erste Kugel die schwarze ist, wird hier nicht bedacht.
                        //Was soll aber auch in der Situation passieren?
                        //Wenn diese Abfrage true ist, ist eine farbige Kugel eingelocht worden.
                        if(temp_holedBalls[i] > 0 && temp_holedBalls[i] != 8){
                            if(containsColoredBall == false) idxOfFirstColoredBall = i;
                            containsColoredBall = true;
                        }
                        if(temp_holedBalls[i] == 0){
                            containsWhiteBall = true;
                        }
                    }
                    //Wurde eine farbige Kugel eingelocht?
                    if(containsColoredBall){
                        //Der Spieler der dran ist, bekommt die Form der ersten Kugel, die eingelocht wurde. Der Andere bekommt die andere Form.
                        playerBall_form[playerTurn] = getBallForm(idxOfFirstColoredBall);
                        playerBall_form[getOtherPlayerIdx(playerTurn)] = getBallForm(idxOfFirstColoredBall);
                        
                        if(containsWhiteBall){
                            switchPlayerTurn();
                            //Spieler darf die Position bestimmen
                        }
                    }
                    else{
                        if(containsWhiteBall){
                            switchPlayerTurn();
                            //Spieler darf die Position bestimmen
                        }
                        else{
                            switchPlayerTurn();
                        }
                    }

                }
                
            }
            
        }
    }
}
