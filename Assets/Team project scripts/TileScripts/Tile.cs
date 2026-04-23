using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;

    [SerializeField] private SpriteRenderer tileRenderer;

    [SerializeField] private GameObject hoverTile;

    public void Init(bool isOffset)
    {
       tileRenderer.color = isOffset ? baseColor : offsetColor; 
    }

    private void Awake()
    {
        hoverTile.SetActive(false);
    }

    private void OnMouseEnter()
    {
       hoverTile.SetActive(true); 
    }

    private void OnMouseExit()
    {
        hoverTile.SetActive(false);
    }
}
