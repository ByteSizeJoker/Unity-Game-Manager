using System.Collections.Generic;
using UnityEngine;

//# Formatted and Commented
//! Method Should'nt be edited. It's Read-Only

/// <summary>
/// Class responsible for managing and rendering dots along an aiming line.
/// </summary>
public class Dots : MonoBehaviour
{
    #region Variables

    [Header("Dot Settings")]
    [Space(10)]

    [Tooltip("Dot Sprite\nThe sprite used for the aiming line's individual points (dots).")]
    public Sprite Dot;

    [Tooltip("Dot Size\nThe size of the dots that make up the aiming line.\nDefault: 0.07")]
    [Range(0.01f, 1f)]
    public float Size;

    [Tooltip("Delta\nThe spacing between dots along the aiming line.\nDefault: 0.3")]
    [Range(0.1f, 2f)]
    public float Delta;

    [Tooltip("Transparency\nThe transparency (alpha value) of the dots.\nDefault: 1")]
    [Range(0, 1f)]
    public float Transparency;

    // Static reference to the current instance of the Dots script
    private static Dots _instance;

    [Tooltip("Static reference to the Dots instance. Used to access Dots functionality globally.")]
    public static Dots Instance => _instance;

    // List to store the positions of the dots to be rendered
    [Tooltip("Holds the positions of the dots along the aiming line.")]
    List<Vector2> positions = new List<Vector2>();

    // List to store references to the GameObjects representing the dots
    [Tooltip("Holds references to the GameObjects used as dots.")]
    List<GameObject> dots = new List<GameObject>();

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the Dots instance. Ensures only one instance exists in the scene.
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    /// <summary>
    /// Clears and destroys all dots on the aiming line at fixed time intervals.
    /// </summary>
    private void FixedUpdate()
    {
        if (positions.Count > 0)
        {
            DestroyAllDots();
            positions.Clear();
        }
    }

    /// <summary>
    /// Destroys all dot GameObjects and clears the list of rendered dots.
    /// </summary>
    private void DestroyAllDots()
    {
        foreach (var dot in dots)
        {
            Destroy(dot);
        }
        dots.Clear();
    }

    /// <summary>
    /// Creates a single dot GameObject with the specified sprite, size, and transparency.
    /// </summary>
    /// <returns>A GameObject representing a single dot.</returns>
    private GameObject GetOneDot()
    {
        var gameObject = new GameObject(); // Create a new GameObject
        gameObject.transform.localScale = Vector3.one * Size; // Set the size of the dot
        gameObject.transform.parent = transform; // Set the parent to keep hierarchy organized

        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, Transparency); // Set dot color with transparency
        sr.sprite = Dot; // Assign the sprite to the SpriteRenderer

        return gameObject;
    }

    /// <summary>
    /// Draws a dotted line between two points by calculating positions and rendering dots at regular intervals.
    /// </summary>
    /// <param name="start">The starting point of the line.</param>
    /// <param name="end">The ending point of the line.</param>
    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots(); // Clear any previously drawn dots
        Vector2 point = start; // Start drawing from the initial point
        Vector2 direction = (end - start).normalized; // Calculate the direction of the line

        // Add dot positions along the line at intervals defined by Delta
        while ((end - start).magnitude > (point - start).magnitude)
        {
            positions.Add(point);
            point += direction * Delta;
        }

        Render(); // Render the calculated positions as dots
    }

    /// <summary>
    /// Instantiates dot GameObjects at the positions stored in the positions list.
    /// </summary>
    private void Render()
    {
        foreach (var position in positions)
        {
            var dot = GetOneDot(); // Create a new dot
            dot.transform.position = position; // Position the dot at the calculated point
            dots.Add(dot); // Add the dot to the list of rendered dots
        }
    }
#endregion
}

