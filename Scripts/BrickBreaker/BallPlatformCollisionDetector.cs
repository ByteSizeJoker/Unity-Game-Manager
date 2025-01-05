using UnityEngine;
using TMPro;

//* Formatted and Commented
public class BallPlatformCollisionDetector : MonoBehaviour
{
    #region Variables
    [Header("GameObjects")]
    [Space(10)]

    [Tooltip("Ball Spawner GameObject\nAttach GameObject that holds the BallSpawner.cs Script.")]
    [SerializeField]
    GameObject ballSpawner;

    [Tooltip("Platform on which Ball Spawner is kept.\nAttach GameObject that holds the *this* Script.")]
    [SerializeField]
    GameObject Platform;

    Vector2 SpawnerNewPosition;


    [Header("Score Management")]
    [Space(10)]  

    [Tooltip("Score\nDefault: 0")]
    [SerializeField]
    int score = 0;

    [Tooltip("Score Text\nAttach GameObject that holds the TextMeshProUGUI Component.")]
    [SerializeField]
    TextMeshProUGUI ScoreText;

    [Tooltip("HighScore Text\nAttach GameObject that holds the TextMeshProUGUI Component.")]
    [SerializeField]
    TextMeshProUGUI HighScoreTxt;

    [Tooltip("Reset HighScore\nSet to false before building project.\nDefault: false")]
    [SerializeField]
    bool resetHighScore = false;


    //just for preventing code from changing position of ballSpawner before all balls are launched
    public static bool Executed = false;
    
    //just for telling code in BrickGenerator.cs to create a new row of brick once
    public static bool CreateRow = false;
    
    public int totalCollisions = 0;
    #endregion


    #region Methods

    //* Runs before the game starts/ before the first frame
    void Awake()
    {
        HighScoreTxt.text = PlayerPrefsManager.HighScorePlayerPrefs(score, "BB_HighScore", resetHighScore).ToString();
        UpdateScore();
    }

    //* Runs when GameObject with Collider and *this* Script/Method is collided with and other colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided GameObject is on layer 7 (Ball)
        if (collision.gameObject.layer == 7)
        {
            //Assigning GameObjects and Components
            GameObject ball = collision.gameObject;
            Animator ballAnimator = ball.GetComponent<Animator>();
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

            //Preventing ball from bouncing after colliding with "Platform" GameObject
            rb.bodyType = RigidbodyType2D.Static;
            ball.transform.position = new Vector2(ball.transform.position.x, Platform.transform.position.y + 0.185f);

            totalCollisions++;

            //Managing BallSpawner

            /*  When first ball is collides take it's x position and Platform's y position
                and assign it to SpawnerNewPosition and ball                                */
            if (ball.CompareTag("FirstBall"))
            {
                SpawnerNewPosition = new Vector2(ball.transform.position.x, Platform.transform.position.y + 0.185f);
                ball.transform.position = SpawnerNewPosition;
            }

            /*  If all balls are launched and below code isn't executed then set ballSpawner's position
                to SpawnerNewPosition and make it active also set Executed to true                      */
            if (BallSpawner.areAllBallsLaunched)
            {
                if (!Executed)
                {
                    ballSpawner.transform.position = SpawnerNewPosition;
                    ballSpawner.SetActive(true);
                    Executed = true;
                }

            }

            /*  If totalCollisions is equal to BallCount or BallCount is 1 then destroy FirstBall and set isBallLaunched to false
                also set CreateRow to true                                                                          */
            if (totalCollisions == BallSpawner.ballCount || BallSpawner.ballCount == 1)
            {
                Destroy(GameObject.FindGameObjectsWithTag("FirstBall")[0]);
                BallSpawner.isBallLaunched = false;
                CreateRow = true;
                BallSpawner.ballCount++; //temp ball adding code
                UpdateScore();
                totalCollisions = 0;
            }

            /* If ball isn't FirstBall then play Shrinking animation and destroy it */
            if (!ball.CompareTag("FirstBall"))
            {
                ballAnimator.SetTrigger("Play");
                Destroy(ball, ballAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }

    //*  Updates Score and HighScore- Just adds 1 to score and checks if score is higher than HighScore
    //*  if it is then set HighScore value to score and update and save PlayerPrefs
    public void UpdateScore()
    {
        score++;
        ScoreText.text = "Score: " + score.ToString();
        HighScoreTxt.text = "HighScore: " + PlayerPrefsManager.HighScorePlayerPrefs(score, "BB_HighScore", false);
    }

    #endregion
}