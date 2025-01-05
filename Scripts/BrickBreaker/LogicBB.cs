using UnityEngine;

//* Formatted and Commented
//! Method Should'nt be edited for Brick Breaker Game
public class LogicBB : MonoBehaviour
{
    #region Methods

    //* Runs before the game starts/before the first frame
    private void Start()
    {
        CreateScreenBorders();
    }

    //* Creates collider on Screen borders to prevent physics GameObject from leaving the screen
    private void CreateScreenBorders()
    {
        // Get Camera
        Camera cam = Camera.main;

        // Get Screen Size
        float screenHeight = cam.orthographicSize * 2;
        float screenWidth = screenHeight * cam.aspect;

        // Create Colliders
        CreateCollider(new Vector2(0, screenHeight / 2 - 2.5f), new Vector2(screenWidth, 0.5f), false);
        CreateCollider(new Vector2(-screenWidth / 2, 0), new Vector2(1, screenHeight), true);
        CreateCollider(new Vector2(screenWidth / 2, 0), new Vector2(1, screenHeight), true);
    }

    //* Creates Colliders with custom transform
    private void CreateCollider(Vector2 position, Vector2 size, bool isVertical)
    {
        // Assigning GameObjects and Components
        GameObject border = new GameObject("Border");
        border.transform.parent = transform;
        border.layer = 3;
        border.transform.position = position;

        // Get BoxCollider2D (Collider used in 2D games)
        var collider = border.AddComponent<BoxCollider2D>();
        collider.size = size;

        // Get localScale of Collider
        border.transform.localScale = isVertical ? new Vector3(0.1f, 1, 1) : new Vector3(1, 0.1f, 1);
    }
}
#endregion
