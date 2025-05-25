// ChallengeMechanics.cs
// Attach this script to a GameObject named "GameManager"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMechanics : MonoBehaviour
{
    public GridManager gridManager;
    public int pollutionSpreadRate = 1; // every X turns
    public int currentTurn = 0;
    public int maxTurns = 100;
    public Text pollutionText;
    public Text biodiversityText;
    public GameObject fireEffect;

    private int pollutionLevel = 0;
    private int biodiversityScore = 100;
    private bool disasterActive = false;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Simulate next turn
        {
            SimulateTurn();
        }
    }

    void SimulateTurn()
    {
        currentTurn++;
        SpreadPollution();
        RandomDisaster();
        UpdateBiodiversity();
        UpdateUI();

        if (currentTurn >= maxTurns)
        {
            EndSimulation();
        }
    }

    void SpreadPollution()
    {
        pollutionLevel += 5; // Example static increase
        gridManager.SpreadPollution();
    }

    void RandomDisaster()
    {
        if (Random.value < 0.1f && !disasterActive) // 10% chance
        {
            disasterActive = true;
            StartCoroutine(TriggerForestFire());
        }
    }

    IEnumerator TriggerForestFire()
    {
        Debug.Log("Forest fire triggered!");
        Vector2Int pos = gridManager.GetRandomTreePosition();
        if (pos != Vector2Int.zero)
        {
            Instantiate(fireEffect, gridManager.GetWorldPosition(pos), Quaternion.identity);
            gridManager.DestroyTreesInRadius(pos, 2);
            pollutionLevel += 20;
        }
        yield return new WaitForSeconds(3f);
        disasterActive = false;
    }

    void UpdateBiodiversity()
    {
        int treeCount = gridManager.GetTreeCount();
        int animalCount = gridManager.GetAnimalCount();

        biodiversityScore = Mathf.Clamp(treeCount + animalCount - pollutionLevel, 0, 100);
    }

    void UpdateUI()
    {
        if (pollutionText != null) pollutionText.text = "Pollution: " + pollutionLevel;
        if (biodiversityText != null) biodiversityText.text = "Biodiversity: " + biodiversityScore;
    }

    void EndSimulation()
    {
        Debug.Log("Simulation ended. Final Biodiversity Score: " + biodiversityScore);
        // Add win/loss condition here
    }
}
