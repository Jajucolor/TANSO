using System.Drawing;
using UnityEngine;


public class Tree : Entity
{
    public int carbonAbsorption = 5;
    private ResourceManager manager;

    private void Start()
    {
        if (ResourceManager.Instance.CanAffordTree())
        {
            ResourceManager.Instance.PlantTree();
            ResourceManager.Instance.treeAmount++;
        }
        else
        {
            Destroy(gameObject); // Not enough credits
        }

        size = 2;
    }

    public void OnTurnEnd()
    {
        for (int i = 0; i < ResourceManager.Instance.treeAmount; i++) 
        {

            ResourceManager.Instance.AdjustBiodiversity(0.001f);

        }

        ResourceManager.Instance.MaintainTree();

    }

    public override void OnTurnPassed()
    {
        //StatsTracker.Instance.ReduceCarbon(carbonAbsorption);
    }
}

