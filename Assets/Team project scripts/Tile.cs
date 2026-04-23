using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;

    [SerializeField] private SpriteRenderer tileRenderer;


    public void Init(bool isOffset)
    {
       tileRenderer.color = isOffset ? baseColor : offsetColor; 
    }
}
