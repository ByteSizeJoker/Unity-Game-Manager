using UnityEngine;
using UnityEngine.EventSystems;


//* Formatted and Commented
public class BirdScript : MonoBehaviour
{
    #region Variables
    [Header("Game Objects and Components")]
    [Space(10)]

    [Tooltip("Bird GameObject\nGameObject on layer 'Player' in Flappy Bird Game")]
    [SerializeField]
    GameObject Bird;


    [Header("Variables")]
    [Space(10)]

    [Tooltip("Bird's Rigidbody\nRigidbody2D component from Bird GameObject")]
    [SerializeField]
    Rigidbody2D Rigidbody;

    [Tooltip("Velocity applied to the Bird when it flaps.")]
    [SerializeField]
    float flapSpeed;
    
    // Private bool to check if the Bird is alive
    bool isBirdAlive = true;

    // Private Reference to logicFB Script
    LogicFB logic;
    
    // Start is called before the first frame update
    #endregion

    #region Methods
    //* Runs before the game starts/ before the first frame
    void Awake()
    {
        // Get GameObject with tag 'LogicFB' (Get logicFB Script)
        logic = GameObject.FindGameObjectWithTag("LogicFB").GetComponent<LogicFB>();
    }

    //* Called after Start Method, and Runs once every frame
    void Update()
    {
        // If Mouse Button is pressed and Bird is alive and velocity to bird
        if (Input.GetMouseButtonDown(0) == true && isBirdAlive == true && LogicFB.isGameStarted)
        {
            //Check if Mouse is not over UI
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Rigidbody.velocity = Vector2.up * flapSpeed;
            }
        }
    }

    //* Runs when GameObject with Collider and *this* Script/Method is collided with and other colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*  Check if collided GameObject is not on layer 3
            (Barrier above Screen to prevent Bird from Flapping over the Screen)    */
        if (collision.gameObject.layer != 3)
        {
            // Call Score Managing Methods
            //logic.HighScore();
            logic.GameOver();
            isBirdAlive = false;

            // Pause Time if Bird Dies and falls below Screen
            if (Bird.transform.position.y <= -10)
            {
                Time.timeScale = 0f;
            }
        }
    }
    #endregion
}
