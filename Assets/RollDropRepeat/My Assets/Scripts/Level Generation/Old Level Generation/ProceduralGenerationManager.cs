using System.Collections;
using UnityEngine;

public class ProceduralGenerationManager : MonoBehaviour
{
    public ProceduralGeneration[] proceduralGenerators;
    

    private void Start()
    {
        OnMiniGameStart();
        // Ensure all procedural generators start moving
        // foreach (ProceduralGeneration generator in proceduralGenerators)
        // {
        //     generator.ResumeGroundMovement();
        // }
    }

    // Call this method when the mini-game starts
    public void OnMiniGameStart()
    {
        // Stop movement for all procedural generators
        foreach (ProceduralGeneration generator in proceduralGenerators)
        {
            generator.StopGroundMovement();
        }
    }

    // Call this method when the mini-game ends
    public void OnMiniGameEnd()
    {
        // Resume movement for all procedural generators
        foreach (ProceduralGeneration generator in proceduralGenerators)
        {
            generator.ResumeGroundMovement();
        }
    }
}
