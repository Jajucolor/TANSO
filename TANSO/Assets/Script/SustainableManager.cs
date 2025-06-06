using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustainableManager : MonoBehaviour
{
    public static SustainableManager Instance;
    public void InvestInRenewable(string type)
    {
        int cost = 20;
        if (ResourceManager.Instance.carbonCredits >= cost)
        {
            ResourceManager.Instance.SpendCarbonCredits(cost);
            ResourceManager.Instance.UseRenewableEnergy(type);
        }
    }
}
