using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    GameObject[] pipeHolder;

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
    [Tooltip("The amount of distance between the pipes.\nDefault: 0.5")]
    [SerializeField]
    float minStep;

    /// <summary>
    /// The vertical distance between pipes. Controls the height variation of pipes.
    /// </summary>
    [Tooltip("The amount decrease from spawnRate based on current score.\nDefault: 0.05")]
    [SerializeField]
    float diffDecrement = 0.05f;

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

            GameObject pipe;
            int selectPipe = Random.Range(0, pipeHolder.Length * 10);
            float heightOffset;

            do {
                heightOffset = Random.Range(-3.5f, 3.5f);
                minStep = (minStep > 3.4 || minStep < -3.4) ? 0.5f : minStep; // Infinite Loop prevention
            } while (Mathf.Abs(heightOffset - PlayerPrefs.GetFloat("LastHeightOffset", 0f)) < minStep);

            if (selectPipe < pipeHolder.Length * 10 * 55 / 100)
            {
                pipe = pipeHolder[0];
            } else if (selectPipe < pipeHolder.Length * 10 * 90 / 100) {
                pipe = pipeHolder[1];
            } else if (selectPipe < pipeHolder.Length * 10 * 97 / 100) {
                pipe = pipeHolder[2];
            } else {
                pipe = pipeHolder[3];
            }

            // Instantiate the pipe prefab at a random Y position within the range
            Instantiate(pipe, new Vector3(transform.position.x, heightOffset, 0), Quaternion.identity);
            PlayerPrefs.SetFloat("LastHeightOffset", heightOffset);
            Debug.Log(heightOffset);
        }
    }

    /// <summary>
    /// Increases the difficulty of the game by reducing the spawn rate of pipes as the score increases.
    /// </summary>
    public void Difficulty()
    {
        // Apply difficulty reduction based on score milestones
        if (logic.score > 0)
        {
            // Gradual spawn rate reduction based on score
            spawnRate = Mathf.Max(4.5f - (logic.score * diffDecrement), 2.5f);
        }
    }

    #endregion

}