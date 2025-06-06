using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;

    public int totalTurns = 100;
    public float secondsPerTurn = 0.1f;

    public GameObject treePrefab;
    public GameObject factoryPrefab;
    public GameObject animalPrefab;
    public GameObject cellPrefab;

    private int currentTurn = 0;
    private GridManager gridManager;
    private ResourceManager manager;
    private Tree tree;
    private Factory factory;
    public bool isSimulating = false;
    public int treeAmount;

    private List<Entity> entities = new List<Entity>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        tree = FindObjectOfType<Tree>();   
        factory = FindObjectOfType<Factory>();
        manager = FindObjectOfType<ResourceManager>();
        InitializeEntities();
        StartCoroutine(RunSimulation());
    }

    void InitializeEntities()
    {
        // 나무 100그루 초기 배치
        for (int i = 0; i < treeAmount * 4; i++)
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

            factory.OnTurnStart();

            // 사용자 나무 편집 (스킵하려면 비활성화)
            yield return StartCoroutine(UIManager.Instance.HandleTreeEditing());

            UpdateEntityList();
            foreach (Entity e in entities)
                e.OnTurnPassed();

            tree.OnTurnEnd();

            StatsTracker.Instance.RecordTurn(currentTurn);
            TrySpawnFactoryRandomly();

            yield return new WaitForSeconds(secondsPerTurn);

            if (ResourceManager.Instance.energy <= 0)
            {
                isSimulating = false;
                Debug.Log("시뮬레이션 종료");
                Application.Quit();
            } 
                
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
            ResourceManager.Instance.factoryAmount++;
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

    //public int pollutionSpreadRate = 1; // every X turns
    //public int maxTurns = 300;
    //public Text pollutionText;
    //public Text biodiversityText;
    //public GameObject fireEffect;

    //private int pollutionLevel = 0;
    //private int biodiversityScore = 100;
    //private bool disasterActive = false;

    //public void SimulateTurn()
    //{
    //    RandomDisaster();
    //    UpdateBiodiversity();
    //    UpdateUI();

    //    if (currentTurn >= maxTurns)
    //    {
    //        EndSimulation();
    //    }
    //}

    //void RandomDisaster()
    //{
    //    if (Random.value < 10f && !disasterActive) // 10% chance
    //    {
    //        disasterActive = true;
    //        StartCoroutine(TriggerForestFire());
    //    }
    //}

    //IEnumerator TriggerForestFire()
    //{
    //    Debug.Log("Forest fire triggered!");
    //    Vector2Int pos = gridManager.GetRandomTreePosition();
    //    Debug.Log(gridManager.GetRandomTreePosition());
    //    if (pos != Vector2Int.zero)
    //    {
    //        TryPlaceEntity(cellPrefab, 10);
    //        gridManager.DestroyTreesInRadius(pos, 2);
    //        pollutionLevel += 20;
    //    }
    //    yield return new WaitForSeconds(3f);
    //    disasterActive = false;
    //}

    //void UpdateBiodiversity()
    //{
    //    int treeCount = gridManager.GetTreeCount();
    //    int animalCount = gridManager.GetAnimalCount();

    //    biodiversityScore = Mathf.Clamp(treeCount + animalCount - pollutionLevel, 0, 100);
    //}

    //void UpdateUI()
    //{
    //    if (pollutionText != null) pollutionText.text = "Pollution: " + pollutionLevel;
    //    if (biodiversityText != null) biodiversityText.text = "Biodiversity: " + biodiversityScore;
    //}

    //void EndSimulation()
    //{
    //    Debug.Log("Simulation ended. Final Biodiversity Score: " + biodiversityScore);
    //    // Add win/loss condition here
    //}

}
