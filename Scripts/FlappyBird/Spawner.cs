using UnityEngine;

//# Formatted and Commented
//! Method Should'nt be edited. It's Read-Only

/// <summary>
/// Handles spawning of pipes at regular intervals, adjusting the spawn rate based on the player's score to increase difficulty over time.
/// The pipes are spawned at random vertical positions within a defined range.
/// </summary>
public class Spawner : MonoBehaviour
{
    #region Variables

    [Header("Pipe Properties")]
    [Space(10)]

    /// <summary>
    /// The Pipe Prefab that is instantiated repeatedly to create Pipes (Obstacles) during the game.
    /// </summary>
    [Tooltip("Pipe Prefab\nThe GameObject that is instantiated repeatedly to create Pipes(Obstacles) during the game.")]
    [SerializeField]
    GameObject pipePrefab;

    /// <summary>
    /// The spawn rate of pipes. Controls how frequently the pipes are spawned.
    /// Lower values = Faster spawn rate, higher values = Slower spawn rate.
    /// </summary>
    [Tooltip("Spawn Rate of pipes\nThe rate at which the pipes are spawned.\nDefault: 4.5\nLower = Faster\nHigher = Slower")]
    [SerializeField]
    float spawnRate;

    /// <summary>
    /// Private timer to track time between spawns.
    /// </summary>
    float timer = 0;

    /// <summary>
    /// The vertical distance between pipes. Controls the height variation of pipes.
    /// </summary>
    [Tooltip("The amount of distance between the pipes.\nDefault: 7")]
    [SerializeField]
    float heightOffset;

    /// <summary>
    /// Private reference to the LogicFB script, which manages the game's logic.
    /// </summary>
    private LogicFB logic;

    #endregion

    #region Methods

    /// <summary>
    /// Called once when the game starts. It initializes necessary variables and spawns the first pipe.
    /// </summary>
    void Start()
    {
        // Get reference to the LogicFB script using its tag
        logic = GameObject.FindGameObjectWithTag("LogicFB").GetComponent<LogicFB>();

        // Spawn the first pipe
        SpawnPipe();
    }

    /// <summary>
    /// Called once per frame. Updates the timer and spawns new pipes when necessary.
    /// Also adjusts the difficulty of the game over time.
    /// </summary>
    void Update()
    {
        // Increment the timer with the time passed since the last frame
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // When the timer exceeds the spawn rate, spawn a new pipe and adjust difficulty
            SpawnPipe();
            Difficulty();
            timer = 0; // Reset the timer
        }
    }

    /// <summary>
    /// Spawns a new pipe at a random vertical position within the specified range.
    /// </summary>
    void SpawnPipe()
    {

        // Check if the game has started before spawning pipes
        if (LogicFB.isGameStarted)
        {

            // Define the lowest and highest Y positions for the pipe
            float lowest_point = transform.position.y - heightOffset;
            float highest_point = transform.position.y + heightOffset;

            // Instantiate the pipe prefab at a random Y position within the range
            Instantiate(pipePrefab, new Vector3(transform.position.x, Random.Range(lowest_point, highest_point), 0), transform.rotation);
        }
    }

    /// <summary>
    /// Increases the difficulty of the game by reducing the spawn rate of pipes as the score increases.
    /// </summary>
    public void Difficulty()
    {
        if (logic.score >= 5 && logic.score < 10)
        {
            if (spawnRate >= 4.5)
            {
                spawnRate -= 1F; // Decrease spawn rate to increase pipe frequency
            }
        }
        else if (logic.score >= 10 && logic.score < 20)
        {
            if (spawnRate >= 3.5)
            {
                spawnRate -= 0.5F; // Further decrease spawn rate
            }
        }
        else if (logic.score >= 20)
        {
            if (spawnRate >= 3)
            {
                spawnRate -= 0.5F; // Reduce spawn rate even more at higher scores
            }
        }
    }

    #endregion

}