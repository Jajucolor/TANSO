using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Cell : MonoBehaviour
{
    public bool occupied = false;
    public Entity occupyingEntity = null;

    public void SetColor(Color color) => GetComponent<SpriteRenderer>().color = color;
}
