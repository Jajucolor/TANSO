using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    private bool[,] occupied;

    void Awake()
    {
        occupied = new bool[width, height];
    }

    public bool IsCellAvailable(int x, int y, int size)
    {
        if (x < 0 || y < 0 || x + size > width || y + size > height)
            return false;

        for (int i = x; i < x + size; i++)
        {
            for (int j = y; j < y + size; j++)
            {
                if (occupied[i, j])
                    return false;
            }
        }

        return true;
    }

    public void PlaceEntity(int x, int y, int size, GameObject prefab)
    {
        for (int i = x; i < x + size; i++)
        {
            for (int j = y; j < y + size; j++)
            {
                occupied[i, j] = true;
            }
        }

        Vector3 spawnPosition = new Vector3(x + size / 2f, y + size / 2f, 0);
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public void ClearCell(int x, int y, int size)
    {
        for (int i = x; i < x + size; i++)
        {
            for (int j = y; j < y + size; j++)
            {
                occupied[i, j] = false;
            }
        }
    }

    public void ClearAll()
    {
        occupied = new bool[width, height];
    }
}
