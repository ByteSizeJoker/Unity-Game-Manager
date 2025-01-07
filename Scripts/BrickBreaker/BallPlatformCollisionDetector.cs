using UnityEngine;
using TMPro;

//# Formatted and Commented

/// <summary>
/// Detects collisions between the ball and the platform, manages the ball spawner's activation,
/// updates the score based on the player's progress, and handles the creation of new rows of bricks.
/// </summary>
public class BallPlatformCollisionDetector : MonoBehaviour {
    #region Variables
    [Header("GameObjects")]
    [Space(10)]

    [Tooltip("Ball Spawner GameObject. Attach the GameObject with BallSpawner.cs Script.")]
    [SerializeField]
    GameObject ballSpawner;

    [Tooltip("Platform holding the Ball Spawner. Attach the GameObject containing this Script.")]
    [SerializeField]
    GameObject platform;

    Vector2 spawnerNewPosition;

    [Header("Score Management")]
    [Space(10)]

    [Tooltip("Player's current score. Starts at 0.")]
    [SerializeField]
    int score = 0;

    [Tooltip("Text displaying the current score. Attach a TextMeshProUGUI component.")]
    [SerializeField]
    TextMeshProUGUI scoreText;

    [Tooltip("Text displaying the highest score. Attach a TextMeshProUGUI component.")]
    [SerializeField]
    TextMeshProUGUI highscoreText;

    [Tooltip("Enable to reset the highscore. Disable before building the project.")]
    [SerializeField]
    bool resetHighScore = false;

    // Prevents repositioning of ballSpawner until all balls are launched
    public static bool hasSpawnerPositionBeenSet = false;

    // Instructs BrickGenerator to create a new row of bricks once
    public static bool createRow = false;

    // Stores the total number of collisions with the balls
    int totalCollisions = 0;
    #endregion


    #region Methods

    /// <summary>
    /// Initializes high score text from PlayerPrefs and updates the score display.
    /// </summary>
    void Awake()
    {
        highscoreText.text = PlayerPrefsManager.HighScorePlayerPrefs(score, "BB_HighScore", resetHighScore).ToString();
        UpdateScore();
    }

    /// <summary>
    /// Handles collision events with the platform, updating ball positions, managing spawner activation,
    /// tracking collisions, and updating scores.
    /// </summary>
    /// <param name="collision">Collision details for the colliding object.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) // Check if the collided object is a ball
        {
            GameObject ball = collision.gameObject;
            Animator ballAnimator = ball.GetComponent<Animator>();
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

            // Stop ball movement and position it on the platform
            rb.bodyType = RigidbodyType2D.Static;
            ball.transform.position = new Vector2(ball.transform.position.x, platform.transform.position.y + 0.185f);

            totalCollisions++; // Increment collision count

            // Update spawner position based on the first ball's position
            if (ball.CompareTag("FirstBall"))
            {
                spawnerNewPosition = new Vector2(ball.transform.position.x, platform.transform.position.y + 0.185f);
                ball.transform.position = spawnerNewPosition;
            }

            // Activate the spawner if all balls are launched and position is not yet set
            if (BallSpawner.areAllBallsLaunched && !hasSpawnerPositionBeenSet)
            {
                ballSpawner.transform.position = spawnerNewPosition;
                ballSpawner.SetActive(true);
                hasSpawnerPositionBeenSet = true;
            }

            // Handle end-of-round updates and reset collision tracking
            if (totalCollisions == BallSpawner.ballCount || BallSpawner.ballCount == 1)
            {
                Destroy(GameObject.FindGameObjectsWithTag("FirstBall")[0]);
                BallSpawner.isBallLaunched = false;
                createRow = true;
                UpdateScore();
                totalCollisions = 0;
            }

            // Play shrinking animation and destroy non-first balls
            if (!ball.CompareTag("FirstBall"))
            {
                ballAnimator.SetTrigger("Play");
                Destroy(ball, ballAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }

    /// <summary>
    /// Increments the score, updates the score text, and checks for a new high score.
    /// Saves the high score in PlayerPrefs if a new high score is achieved.
    /// </summary>
    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "HighScore: " + PlayerPrefsManager.HighScorePlayerPrefs(score, "BB_HighScore", false);
    }
    #endregion
}