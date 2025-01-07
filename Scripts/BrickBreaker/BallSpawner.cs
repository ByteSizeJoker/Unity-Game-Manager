using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

//# Formatted and Commented

/// <summary>
/// Manages the spawning and launching of balls during gameplay. Handles aiming, launching, and ball count updates.
/// </summary>
public class BallSpawner : MonoBehaviour
{
    #region Variables
    [Header("Ball Spawning Properties")]
    [Header("Game Objects")]
    [Space(10)]

    [Tooltip("Prefab for the Ball Spawner. This is instantiated when the game starts.")]
    [SerializeField]
    GameObject ballSpawner;

    [Tooltip("Prefab for the Ball. This prefab is instantiated multiple times based on the ball count.")]
    [SerializeField]
    GameObject ballPrefab;

    [Header("Properties")]
    [Space(10)]

    [Tooltip("Number of balls collected by the player. Default: 1.")]
    [SerializeField]
    int balls;

    public static int ballCount;   // Tracks total balls available across all scripts
    public static int newBalls = 0; // Tracks newly added balls during gameplay

    [Tooltip("Delay between the launch of each ball. Default: 1.\nNote: The unit is not in seconds.")]
    [SerializeField]
    float intervalBetweenBallLaunches;

    [Tooltip("Force applied to a ball during launch. Default: 5.")]
    [SerializeField]
    float force;

    [Tooltip("Minimum and maximum angles for aiming and launching the ball. Default: -87, 87.")]
    [SerializeField]
    Vector2 minMaxAngle;

    [Tooltip("LayerMask for detecting collisions using raycast.")]
    [SerializeField]
    LayerMask layerMask;

    RaycastHit2D ray;  // Stores the raycast hit information
    float angle;       // Stores the current aiming angle

    public static bool isBallLaunched;       // Indicates if the LaunchBalls method has executed
    public static bool areAllBallsLaunched; // Indicates if all balls have been launched

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the total ball count at the start of the game.
    /// </summary>
    void Awake()
    {
        ballCount = balls; // Set the static ball count to the number of balls collected.
    }

    /// <summary>
    /// Handles physics-related tasks at fixed intervals, such as updating the aiming line.
    /// </summary>
    void FixedUpdate()
    {
        DrawDots(); // Continuously draw the aiming trajectory.
    }

    /// <summary>
    /// Monitors user input and triggers ball launching if conditions are met.
    /// </summary>
    void Update()
    {
        /*  Check if the left mouse button is released, no ball is currently launched, 
            and the pointer is not over a UI element.                                  */
        if (Input.GetMouseButtonUp(0) && !isBallLaunched && !EventSystem.current.IsPointerOverGameObject())
        {
            // Launch the balls if the aiming angle is within the valid range.
            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y)
                StartCoroutine(LaunchBalls());
        }
    }


    /// <summary>
    /// Draws the aiming line to visualize the trajectory using the Dots class.
    /// </summary>
    void DrawDots()
    {
        // Checks if the left mouse button is pressed/held, the pointer is not over a UI element, and no ball is currently launched.
        if (Input.GetMouseButton(0) && !isBallLaunched && !EventSystem.current.IsPointerOverGameObject())
        {
            ray = Physics2D.Raycast(transform.position, transform.up, 15f, layerMask);
            Vector2 reflectPos = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            // Draws the aiming line if the angle is within the allowed range.
            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y)
            {
                Dots.Instance.DrawDottedLine(transform.position, ray.point);
                Dots.Instance.DrawDottedLine(ray.point, ray.point + reflectPos.normalized * 2f);
            }

            // Updates the object's rotation to match the aim direction.
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
    }

    /// <summary>
    /// Launches multiple balls with a delay between each launch. Adjusts properties based on ball count and intervals.
    /// </summary>
    IEnumerator LaunchBalls()
    {
        // Sets up variables before launching balls.
        ballCount = balls + newBalls;
        Debug.Log(ballCount);
        int ballNumber = 0;
        isBallLaunched = true;
        areAllBallsLaunched = false;
        BallPlatformCollisionDetector.hasSpawnerPositionBeenSet = false;

        // Calculate interval for ball launches, making sure it doesn't go below a minimum threshold.
        float interval = Mathf.Max((intervalBetweenBallLaunches - ballCount * 0.01f) * 0.1f, 0.04f);

        // Ensures at least one ball is launched.
        if (ballCount == 0) ballCount = 1;

        // Launch each ball with a delay.
        for (int i = 1; i <= ballCount; i++)
        {
            yield return new WaitForSeconds(interval); // Delay between launches.

            // Instantiate the ball and apply force to launch it.
            GameObject ball = Instantiate(ballPrefab, transform.position + new Vector3(0, 0.01f, 0), Quaternion.identity);
            ballNumber++;
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * force * 100);

            // Tag the first for special logic handling.
            if (ballNumber == 1) ball.tag = "FirstBall";

            // Deactivate the ball spawner once all balls are launched.
            if (i == ballCount) ballSpawner.SetActive(false);
        }
        areAllBallsLaunched = true;
    }
    #endregion

}

