using UnityEngine;

public class BB_Trigger : MonoBehaviour
{
    int Row;
    int Column;
    
    
    public void SetIndex(int i, int j) {
        Row = i;
        Column = j;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 7) {
            BrickGenerator brickGenerator;
            brickGenerator = GameObject.Find("BrickGenerator").GetComponent<BrickGenerator>();
            Array.PrintArray(brickGenerator.BrickArray);
            brickGenerator.BrickArray[Row, Column] = 0;
            Array.PrintArray(brickGenerator.BrickArray);
            BallSpawner.newBalls++;
            Destroy(this.gameObject);
        }
    }
}
