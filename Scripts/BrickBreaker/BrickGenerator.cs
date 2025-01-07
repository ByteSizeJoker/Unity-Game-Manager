using TMPro;
using UnityEngine;
using UnityEngine.UI;

//# Formatted and Commented

/// <summary>
/// Responsible for generating and managing bricks in the game. Handles the creation of new rows of bricks, brick toughness, and ball spawning.
/// </summary>
public class BrickGenerator : MonoBehaviour
{
    #region Variables
    [Header("Brick Properties")]
    [Space(10)]

    [Tooltip("Prefab for the Brick. Instantiated to create bricks during gameplay.")]
    [SerializeField]
    GameObject brickPrefab;

    [Tooltip("Prefab for the Ball. Increases the ball count when the projectile collides with it.")]
    [SerializeField]
    GameObject ballPrefab;

    [Tooltip("Scale of each brick. Default: (0.0055, 0.0055).")]
    [SerializeField]
    Vector2 scaleOfBrick;

    [Tooltip("Horizontal offset between adjacent bricks. Default: -1.375.")]
    [SerializeField]
    float offsetBetBricks;

    [Tooltip("X: Horizontal offset between centers of two adjacent bricks.\nY: Vertical position of the brick row.\nDefault: (0.555, 1.65).")]
    [SerializeField]
    Vector2 offsetBY;

    [Tooltip("Array of brick sprites. Used to randomize the appearance of bricks.")]
    [SerializeField]
    Sprite[] spriteHolder;

    [Tooltip("Sprite for the glow effect applied to balls.")]
    [SerializeField]
    Sprite glowSprite;

    [Tooltip("Text component for the score display. Used to calculate brick toughness.")]
    [SerializeField]
    TextMeshProUGUI scoreText;

    [HideInInspector]
    public int[,] brickArray = new int[8, 6]; // 2D array to store brick properties (e.g., toughness or type)

    // Private instances of helper classes for managing brick data
    Array.Shift shift = new Array.Shift();  // Handles shifting rows of bricks
    Array.Write write = new Array.Write();  // Handles writing brick properties to the array
    #endregion


    #region Methods

    /// <summary>
    /// Initializes the game by creating the first row of bricks.
    /// </summary>
    void Awake()
    {
        CreateNewRowOfBrick();
    }

    /// <summary>
    /// Handles periodic tasks during gameplay, such as creating a new row of bricks when required.
    /// </summary>
    void FixedUpdate()
    {
        if (BallPlatformCollisionDetector.createRow)
        {
            CreateNewRowOfBrick();
            BallPlatformCollisionDetector.createRow = false;
        }
    }

    /// <summary>
    /// Creates a new row of bricks by shifting existing rows, randomizing the new row, and spawning bricks.
    /// </summary>
    public void CreateNewRowOfBrick()
    {
        destroyAllOldBrick();
        int score = int.Parse(scoreText.text[7..].Trim());

        // Shift rows and randomize the first row
        shift.LinearShift(brickArray, 1);
        write.random(brickArray, (int)(score / 1.05f), score + 2, 0, 1);
        BrickSpawner(score);
    }

    /// <summary>
    /// Spawns bricks based on the brick array values. 
    /// Each value corresponds to the toughness of a brick or special items (e.g., balls).
    /// </summary>
    /// <param name="score">The current score to determine spawning chances.</param>
    private void BrickSpawner(int score)
    {
        float Offset = offsetBetBricks;

        for (int i = 0; i < brickArray.GetLength(0); i++)
        {
            for (int j = 0; j < brickArray.GetLength(1); j++)
            {
                if (brickArray[i, j] == 0)
                {
                    Offset = j == 5 ? offsetBetBricks : Offset + offsetBY.x;
                    continue;
                }

                if (i == 0 && Random.Range(0, (int)SpawningChance(score, false)) == 0 || brickArray[i, j] == -1)
                {
                    spawnBall(i, j, Offset);
                }
                else if (i == 0 && Random.Range(0f, 1f) < 0.7f || i != 0)
                {
                    spawnBrick(i, j, Offset);
                }
                else
                {
                    brickArray[i, j] = 0;
                }

                Offset = j == 5 ? offsetBetBricks : Offset + offsetBY.x;
            }
        }
    }

    /// <summary>
    /// Spawns a brick with the given position, size, and toughness.
    /// </summary>
    void spawnBrick(int i, int j, float Offset)
    {
        GameObject Brick = Instantiate(brickPrefab);
        RectTransform BrickRect = Brick.GetComponent<RectTransform>();
        TextMeshProUGUI Toughness = Brick.GetComponentInChildren<TextMeshProUGUI>();
        Image img = Brick.GetComponentInChildren<Image>();

        Brick.transform.SetParent(transform);
        BrickRect.sizeDelta = new Vector2(100f, 100f);
        BrickRect.localScale = scaleOfBrick;
        BrickRect.anchoredPosition = new Vector2(Offset, offsetBY.y - 0.6f * i);
        Toughness.text = brickArray[i, j].ToString();

        int SpriteIndex = Random.Range(0, spriteHolder.Length);
        img.sprite = spriteHolder[SpriteIndex];

        Brick.GetComponent<BrickScript>().SetIndex(i, j);
    }

    /// <summary>
    /// Spawns a ball at the given position with appropriate size and components.
    /// </summary>
    void spawnBall(int i, int j, float Offset)
    {
        GameObject NewBall = Instantiate(ballPrefab);
        Destroy(NewBall.GetComponent<Animator>());
        NewBall.AddComponent<RectTransform>();
        RectTransform BallRect = NewBall.GetComponent<RectTransform>();

        NewBall.transform.SetParent(transform);
        BallRect.sizeDelta = new Vector2(0.2f, 0.2f);
        BallRect.anchorMin = new Vector2(0, 0);
        BallRect.anchorMax = new Vector2(0, 0);
        BallRect.anchoredPosition = new Vector2(Offset, offsetBY.y - 0.6f * i);
        NewBall.GetComponent<Collider2D>().isTrigger = true;
        NewBall.layer = 0;

        NewBall.GetComponent<BB_Trigger>().SetIndex(i, j);

        GameObject Glow = new GameObject();
        Glow.transform.SetParent(NewBall.transform);
        Glow.transform.localPosition = Vector3.zero;
        Glow.transform.localScale = new Vector2(0.7f, 0.7f);

        Glow.AddComponent<SpriteRenderer>();
        Glow.GetComponent<SpriteRenderer>().sprite = glowSprite;
        Glow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        brickArray[i, j] = -1;
    }

    /// <summary>
    /// Destroys all existing brick objects in the scene.
    /// </summary>
    void destroyAllOldBrick()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Calculates the chance of spawning a specific object (brick or ball) based on the score.
    /// </summary>
    /// <param name="score">The player's current score.</param>
    /// <param name="forBricks">Whether the chance is being calculated for bricks (true) or balls (false).</param>
    /// <returns>A float representing the spawn chance.</returns>
    float SpawningChance(int score, bool forBricks)
    {
        if (forBricks)
        {
            switch (true)
            {
                case bool when score > 0 && score <= 10: return 0.50f;
                case bool when score > 10 && score <= 30: return 0.60f;
                case bool when score > 30 && score <= 40: return 0.70f;
                case bool when score > 40: return 0.75f;
                default: return -1;
            }
        }
        else
        {
            switch (true)
            {
                case bool when score > 0 && score <= 10: return 4;
                case bool when score > 10 && score <= 20: return 6;
                case bool when score > 20 && score <= 40: return 8;
                case bool when score > 40: return 15;
                default: return -1;
            }
        }
    }
    #endregion

}
