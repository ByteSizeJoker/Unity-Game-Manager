using UnityEngine;

//# Formatted and Commented

/// <summary>
/// Class responsible for handling brick trigger events and managing the brick array.
/// </summary>
public class BB_Trigger : MonoBehaviour
{
    int Row; // Row index of the object in the brick array
    int Column; // Column index of the object in the brick array

    /// <summary>
    /// Sets the row and column indices of this object in the brick array.
    /// </summary>
    /// <param name="i">The row index.</param>
    /// <param name="j">The column index.</param>
    public void SetIndex(int i, int j)
    {
        Row = i;
        Column = j;
    }

    /// <summary>
    /// Trigger event that occurs when another collider enters this object's trigger zone.
    /// Used to handle collisions with the ball.
    /// </summary>
    /// <param name="collider">The collider that entered the trigger zone.</param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collider belongs to the ball (layer 7 is assumed to represent the ball layer)
        if (collider.gameObject.layer == 7)
        {
            // Retrieve the BrickGenerator component from the "BrickGenerator" GameObject
            BrickGenerator brickGenerator = GameObject.Find("BrickGenerator").GetComponent<BrickGenerator>();

            // Debug: Print the brick array before making changes
            Array.PrintArray(brickGenerator.brickArray);

            // Set the corresponding brick array position to 0 (indicating removal)
            brickGenerator.brickArray[Row, Column] = 0;

            // Debug: Print the brick array after making changes
            Array.PrintArray(brickGenerator.brickArray);

            // Increment the count of new balls to spawn
            BallSpawner.newBalls++;

            // Destroy this game object (remove the brick from the scene)
            Destroy(this.gameObject);
        }
    }
}
