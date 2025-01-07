using UnityEngine;

//# Formatted and Commented
//! Method Should'nt be edited. It's Read-Only

/// <summary>
/// Class responsible for setting up screen borders to prevent objects from leaving the screen.
/// </summary>
public class LogicBB : MonoBehaviour
{
    #region Methods

    /// <summary>
    /// Initializes the game by creating screen border colliders to prevent objects from leaving the screen.
    /// </summary>
    private void Start()
    {
        CreateScreenBorders();
    }

    /// <summary>
    /// Creates invisible colliders along the screen borders to confine physics-based GameObjects within the screen bounds.
    /// </summary>
    private void CreateScreenBorders()
    {
        // Get the main camera to calculate screen dimensions
        Camera cam = Camera.main;

        // Calculate screen height and width in world units
        float screenHeight = cam.orthographicSize * 2; // Orthographic height of the screen
        float screenWidth = screenHeight * cam.aspect; // Width based on camera aspect ratio

        // Create the top border collider
        CreateCollider(
            position: new Vector2(0, screenHeight / 2 - 2.5f), // Position near the top of the screen
            size: new Vector2(screenWidth, 0.5f), // Horizontal border
            isVertical: false
        );

        // Create the left border collider
        CreateCollider(
            position: new Vector2(-screenWidth / 2, 0), // Position on the left of the screen
            size: new Vector2(1, screenHeight), // Vertical border
            isVertical: true
        );

        // Create the right border collider
        CreateCollider(
            position: new Vector2(screenWidth / 2, 0), // Position on the right of the screen
            size: new Vector2(1, screenHeight), // Vertical border
            isVertical: true
        );
    }

    /// <summary>
    /// Creates an individual border collider with a given position, size, and orientation.
    /// </summary>
    /// <param name="position">Position of the collider in world space.</param>
    /// <param name="size">Size of the collider (width and height).</param>
    /// <param name="isVertical">Whether the collider is vertical (true) or horizontal (false).</param>
    private void CreateCollider(Vector2 position, Vector2 size, bool isVertical)
    {
        // Create a new GameObject to represent the border
        GameObject border = new GameObject("Border");

        // Set the border as a child of the object this script is attached to
        border.transform.parent = transform;

        // Assign the border to a specific layer (e.g., "Ignore Raycast" layer 3 here)
        border.layer = 3;

        // Set the border's position
        border.transform.position = position;

        // Add a BoxCollider2D component to the border
        BoxCollider2D collider = border.AddComponent<BoxCollider2D>();

        // Set the size of the collider
        collider.size = size;

        // Adjust the scale of the border GameObject for proper visual alignment
        border.transform.localScale = isVertical
            ? new Vector3(0.1f, 1, 1) // Narrow width for vertical borders
            : new Vector3(1, 0.1f, 1); // Narrow height for horizontal borders
    }
    #endregion
}

