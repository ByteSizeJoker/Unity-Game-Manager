using UnityEngine;

//* Formatted and Commented
//! Method Should'nt be edited for Flappy Bird Game
public class MovePipe : MonoBehaviour
{
    #region Variables
    [Header("Pipe Settings")]
    [Space(10)]

    [Tooltip("Speed of the pipe\nRate at which pipe move along the screen")]
    [SerializeField]
    float moveSpeed;

    [Tooltip("Co-ordinate at which pipe GameObject will be deleted")]
    [SerializeField]
    float death_coords = -30;
    #endregion

    #region Methods

    //* Called after Start Method, and Runs once every frame
    void Update()
    {
        // Checks if the game is started
        if (LogicFB.isGameStarted)
        {
            // Moves the pipe by Changing it position
            transform.position += (Vector3.left * moveSpeed * Time.deltaTime);

            // Deletes the pipe if it reaches the death_coords
            if (transform.position.x < death_coords) Destroy(this.gameObject);
        }
    }
    #endregion
}
