using System.Drawing;
using UnityEngine;


public class Tree : Entity
{
    public int carbonAbsorption = 5;

    private void Start()
    {
        if (ResourceManager.Instance.CanAffordTree())
        {
            ResourceManager.Instance.PlantTree();
        }
        else
        {
            Destroy(gameObject); // Not enough credits
        }

        size = 2;
    }

    public void OnTurnEnd()
    {
        ResourceManager.Instance.MaintainTree();
        ResourceManager.Instance.AdjustBiodiversity(0.01f);
    }

    public override void OnTurnPassed()
    {
        StatsTracker.Instance.ReduceCarbon(carbonAbsorption);
    }
}

