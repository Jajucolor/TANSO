using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsTracker : MonoBehaviour
{
    public static StatsTracker Instance;

    public LineRenderer carbonLine;
    public LineRenderer treeLine;
    public LineRenderer factoryLine;
    public LineRenderer animalLine;

    private List<int> carbonData = new List<int>();
    private List<int> treeData = new List<int>();
    private List<int> factoryData = new List<int>();
    private List<int> animalData = new List<int>();



    private int totalCarbon = 0;

    void Awake() => Instance = this;

    public void IncreaseCarbon(int amount) => totalCarbon += amount;

    public void ReduceCarbon(int amount) => totalCarbon = Mathf.Max(0, totalCarbon - amount);

    public void RecordTurn(int turn)
    {
        carbonData.Add(totalCarbon);
        treeData.Add(GameObject.FindObjectsOfType<Tree>().Length);
        factoryData.Add(GameObject.FindObjectsOfType<Factory>().Length);
        animalData.Add(GameObject.FindObjectsOfType<Animal>().Length);

        UpdateGraph(carbonLine, carbonData);
        UpdateGraph(treeLine, treeData);
        UpdateGraph(factoryLine, factoryData);
        UpdateGraph(animalLine, animalData);
    }

    void UpdateGraph(LineRenderer line, List<int> data)
    {


        int counts = data.Count;
        line.positionCount = counts;
        for (int i = 0; i < counts; i++)
        {

            line.SetPosition(i, new Vector3(i * 0.2f, data[i] * 0.1f, 0));
        }
    }
}