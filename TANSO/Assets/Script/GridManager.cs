using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    private bool[,] occupied;
    public GameObject[,] grid;
    public GameObject pollutionPrefab;
    public GameObject treePrefab;
    public GameObject animalPrefab;

    void Awake()
    {
        occupied = new bool[width, height];
        grid = new GameObject[width, height];
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

    public void SpreadPollution()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null && grid[x, y].CompareTag("Factory"))
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (IsValidCell(nx, ny) && grid[nx, ny] == null)
                            {
                                GameObject pollution = Instantiate(pollutionPrefab, GetWorldPosition(new Vector2Int(nx, ny)), Quaternion.identity);
                                pollution.tag = "Pollution";
                                grid[nx, ny] = pollution;
                            }
                        }
                    }
                }
            }
        }
    }

    public Vector2Int GetRandomTreePosition()
    {
        List<Vector2Int> trees = new List<Vector2Int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null && grid[x, y].CompareTag("Tree"))
                {
                    trees.Add(new Vector2Int(x, y));
                }
            }
        }
        if (trees.Count == 0) return Vector2Int.zero;
        return trees[Random.Range(0, trees.Count)];
    }

    public void DestroyTreesInRadius(Vector2Int center, int radius)
    {
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                int x = center.x + dx;
                int y = center.y + dy;
                if (IsValidCell(x, y) && grid[x, y] != null && grid[x, y].CompareTag("Tree"))
                {
                    Destroy(grid[x, y]);
                    grid[x, y] = null;
                }
            }
        }
    }

    public int GetTreeCount()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null && grid[x, y].CompareTag("Tree"))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public int GetAnimalCount()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null && grid[x, y].CompareTag("Animal"))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, 0, gridPos.y); // Assumes flat grid
    }

    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    
}


