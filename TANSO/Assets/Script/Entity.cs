using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Vector2Int position;  // Left-bottom corner
    public int size;
    public abstract void OnTurnPassed();  // 매 턴마다 실행
}
