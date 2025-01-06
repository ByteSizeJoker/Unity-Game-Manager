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

    void Awake()
    {
        brickGenerator = GameObject.Find("BrickGenerator").GetComponent<BrickGenerator>();
    }

    //* Runs when GameObject with Collider and *this* Script/Method is collided with and other colliders
    void OnCollisionEnter2D(Collision2D collision)
    {

        // Get TMProUGUI component from BrickPrefab
        TextMeshProUGUI ToughnessText = BrickPrefab.GetComponentInChildren<TextMeshProUGUI>();
        int Toughness = int.Parse(ToughnessText.text);

        /*  When GameObject on layer 7 (Ball) collides and Toughness is not 0 decrease it by 1
            Simulate Brick breaking behavior                                                     */
        if (collision.gameObject.layer == 7 && Toughness != 0)
        {
            ToughnessText.text = (Toughness - 1).ToString();
            brickGenerator.BrickArray[Row, Column] = Toughness - 1;
        }

        // Destroy BrickPrefab when Toughness is 0
        if (int.Parse(ToughnessText.text) == 0)
        {
            brickGenerator.BrickArray[Row, Column] = 0;
            Destroy(this.gameObject);
        }
    }

    public void SetIndex(int i, int j)
    {
        Row = i;
        Column = j;;
    }
}
