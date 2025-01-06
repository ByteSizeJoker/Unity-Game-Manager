using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//* Formatted and Commented
public class BallSpawner : MonoBehaviour
{
    #region Variables
    [Header("Ball Spawning Properties")]
    [Header("Game Objects")]
    [Space(10)]

    [Tooltip("Ball Spawner\nThe prefab that represents the ball spawner in the game and is instantiated when the game starts.")]
    [SerializeField]
    GameObject ballSpawner;

    [Tooltip("Ball Prefab\nThe prefab that represents the ball in the game and is instantiated n time\nwhere n is ballCount.")]
    [SerializeField]
    GameObject ballPrefab;

    [Tooltip("")]
    [SerializeField]
    GameObject BallHolder;


    [Header("Properties")]
    [Space(10)]

    [Tooltip("Number of Balls that player has collected while playing\nDefault: 1")]
    [SerializeField]
    int balls;
    public static int ballCount;   //Static Variable of Balls for providing reference in all the scripts
    public static int newBalls = 0;

    [Tooltip("Delay between instantiation of balls\nDefault: 1\n[Note: Measurement is not in seconds or any other general unit]")]
    [SerializeField]
    float intervalBetweenBallLaunches;

    [Tooltip("Force applied to the ball when it is launched\nDefault: 5")]
    [SerializeField]
    float force;

    [Tooltip("Minimum and Maximum angle for which aiming line will be rendered and the ball can be launched\nDefault: -87, 87")]
    [SerializeField]
    Vector2 minMaxAngle;

    [Tooltip("LayerMask for raycast")]
    [SerializeField]
    LayerMask layerMask;

    RaycastHit2D ray;
    float angle;
    public static bool isBallLaunched;      //  Static bool for checking if LaunchBalls method is executed or not
    public static bool areAllBallsLaunched; //  Static bool for checking if all the balls are launched or not
    #endregion

    #region Methods

    //* Runs before the game starts/before the first frame
    void Awake()
    {
        ballCount = balls;
    }

    //* Called after Start Method, and Runs repeatedly at a fixed time interval during the game, mainly used for physics related tasks
    void FixedUpdate()
    {
        DrawDots();
    }

    //* Called after Start Method, and Runs once every frame
    void Update()
    {
        //Checks if Mouse Button is released, it's not over UI and ball is not launched (LaunchBalls method is not executed)
        if (Input.GetMouseButtonUp(0) && !isBallLaunched && !EventSystem.current.IsPointerOverGameObject())
        {
            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y) //Check if user is aiming in the correct angle
                StartCoroutine(LaunchBalls());
        }
    }

    //* Render the aiming line using Dots Class
    void DrawDots()
    {
        //Check if Mouse Button is pressed/Held, it's not over UI and ball is not launched (LaunchBalls method is not executed)
        if (Input.GetMouseButton(0) && !isBallLaunched && !EventSystem.current.IsPointerOverGameObject())
        {
            ray = Physics2D.Raycast(transform.position, transform.up, 15f, layerMask);
            Vector2 reflectPos = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y)
            {
                Dots.Instance.DrawDottedLine(transform.position, ray.point);
                Dots.Instance.DrawDottedLine(ray.point, ray.point + reflectPos.normalized * 2f);
            }

            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
    }

    //* Launches n ball after t delay where n is ballCount and t is intervalBetweenBallLaunches
    IEnumerator LaunchBalls()
    {
        //Prepare Variables before launching balls
        ballCount = balls + newBalls;
        Debug.Log(ballCount);
        int ballNumber = 0;
        isBallLaunched = true;
        areAllBallsLaunched = false;
        BallPlatformCollisionDetector.Executed = false;
        float interval = (float)((intervalBetweenBallLaunches - ballCount * 0.01f) * 0.1f < 0.04f ? 0.04f : (intervalBetweenBallLaunches - ballCount * 0.01f) * 0.1f);

        //Check if ballCount is 0 if it is then set it to 1 so game doesn't freeze
        if (ballCount == 0) ballCount = 1;

        for (int i = 1; i <= ballCount; i++)
        {

            yield return new WaitForSeconds(interval); //Delay between instantiation of balls

            //Instantiate Ball and Launch it
            GameObject ball = Instantiate(ballPrefab, transform.position + new Vector3(0, 0.01f, 0), Quaternion.identity);
            //ball.transform.parent = BallHolder.transform;
            ballNumber++;
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * force * 100);

            //Add tags to first and last ball for collision detection and logic management
            if (ballNumber == 1) ball.tag = "FirstBall";

            if (i == ballCount) ballSpawner.SetActive(false); //Hide BallSpawner after all the balls are launched
        }
        areAllBallsLaunched = true;
    }
    #endregion
}

