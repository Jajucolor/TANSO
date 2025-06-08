using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{

    public static ResourceManager Instance;

    public RectTransform graphContainer;
    public GameObject dotPrefab;  // 작은 원형 UI 이미지 프리팹
    public Color carbonColor = Color.red;
    public Color treeColor = Color.green;
    public Color factoryColor = Color.gray;
    public Color animalColor = Color.blue;

    private List<GameObject> carbonDots = new List<GameObject>();
    private List<GameObject> treeDots = new List<GameObject>();
    private List<GameObject> factoryDots = new List<GameObject>();
    private List<GameObject> animalDots = new List<GameObject>();

    private float graphWidth = 600f;
    private float graphHeight = 200f;
    private float xSpacing = 10f; // 점 간격


    //public void UpdateGraph(List<int> carbonData, List<int> treeData, List<int> factoryData, List<int> animalData)
    //{
    //    ClearDots();

    //    DrawLine(carbonData, carbonColor, carbonDots);
    //    DrawLine(treeData, treeColor, treeDots);
    //    DrawLine(factoryData, factoryColor, factoryDots);
    //    DrawLine(animalData, animalColor, animalDots);
    //}

    void Start()
    {
        //UpdateGraph(ResourceManager.Instance.carbonData, treeDots, factoryDots animalDots);
        UpdateGraph(ResourceManager.Instance.carbonData);
        print(1);
    }

    void Awake()
    {

    }


    //void Update()
    //{
    //    //UpdateGraph(ResourceManager.Instance.carbonData, treeDots, factoryDots animalDots);
    //    UpdateGraph(ResourceManager.Instance.carbonData);
    //    print(ResourceManager.Instance.carbonData);
    //}

    public void UpdateGraph(List<int> carbonData)
    {
        ClearDots();

        DrawLine(carbonData, carbonColor, carbonDots);
    }




    void ClearDots()
    {
        foreach (var dot in carbonDots) Destroy(dot);
        foreach (var dot in treeDots) Destroy(dot);
        foreach (var dot in factoryDots) Destroy(dot);
        foreach (var dot in animalDots) Destroy(dot);

        carbonDots.Clear();
        treeDots.Clear();
        factoryDots.Clear();
        animalDots.Clear();
    }

    void DrawLine(List<int> data, Color color, List<GameObject> dots)
    {
        if (data.Count == 0) return;

        int maxValue = Mathf.Max(data.ToArray());
        maxValue = Mathf.Max(maxValue, 1); // 0 나누기 방지

        for (int i = 0; i < data.Count; i++)
        {
            float x = i * xSpacing;
            float y = ((float)data[i] / maxValue) * graphHeight;
            GameObject dot = Instantiate(dotPrefab, graphContainer);
            dot.GetComponent<Image>().color = color;
            dot.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            dots.Add(dot);

            // 점과 점을 잇는 선 (Optional)
            if (i > 0)
            {
                DrawLineBetween(dots[i - 1].GetComponent<RectTransform>().anchoredPosition,
                                dots[i].GetComponent<RectTransform>().anchoredPosition, color);
            }
        }
    }

    void DrawLineBetween(Vector2 start, Vector2 end, Color color)
    {
        GameObject lineObj = new GameObject("Line", typeof(Image));
        lineObj.transform.SetParent(graphContainer, false);
        Image image = lineObj.GetComponent<Image>();
        image.color = color;
        RectTransform rt = lineObj.GetComponent<RectTransform>();

        Vector2 dir = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        rt.sizeDelta = new Vector2(distance, 2f); // 두께 2
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0, 0);
        rt.anchoredPosition = start + dir * distance * 0.5f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rt.localEulerAngles = new Vector3(0, 0, angle);
    }


}
