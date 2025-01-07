using UnityEngine;

//# Formatted and Commented
//! Method Should'nt be edited. It's Read-Only

/// <summary>
/// Detects when the Bird passes through a pipe's trigger area and increments the player's score.
/// The score is updated by 1 each time the Bird successfully passes through a pipe.
/// </summary>
public class FB_Trigger : MonoBehaviour
{
    // Private reference to the LogicFB script, which handles game logic and score.
    public LogicFB logic;

    /// <summary>
    /// Called after Awake and before the first frame update.
    /// Initializes the private reference to the LogicFB script attached to a GameObject with the "LogicFB" tag.
    /// </summary>
    void Start()
    {
        // Find the GameObject with the "LogicFB" tag and get its LogicFB component
        logic = GameObject.FindGameObjectWithTag("LogicFB").GetComponent<LogicFB>();
    }

    /// <summary>
    /// Triggered when this GameObject's Collider2D enters another collider's trigger area.
    /// Increments the score if the collided object is on the specified layer (layer 6).
    /// </summary>
    /// <param name="collision">The Collider2D of the object this GameObject collided with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is on layer 6 (specific layer for score-related collisions)
        if (collision.gameObject.layer == 6)
        {
            // Add 1 to the score through the LogicFB script
            logic.addScore(1);
        }
    }

}

