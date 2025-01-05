using UnityEngine;

//* Formatted and Commented
//! Method Should'nt be edited for Flappy Bird Game
public class Trigger : MonoBehaviour
{
    // Private reference to Logic script
    public LogicFB logic;

    //* Called after awake and before the first frame update
    void Start()
    {
        // Create private reference to Logic script
        logic = GameObject.FindGameObjectWithTag("LogicFB").GetComponent<LogicFB>();
    }

    //* Runs when GameObject with Collider and *this* Script/Method is collided with and other colliders
    //* But Collision doesn't result into physic interaction
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 6) {
            logic.addScore(1);
        }
    }
}

