using System.Collections.Generic;
using UnityEngine;

//* Formatted and Commented
//! Method Should'nt be edited for Brick Breaker Game
public class Dots : MonoBehaviour
{
    #region Variables
    [Header("Dot Settings")]
    [Space(10)]

    [Tooltip("Dot Sprite\n\nThe sprite that will be used for the dots(aiming line's individual point)")]
    public Sprite Dot;

    [Tooltip("Dot Size\nThe size of the dots\nDefault: 0.07")]
    [Range(0.01f, 1f)]
    public float Size;

    [Tooltip("Delta\nThe gap between the dots on the aiming line.\nDefault: 0.3")]
    [Range(0.1f, 2f)]
    public float Delta;

    [Tooltip("Transparency\nThe transparency of the dots\nDefault: 1")]
    [Range(0, 1f)] public float Transparency;

    // Static reference to the Dots instance
    private static Dots _instance;

    // Static property for accessing the Dots instance
    public static Dots Instance => _instance;

    // List to store the positions of each dot in the game
    List<Vector2> positions = new List<Vector2>();

    // List to store the GameObjects representing the dots
    List<GameObject> dots = new List<GameObject>();
    #endregion

    #region Methods

    //* Runs before the game starts/ before the first frame
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    //* Called after Start Method, and Runs repeatedly at a fixed time interval during the game, mainly used for physics related tasks
    private void FixedUpdate()
    {
        //Destroys all dots and clears their positions if there are any
        if (positions.Count > 0)
        {
            DestroyAllDots();
            positions.Clear();
        }
    }

    //* Destroys all dot GameObjects and clears the list of dots
    private void DestroyAllDots()
    {
        foreach (var dot in dots)
        {
            Destroy(dot);
        }
        dots.Clear();
    }

    //* Creates a new dot GameObject with the specified size, transparency, and sprite.
    private GameObject GetOneDot()
    {
        // Assigning GameObjects and Components
        var gameObject = new GameObject();
        gameObject.transform.localScale = Vector3.one * Size;
        gameObject.transform.parent = transform;

        var sr = gameObject.AddComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, Transparency);
        sr.sprite = Dot;

        return gameObject;
    }

    //* Renders a dotted line from start to end by creating dots at regular intervals
    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots();
        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            positions.Add(point);
            point += direction * Delta;
        }

        Render();
    }

    //* Renders all dots in the positions list by creating a dot GameObject at each position
    private void Render()
    {
        foreach (var position in positions)
        {
            var dot = GetOneDot();
            dot.transform.position = position;
            dots.Add(dot);
        }
    }
}
#endregion
