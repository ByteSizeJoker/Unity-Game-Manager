using UnityEngine;

//* Formatted and Commented
//! Method Should'nt be edited for Flappy Bird Game
public class Spawner : MonoBehaviour
{
    #region Variables
    [Header("Pipe Properties")]
    [Space(10)]

    [Tooltip("Pipe Prefab\nThe GameObject that is instantiated repeatedly to create Pipes(Obstacles) during the game.")]
    [SerializeField]
    GameObject pipePrefab;

    [Tooltip("Spawn Rate of pipes\nThe rate at which the pipes are spawned.\nDefault: 4.5\nLower = Faster\nHigher = Slower")]
    [SerializeField]
    float spawnRate;

    // Private float to keep track of time
    float timer = 0;

    [Tooltip("The amount of distance between the pipes.\nDefault: 7")]
    [SerializeField]
    float heightOffset;

    // Private reference to the Logic script
    LogicFB logic;
    #endregion

    #region Methods

    //* Called after awake and before the first frame update
    void Start()
    {
        // Create private reference to Logic script
        logic = GameObject.FindGameObjectWithTag("LogicFB").GetComponent<LogicFB>();

        // Spawn first pipe
        SpawnPipe();
    }

    //* Called after Start Method, and Runs once every frame
    void Update()
    {
        /*  Check if the timer is less than the spawn rate,
            if it is then increment the timer else spawn the pipe   */
        if (timer < spawnRate) {
            timer = timer + Time.deltaTime;
        }
        else {
            SpawnPipe();
            difficulty();
            timer = 0;
        }
    }

    // Spawns pipe
    void SpawnPipe() {

        // Check if the game is started
        if (LogicFB.isGameStarted) {

        // Set Lowest and highest point of the pipe
        float lowest_point = transform.position.y - heightOffset;
        float highest_point = transform.position.y + heightOffset;

        // Instantiate Pipe with randomizing Y coordinates
        Instantiate(pipePrefab, new Vector3(transform.position.x, Random.Range(lowest_point, highest_point), 0), transform.rotation);
        }
    }

    // Increase difficultly as the game progresses
    public void difficulty() {
        if (logic.score >= 5 && logic.score < 10) {
            if (spawnRate >= 4.5) {
            spawnRate = spawnRate - 1F;
            }
        }
        else if (logic.score >= 10 && logic.score < 20) {
            if (spawnRate >= 3.5) {
            spawnRate = spawnRate - 0.5F;
            }
        }
        else if (logic.score >= 20) {
            if (spawnRate >= 3) {
            spawnRate = spawnRate - 0.5F;
            }
        }
    }
    #endregion
}