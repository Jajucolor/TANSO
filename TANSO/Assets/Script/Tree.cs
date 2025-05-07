using UnityEngine;

public class Tree : Entity
{
    public int carbonAbsorption = 5;

    private void Start()
    {
        size = 2;
    }

    public override void OnTurnPassed()
    {
        StatsTracker.Instance.ReduceCarbon(carbonAbsorption);
    }
}
