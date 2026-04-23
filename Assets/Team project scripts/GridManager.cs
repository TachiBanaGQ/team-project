using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                //Offset the colours
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 ==0);
                spawnedTile.Init(isOffset);

            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

}
