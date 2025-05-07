using UnityEngine;

public class Factory : Entity
{
    public int carbonEmission = 20;
    public float animalDeathRadius = 10f;
    public float treeDeathRadius = 20f;

    private void Start()
    {
        size = 4;
    }

    public override void OnTurnPassed()
    {
        StatsTracker.Instance.IncreaseCarbon(carbonEmission);
        KillNearbyAnimals();
        KillNearbyTrees();
    }

    void KillNearbyAnimals()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, animalDeathRadius);
        
        foreach (var hit in hits)
        {
            Animal animal = hit.GetComponent<Animal>();
            if (animal != null)
                Destroy(animal.gameObject);
        }
    }
    void KillNearbyTrees()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, treeDeathRadius);
        foreach (var hit in hits)
        {
            Tree tree = hit.GetComponent<Tree>();
            if (tree != null)
                Destroy(tree.gameObject);
        }
    }
}
