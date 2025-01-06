using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//* Formatted and Commented
public class BrickGenerator : MonoBehaviour
{
    #region Variables
    [Header("Brick Properties")]
    [Space(10)]

    [Tooltip(" Brick Prefab\nThe GameObject that is instantiated repeatedly to create bricks during the game.")]
    [SerializeField]
    GameObject BrickPrefab;

    [Tooltip(" Ball Prefab\nIncrease no of balls when Ball projectile collides with it")]
    [SerializeField]
    GameObject BallPrefab;

    [Tooltip(" Scale of Brick\n Default = 0.0055,0.0055")]
    [SerializeField]
    Vector2 ScaleOfBrick;

    [Tooltip(" Offset between each brick\n Default = -1.375")]
    [SerializeField]
    float OffsetBetBricks;

    [Tooltip(" X: Offset between centre of two adjacent bricks\n Y: Vertical Position of brick row\n Default = 0.555,1.65")]
    [SerializeField]
    Vector2 offsetBY;

    [Tooltip(" Brick Sprite Holder\nRandomizes Sprites of Bricks")]
    [SerializeField]
    Sprite[] SpriteHolder;

    [Tooltip("Glow effect for ball")]
    [SerializeField]
    Sprite GlowSprite;

    [Tooltip(" Score Text\nUsed to calculate brick Toughness")]
    [SerializeField]
    TextMeshProUGUI ScoreText;

    [HideInInspector]
    public int[,] BrickArray = new int[8, 6];

    // Private reference to Sub classes of Array class
    Array.Shift shift = new Array.Shift();
    Array.Write write = new Array.Write();
    #endregion

    #region Methods

    //* Runs before the game starts/ before the first frame
    void Awake()
    {
        CreateNewRowOfBrick();
    }

    //* Called after Start Method, and Runs repeatedly at a fixed time interval during the game, mainly used for physics related tasks
    void FixedUpdate()
    {
        if (BallPlatformCollisionDetector.CreateRow)
        {
            CreateNewRowOfBrick();
            BallPlatformCollisionDetector.CreateRow = false;
        }
    }

    //* Creates a new row of bricks by Shifting each row in array by 1, randomizing the first row and executing BrickSpawner Method
    public void CreateNewRowOfBrick()
    {
        destroyAllOldBrick();
        int score = int.Parse(ScoreText.text[7..].Trim());

        // Shift each row in array by 1 and randomize first row
        shift.LinearShift(BrickArray, 1);
        write.random(BrickArray, (int)(score / 1.05f), score + 2, 0, 1); // replace 10 with score
        Array.PrintArray(BrickArray);
        BrickSpawner();
    }

    //* Reads each value in array and Instantiates the BrickPrefab at the corresponding position and Toughness
    private void BrickSpawner()
    {
        float Offset = OffsetBetBricks; // Offset between each brick

        /*  For each row (i), iterate through each column (j). 
            Instantiate a brick if the value at that position is not 0. */
        for (int i = 0; i < BrickArray.GetLength(0); i++)
        {
            for (int j = 0; j < BrickArray.GetLength(1); j++)
            {
                //Skip empty spaces(0) in Array
                if (BrickArray[i, j] == 0)
                {
                    //Check if it is the last column if it is then reset offset and continue else update offset
                    if (j == 5)
                    {
                        Offset = OffsetBetBricks;
                    }
                    else
                    {
                        Offset += offsetBY.x;
                    }
                    BrickArray[i, j] = 0;
                    continue;
                }
                // else if (BrickArray[i, j] == -1)
                // {
                //     continue;
                // }

                if (i == 0 ? Random.Range(0, 10) == 0 : false || BrickArray[i,j] == -1) // null% chance to spawn a New ball for User
                { 
                    GameObject NewBall = Instantiate(BallPrefab);
                    Destroy(NewBall.GetComponent<Animator>());
                    NewBall.AddComponent<RectTransform>();
                    RectTransform BallRect = NewBall.GetComponent<RectTransform>();

                    NewBall.transform.SetParent(transform);
                    BallRect.sizeDelta = new Vector2(0.2f, 0.2f);
                    BallRect.anchorMin = new Vector2(0, 0); BallRect.anchorMax = new Vector2(0, 0);
                    BallRect.anchoredPosition = new Vector2(Offset, offsetBY.y - 0.6f * i);
                    NewBall.GetComponent<Collider2D>().isTrigger = true;
                    NewBall.layer = 0;
                    

                    NewBall.GetComponent<BB_Trigger>().SetIndex(i, j);

                    GameObject Glow = new GameObject();

                    Glow.transform.SetParent(NewBall.transform);
                    Glow.transform.localPosition = Vector3.zero;
                    Glow.transform.localScale = new Vector2(0.7f, 0.7f);

                    Glow.AddComponent<SpriteRenderer>();
                    Glow.GetComponent<SpriteRenderer>().sprite = GlowSprite;
                    Glow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

                    BrickArray[i, j] = -1;

                }
                else
                {
                    // Instantiate Brick and Get its Transform, Toughness and Image component
                    GameObject Brick = Instantiate(BrickPrefab);
                    RectTransform BrickRect = Brick.GetComponent<RectTransform>();
                    TextMeshProUGUI Toughness = Brick.GetComponentInChildren<TextMeshProUGUI>();
                    Image img = Brick.GetComponentInChildren<Image>();

                    // Set the Brick as a child of the GameObject to which this script is attached.
                    Brick.transform.SetParent(transform);

                    // Set Brick's Transform
                    BrickRect.sizeDelta = new Vector2(100f, 100f);
                    BrickRect.localScale = ScaleOfBrick;
                    BrickRect.anchoredPosition = new Vector2(Offset, offsetBY.y - 0.6f * i);



                    // Set brick toughness text
                    Toughness.text = BrickArray[i, j].ToString();

                    // Assign Random Sprite
                    int SpriteIndex = Random.Range(0, SpriteHolder.Length);
                    img.sprite = SpriteHolder[SpriteIndex];

                    // Assign Row and Column to Brick
                    Brick.GetComponent<BrickScript>().SetIndex(i, j);
                }

                // Update Offset for next brick
                Offset += offsetBY.x;

                //Reset offset if code reached last column/End of row
                if (j == 5)
                {
                    Offset = OffsetBetBricks;
                }
            }
        }
    }

    public void destroyAllOldBrick() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        
    }
    #endregion
}
