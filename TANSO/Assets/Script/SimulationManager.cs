using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;

    public int totalTurns = 100;
    public float secondsPerTurn = 0.1f;

    public GameObject treePrefab;
    public GameObject factoryPrefab;
    public GameObject animalPrefab;

    private int currentTurn = 0;
    private GridManager gridManager;
    private bool isSimulating = false;

    private List<Entity> entities = new List<Entity>();
    internal object treeCounts;
    internal object animalCounts;

    public object factoryCounts { get; internal set; }
    public object factoryCount { get; internal set; }

    private void Awake() => Instance = this;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        InitializeEntities();
        StartCoroutine(RunSimulation());
    }

    void InitializeEntities()
    {
        // 나무 100그루 초기 배치
        for (int i = 0; i < 400; i++)
            TryPlaceEntity(treePrefab, 2);

        // 동물 50마리 초기 배치
        for (int i = 0; i < 50; i++)
            TryPlaceEntity(animalPrefab, 1);
    }

    public void TryPlaceEntity(GameObject prefab, int size)
    {
        for (int attempt = 0; attempt < 50; attempt++)
        {
            int x = Random.Range(0, gridManager.width - size);
            int y = Random.Range(0, gridManager.height - size);

            if (gridManager.IsCellAvailable(x, y, size))
            {
                gridManager.PlaceEntity(x, y, size, prefab);
                return;
            }
        }
    }

    IEnumerator RunSimulation()
    {
        isSimulating = true;

        while (currentTurn < totalTurns)
        {
            currentTurn++;
            Debug.Log($"Turn {currentTurn} 시작 (Year {currentTurn * 3})");

            // 사용자 나무 편집 (스킵하려면 비활성화)
            yield return StartCoroutine(UIManager.Instance.HandleTreeEditing());

            UpdateEntityList();
            foreach (Entity e in entities)
                e.OnTurnPassed();

            StatsTracker.Instance.RecordTurn(currentTurn);
            TrySpawnFactoryRandomly();

            yield return new WaitForSeconds(secondsPerTurn);
        }

        isSimulating = false;
        Debug.Log("시뮬레이션 종료");
    }

    void UpdateEntityList()
    {
        entities.Clear();
        entities.AddRange(FindObjectsOfType<Entity>());
    }

    void TrySpawnFactoryRandomly()
    {
        if (Random.value < 0.4f) // 20% 확률
        {
            TryPlaceEntity(factoryPrefab, 4);
        }
    }

    public void SpawnAnimalNear(Vector3 pos)
    {
        if (Random.value < 0.3f)
        {
            for (int attempt = 0; attempt < 2; attempt++)
            {
                Vector3 offset = new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), 0);
                Vector3 newPos = pos + offset;

                int x = Mathf.RoundToInt(newPos.x);
                int y = Mathf.RoundToInt(newPos.y);

                if (gridManager.IsCellAvailable(x, y, 1))
                {
                    gridManager.PlaceEntity(x, y, 1, animalPrefab);
                    return;
                }
            }
        }
    }
}