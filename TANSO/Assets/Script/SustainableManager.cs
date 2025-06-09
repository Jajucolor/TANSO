using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SustainableManager : MonoBehaviour
{
    public static SustainableManager Instance;
    public void InvestInRenewable(string type)
    {

        if (type == "solar" && ResourceManager.Instance.carbonCredits >= ResourceManager.Instance.solarPowerPlantCost)
        {
            ResourceManager.Instance.SpendCarbonCredits(ResourceManager.Instance.solarPowerPlantCost);
            ResourceManager.Instance.UseRenewableEnergy(type);
        }
        else if (type == "hydro" && ResourceManager.Instance.carbonCredits >= ResourceManager.Instance.hydroPowerPlantCost)
        {
            ResourceManager.Instance.SpendCarbonCredits(ResourceManager.Instance.hydroPowerPlantCost);
            ResourceManager.Instance.UseRenewableEnergy(type);
        }
        else if (type == "wind" && ResourceManager.Instance.carbonCredits >= ResourceManager.Instance.windPowerPlantCost)
        {
            ResourceManager.Instance.SpendCarbonCredits(ResourceManager.Instance.windPowerPlantCost);
            ResourceManager.Instance.UseRenewableEnergy(type);
        }
    }
}
