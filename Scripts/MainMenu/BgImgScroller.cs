using UnityEngine;
using UnityEngine.UI;

//# Formatted and Commented

/// <summary>
/// Manages the background image's scrolling effect by adjusting its UV coordinates.
/// </summary>
public class BgImgLoper : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float x, y;

    public void Update() {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }
}
