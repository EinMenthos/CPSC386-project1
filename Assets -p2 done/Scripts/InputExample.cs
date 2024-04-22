using TMPro;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Unity has an existing input system and a new one
/// They can not be used simultaneously. To use the new
/// input system, you must open the package manager window
/// then add "Input System" from the Unity Registry
/// </summary>
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputExample : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D jumpingBody, movingBody;
    [SerializeField]
    bool useForce = true;
    [SerializeField] float jumpPower = 6f, moveSpeed = 3.0f;
    float physicsModifier = 100f;
    Vector2 moveDir = Vector2.zero;
    [SerializeField] public float waypointRadius = 7.18f;
    [SerializeField] AudioSource backgroundMusic;    
    [SerializeField] TMP_Text tPause;
    public TMP_Text tClear;


    void Start(){
        //have to relink with actual music due to transition of game1 to game1b
        if (backgroundMusic == null){
            Debug.Log("Button: Creating link to original AudioSource Object");
            backgroundMusic = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioSource>();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(movingBody) 
            if(useForce)    //need to set the flag        
                movingBody.AddForce(moveDir*moveSpeed*Time.deltaTime*physicsModifier, ForceMode2D.Force);
            else{   //we are mainly using this route
                //get ball position to avoid it going into the walls
                Vector2 ballPos = movingBody.position+(moveDir*moveSpeed*Time.deltaTime);
                if (ballPos.x > -waypointRadius && ballPos.x < waypointRadius) {
                    speed = jumpingBody.velocity.magnitude;
                    if (speed == 0)
                    {
                        //ball will move together with the bar
                        jumpingBody.MovePosition(jumpingBody.position+(moveDir*moveSpeed*Time.deltaTime));     
                    }
                        //bar will move no matter the speed of the ball
                        movingBody.MovePosition(movingBody.position+(moveDir*moveSpeed*Time.deltaTime));
                }
            }
    }


    //This function will provide movement using the new input system
    void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>() * moveSpeed;
    }
    
    float speed;

    void OnJump()
    {
        Debug.Log("Spacebar pressed");
        speed = jumpingBody.velocity.magnitude;
        if (speed == 0 )
        {
            SetRandomTrajectory();  //this one will allow the ball to start randomly
        }
        else{   //ball is moving
            Scene currentScene = SceneManager.GetActiveScene ();
            string sceneName = currentScene.name;
            
            if (GameObject.Find("Text-clear")){
                Debug.Log("Scene completed - cannot pause/unpause");
            }
            else{
                Debug.Log("Scene is running - can pause/unpause");
                if(sceneName.Contains("game")){
                    if (Time.timeScale == 1) { // pausing the game
                        Time.timeScale = 0;
                        if(PlayerPrefs.GetInt("VolumeMute") == 1)backgroundMusic.mute = true;
                        tPause.gameObject.SetActive(true);
                    }
                    else{
                        Time.timeScale = 1;
                        if(PlayerPrefs.GetInt("VolumeMute") == 1)backgroundMusic.mute = false;
                        tPause.gameObject.SetActive(false);
                    } 
                }
            }  
        }
    }
    
    //created with professor's help
    private void SetRandomTrajectory(){
        Vector2 force = Vector2.up;
        force.x = Random.Range(-1f, 1f);
        jumpingBody.AddForce(force.normalized * jumpPower,ForceMode2D.Impulse);
        jumpingBody.velocity = jumpingBody.velocity.normalized * jumpPower;
    }

}