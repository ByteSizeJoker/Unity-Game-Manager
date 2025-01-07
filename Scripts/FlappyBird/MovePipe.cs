using UnityEngine;

//# Formatted and Commented
//! Method Should'nt be edited. It's Read-Only

/// <summary>
/// Controls the movement of pipes in the Flappy Bird game. The pipes move from right to left at a set speed, 
/// and are destroyed once they move off-screen. The class also checks if the game has started before moving the pipes.
/// </summary>
public class MovePipe : MonoBehaviour
{
    #region Variables
    [Header("Pipe Settings")]
    [Space(10)]

    /// <summary>
    /// The speed at which the pipe moves along the screen.
    /// </summary>
    [Tooltip("Speed of the pipe\nRate at which pipe move along the screen")]
    [SerializeField]
    float moveSpeed;

    /// <summary>
    /// The x-coordinate at which the pipe GameObject will be destroyed.
    /// </summary>
    [Tooltip("Co-ordinate at which pipe GameObject will be deleted")]
    [SerializeField]
    float death_coords = -30;
    #endregion

    #region Methods

    /// <summary>
    /// Called every frame to update the position of the pipe and check if it should be destroyed.
    /// </summary>
    void Update()
    {
        // Checks if the game has started
        if (LogicFB.isGameStarted)
        {
            // Moves the pipe leftwards based on moveSpeed and the frame time
            transform.position += (Vector3.left * moveSpeed * Time.deltaTime);

            // Destroys the pipe if it moves past the death coordinates
            if (transform.position.x < death_coords)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

}
