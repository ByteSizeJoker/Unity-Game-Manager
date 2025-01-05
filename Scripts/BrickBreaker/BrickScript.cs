using TMPro;
using UnityEngine;

//* Formatted and Commented
public class BrickScript : MonoBehaviour
{
    public int Row;
    public int Column;

    [Tooltip("Brick Prefab\nThe GameObject with *this* Script assign to it\nThe GameObject that is instantiated repeatedly to create bricks during the game.")]
    [SerializeField]
    GameObject BrickPrefab;
    BrickGenerator brickGenerator;
    Array.Write write = new Array.Write();

    void Awake()
    {
        brickGenerator = GameObject.Find("BrickGenerator").GetComponent<BrickGenerator>();
    }

    //* Runs when GameObject with Collider and *this* Script/Method is collided with and other colliders
    void OnCollisionEnter2D(Collision2D collision)
    {

        // Get TMProUGUI component from BrickPrefab
        TextMeshProUGUI ToughnessText = BrickPrefab.GetComponentInChildren<TextMeshProUGUI>();

        /*  When GameObject on layer 7 (Ball) collides and Toughness is not 0 decrease it by 1
            Simulate Brick breaking behavior                                                     */
        if (collision.gameObject.layer == 7 && int.Parse(ToughnessText.text) != 0)
        {
            ToughnessText.text = (int.Parse(ToughnessText.text) - 1).ToString();
            write.Custom(brickGenerator.UniversalBrickHolder, Row, Column, int.Parse(ToughnessText.text));
        }

        // Destroy BrickPrefab when Toughness is 0
        if (int.Parse(ToughnessText.text) == 0)
        {
            write.Custom(brickGenerator.UniversalBrickHolder, Row, Column, 0);
            Destroy(BrickPrefab);
        }
    }

    public void SetIndex(int i, int j)
    {
        Row = i;
        Column = j;
        //Debug.Log(Row + ", " + Column);
    }

    public Vector2Int GetIndex()
    {
        return new Vector2Int(Row, Column);
    }
}
