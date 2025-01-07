using TMPro;
using UnityEngine;

//# Formatted and Commented

/// <summary>
/// Manages the behavior of individual bricks in the game, including toughness reduction upon collision and brick destruction when toughness reaches zero.
/// </summary>
public class BrickScript : MonoBehaviour
{
    public int Row; // The row index of this brick in the brick array
    public int Column; // The column index of this brick in the brick array

    [Tooltip("Brick Prefab\nThe GameObject with *this* Script assigned to it.\nRepresents a single brick in the game.")]
    [SerializeField]
    GameObject BrickPrefab; // Reference to the prefab for this brick
    BrickGenerator brickGenerator; // Reference to the BrickGenerator script

    /// <summary>
    /// Initializes the brick by finding and referencing the BrickGenerator in the scene.
    /// </summary>
    void Awake()
    {
        brickGenerator = GameObject.Find("BrickGenerator").GetComponent<BrickGenerator>();
    }

    /// <summary>
    /// Handles the behavior when this brick collides with another object.
    /// Specifically manages reducing toughness when hit by the ball and destroying the brick if toughness reaches zero.
    /// </summary>
    /// <param name="collision">The collision data from the Unity physics system.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the TMProUGUI component that displays the brick's toughness
        TextMeshProUGUI ToughnessText = BrickPrefab.GetComponentInChildren<TextMeshProUGUI>();
        int Toughness = int.Parse(ToughnessText.text);

        /* 
            Check if the colliding object is a ball (layer 7) and the brick's toughness is not zero.
            If true, decrease toughness by 1 to simulate the brick breaking.
        */
        if (collision.gameObject.layer == 7 && Toughness != 0)
        {
            ToughnessText.text = (Toughness - 1).ToString(); // Update toughness text
            brickGenerator.brickArray[Row, Column] = Toughness - 1; // Update toughness in the brick array
        }

        // If toughness is zero, remove the brick from the array and destroy the game object
        if (int.Parse(ToughnessText.text) == 0)
        {
            brickGenerator.brickArray[Row, Column] = 0; // Set the brick's array value to 0
            Destroy(this.gameObject); // Destroy the brick
        }
    }

    /// <summary>
    /// Sets the row and column indices of this brick in the brick array.
    /// </summary>
    /// <param name="i">The row index.</param>
    /// <param name="j">The column index.</param>
    public void SetIndex(int i, int j)
    {
        Row = i;
        Column = j;
    }
}
