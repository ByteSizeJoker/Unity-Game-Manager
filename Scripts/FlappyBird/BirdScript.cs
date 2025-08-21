using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

//# Formatted and Commented

/// <summary>
/// Handles the behavior and movement of the Bird GameObject in the Flappy Bird game.
/// It responds to user input (mouse click) to make the bird flap, manages collision detection, 
/// and triggers game over logic when the bird collides with obstacles or falls out of bounds.
/// </summary>
public class BirdScript : MonoBehaviour
{
    #region Variables
    [Header("Game Objects and Components")]
    [Space(10)]

    /// <summary>
    /// The Bird GameObject controlled by the player.
    /// </summary>
    [Tooltip("Bird GameObject\nThe main player object in the Flappy Bird game.")]
    [SerializeField]
    GameObject bird;

    [Header("Variables")]
    [Space(10)]

    /// <summary>
    /// Rigidbody2D component attached to the Bird GameObject.
    /// </summary>
    [Tooltip("Bird's Rigidbody\nThe Rigidbody2D component from the Bird GameObject.")]
    [SerializeField]
    Rigidbody2D rb;

    /// <summary>
    /// The upward velocity applied to the Bird when it flaps.
    /// </summary>
    [Tooltip("Velocity applied to the Bird when it flaps.")]
    [SerializeField]
    float flapSpeed;

    /// <summary>
    /// A flag indicating whether the Bird is alive.
    /// </summary>
    bool isBirdAlive = true;

    /// <summary>
    /// Reference to the `LogicFB` script for managing game logic.
    /// </summary>
    LogicFB logic;

    public bool immortalBird = false;
    #endregion

    #region Methods

    /// <summary>
    /// Initializes game settings and references before the first frame update.
    /// </summary>
    void Awake()
    {
        // Get the LogicFB script from the GameObject tagged as 'LogicFB'.
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicFB>();
    }

    /// <summary>
    /// Handles input and applies flap velocity to the Bird when the left mouse button is clicked.
    /// </summary>
    void Update()
    {
        // Check if the left mouse button is clicked, the Bird is alive, and the game has started.
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) && isBirdAlive && LogicFB.isGameStarted)
        {
            // Ensure the mouse click is not over UI elements.
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Apply upward velocity to the Bird.
                rb.velocity = Vector2.up * flapSpeed;
            }

        }

        if (immortalBird)
        {
            rb.angularVelocity = 0f;
            bird.transform.rotation = Quaternion.identity;
            if (immortalBird && bird.transform.position.y < -100)
            {
                bird.transform.position = new Vector2(0, 0);
                rb.velocity = Vector2.zero;
            }
        }
    }

    /// <summary>
    /// Detects collisions and triggers game over logic when the Bird collides with an object.
    /// </summary>
    /// <param name="collision">The collision data from the Bird's Rigidbody2D.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with the barrier layer (layer 3).
        if (collision.gameObject.layer != 3 && !immortalBird)
        {
            // Trigger game-over logic.
            logic.GameOver();
            isBirdAlive = false;

            // Pause the game if the Bird falls below the screen.
            if (bird.transform.position.y <= -10 && !immortalBird)
            {
                Time.timeScale = 0f;
            }

        }
    }
    #endregion

}
