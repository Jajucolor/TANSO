// ResourceManager.cs
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int carbonCredits = 100;
    public int energy = 50;
    public float biodiversity = 1.0f; // 1.0 = balanced, 0 = collapsed

    public int treePlantCost = 10;
    public int treeMaintainReward = 2;
    public int factoryEnergyCost = 5;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanAffordTree()
    {
        return carbonCredits >= treePlantCost;
    }

    public void PlantTree()
    {
        carbonCredits -= treePlantCost;
    }

    public void MaintainTree()
    {
        carbonCredits += treeMaintainReward;
    }

    public bool CanPowerFactory()
    {
        return energy >= factoryEnergyCost;
    }

    public void PowerFactory()
    {
        energy -= factoryEnergyCost;
    }

    public void GainEnergy(int amount)
    {
        energy += amount;
    }

    public void AdjustBiodiversity(float amount)
    {
        biodiversity = Mathf.Clamp01(biodiversity + amount);
    }

    public void GainCarbonCredits(int amount)
    {
        carbonCredits += amount;
    }

    public void SpendCarbonCredits(int amount)
    {
        carbonCredits -= amount;
    }
}
