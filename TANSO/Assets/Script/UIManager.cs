using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button confirmButton;
    public Text editModeText;

    private bool isEditing = false;
    private GridManager gridManager;
    private GameObject treePrefab;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        treePrefab = SimulationManager.Instance.treePrefab;

        confirmButton.onClick.AddListener(ConfirmEdit);
        confirmButton.gameObject.SetActive(false);
    }

    public IEnumerator HandleTreeEditing()
    {
        isEditing = true;
        confirmButton.gameObject.SetActive(true);
        editModeText.text = "���� ���� �� (��Ŭ��: �߰�, ��Ŭ��: ����)";

        while (isEditing)
        {
            HandleMouseInput();
            yield return null;
        }

        editModeText.text = "";
        confirmButton.gameObject.SetActive(false);
    }

    void ConfirmEdit()
    {
        isEditing = false;
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))  // ��Ŭ��: ���� �߰�
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int gridPos = Vector2Int.FloorToInt(worldPos);

            if (gridManager.IsCellAvailable(gridPos.x, gridPos.y, 2))
            {
                gridManager.PlaceEntity(gridPos.x, gridPos.y, 2, treePrefab);
            }
        }
        else if (Input.GetMouseButtonDown(1))  // ��Ŭ��: ���� ����
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapPoint(worldPos);

            if (col != null && col.GetComponent<Tree>())
            {
                Destroy(col.gameObject);
            }
        }
    }
}
