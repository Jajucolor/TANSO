using UnityEngine;

public class Animal : Entity
{
    private void Start() => size = 1;

    public override void OnTurnPassed() => TryReproduceNearTrees();

    void TryReproduceNearTrees()
    {
        float treeRadius = 3f;
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, treeRadius);
        foreach (var hit in nearby)
        {
            if (hit.GetComponent<Tree>() != null)
            {
                if (Random.value < 0.1f)
                    SimulationManager.Instance.SpawnAnimalNear(transform.position);
                break;
            }
        }
    }
}
