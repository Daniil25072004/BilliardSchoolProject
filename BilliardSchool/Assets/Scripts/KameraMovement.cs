using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KameraMovement : MonoBehaviour
{
    [SerializeField] public float distanceFromTarget = 5.0f;
    [SerializeField] private GameObject whiteBall;
    private Transform whiteBall_tf;   //Braucht den weissen Ball zur Positionierung
    private Rigidbody whiteBall_rb; //Den Ball muss man einen Impuls geben können
    [SerializeField] private GameObject poolTable;   //Den Tisch für Vogelperspektive
    private Transform pooltable_tf;
    private Vector3 nachVorne;
    private Vector3 nachVorne_richtung;
    [SerializeField] private float smoothenessTime = 0.2f;
    [SerializeField] private float forwardOnCue = 20f;
    [SerializeField] private GameObject gameplayManager;
    private GameplayManager gameplayManager_script;
    private float rotationX = 0f;
    private float rotationY = 0f;
    public float sensivity = 3f;
    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 currentRotation;
    private int mode = 1;       //Diese Variable sagt, was genau die Kamera macht. 0 ist z.B. Vogelperspektive, 1 wäre die Kamera dann auf der Kugel fixiert.
    //Variablen für das Kugelschießen
    private bool holdingPushButton = false;
    private float queueForce = 0f;
    private bool riseQueueForce = true;
    private bool isAllowedToChangePerspective = true;
    private bool isPlayerDecidingBallPosition = false;
    public Slider powerBar;
    public LayerMask layersToHit;
    public void forceCameraMode(int m){
        mode = m;
        isAllowedToChangePerspective = false;
    }
    public void allowChangeOfPerspective(){
        isAllowedToChangePerspective = true;
    }
    void CameraAsBirdsEye(){

        transform.localEulerAngles = new Vector3(90, 0, 90);
        transform.position = pooltable_tf.position + new Vector3(0,300,0);

    }
    void CameraOnBall(){

        //Input.mouseScrollDelta.y ------> Wenn der Wert 1: Mausrad nach vorne. Bei -1 nach hinten und 0 ist default
        switch(Input.mouseScrollDelta.y){
            case 1:
                if(distanceFromTarget > 10){
                    distanceFromTarget = distanceFromTarget - 2;
                }
                break;
            case -1:
                if(distanceFromTarget < 40){
                    distanceFromTarget = distanceFromTarget + 2;
                }
                break;
            default:
                break;
        }

        //--------------------------------------------------------------------
        // 1. Den Input der Maus Einlesen und die Kamera smooth machen. Der Kamerarichtungsvektor ist also mit dem Vektor "currentRotation" erreichbar

        rotationX += Input.GetAxis("Mouse X") * sensivity;
        
        //Diese If sorgt dafür, dass man nicht auf der Y achse überrotiert.
        if(rotationY <= 75){
            if(rotationY < -15){
                rotationY = -15;
            }
            else{
                rotationY -= Input.GetAxis("Mouse Y") * sensivity;      //Hier -= da bei += sonst invertiert ist aus irgend einem Grund?
            }
        }
        else{
            rotationY = 75;
        }

        //  Die Kamera smooth machen
        Vector3 nextRotation = new Vector3 (rotationY, rotationX, 0);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothenessTime);

        //      Siehe "kameraPositioning.png"
        //      1.
        Transform transform_copy = transform;
        transform_copy.localEulerAngles = currentRotation;
        nachVorne = whiteBall_tf.position + transform_copy.forward * forwardOnCue;
        //      2.
        transform.localEulerAngles = currentRotation + new Vector3(15, 0, 0);
        transform.position = nachVorne - (transform.forward) * distanceFromTarget;

    }

    private float getCurrentQueueForce(){
        return queueForce;
    }
    void Start(){

        whiteBall_tf = whiteBall.GetComponent<Transform>();
        whiteBall_rb = whiteBall.GetComponent<Rigidbody>();
        pooltable_tf = poolTable.GetComponent<Transform>();
        gameplayManager_script = gameplayManager.GetComponent<GameplayManager>();
        powerBar.maxValue = gameplayManager_script.getMaxQueueForce();

    }
    void manageQueueForce(){
        float timeForMax = gameplayManager_script.getMaxQueueForce() / gameplayManager_script.getTimeForMaxQueueForce();    // MaxForce/TimeForMaxForce = Faktor f+r deltaTime. 
        if(riseQueueForce){
            if(queueForce >= gameplayManager_script.getMaxQueueForce()){
                riseQueueForce = false;
            }
        }
        else{
            if(queueForce <= 0){
                riseQueueForce = true;
            }
        }
        if(riseQueueForce){
            queueForce += Time.deltaTime * timeForMax;
            powerBar.value = queueForce;


        }
        else{
            queueForce -= Time.deltaTime * timeForMax;
            powerBar.value = queueForce;
        }
    }

    public void playerDecidesBallPosition(){
        mode = 0;
        CameraAsBirdsEye();
        gameplayManager_script.constrainAllBalls(true);
        isAllowedToChangePerspective = false;
        isPlayerDecidingBallPosition = true;
    }
    void Update()
    {
        if((holdingPushButton || Input.GetKey(PlayerControl.getHitKey())) && gameplayManager_script.getPlayerIsAllowedToMove()){
            holdingPushButton = true;
            manageQueueForce();
            if(Input.GetKey(PlayerControl.getHitKey()) == false){ 
                //Dem gameplayManager wird befohlen, die Kugel zu schießen
                gameplayManager_script.shootWhiteBall(new Vector3(transform.forward.x, 0, transform.forward.z)*queueForce);
                //whiteBall_rb.velocity = new Vector3(transform.forward.x, 0, transform.forward.z)*queueForce;
                holdingPushButton = false;
                queueForce = 0f;
            }
        }

        if(isPlayerDecidingBallPosition){
            //Die Position der Maus auf dem Bildschirm auf den jeweiligen Pixel
            Vector3 screenPosition = Input.mousePosition;
            Vector3 worldPosition;
            //Ein Ray ist eine Linie die von der Kamera aus durch den Punkt zeigt, auf den die Maus zeigt.
            //  (Dieser Punkt ist in der 3D-Welt von Unity, nicht der 2D-Pixel auf dem Bildschirm)
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            //Diese Abfrage ist wahr, wenn der Ray mit etwas (Was genau sagt "layersToHit") kollidiert.
            //d.h. Wenn die Maus auf dem Tisch ist, ist diese Abfrage wahr
            if(Physics.Raycast(ray, out RaycastHit hitData, 500, layersToHit)){
                worldPosition = hitData.point;
                whiteBall_tf.position = worldPosition + new Vector3(0f, gameplayManager_script.getBallSize(0).y/2, 0f);
            }

            //Der Spieler bestätigt die Position mit der "place" Taste.
            if(Input.GetKeyDown(PlayerControl.getPlaceKey())){
                isPlayerDecidingBallPosition = false;
                isAllowedToChangePerspective = true;
                gameplayManager_script.playerPlacedBall();
                mode = 1;
            }

        }
        
        if(Input.GetKeyDown(PlayerControl.getCameraKey()) && holdingPushButton == false && isAllowedToChangePerspective){
            if(mode == 0){
                mode = 1;
            }
            else{
                mode = 0;
            }
        }
        switch (mode){
            case 0:
                CameraAsBirdsEye();
                break;
            case 1:
                CameraOnBall();
                break;
            default:
                break;
        }
    }
}
