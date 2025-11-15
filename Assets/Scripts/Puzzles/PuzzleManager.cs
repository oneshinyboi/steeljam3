using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzles")]
    [Tooltip("All possible puzzle prefabs you've created.")]
    [SerializeField] private List<BasePuzzle> availablePuzzlePrefabs;

    [Tooltip("Where each puzzle will be spawned on the door UI.")]
    [SerializeField] private Transform[] puzzleSlots;

    [Tooltip("How many puzzles to spawn per run")]
    [SerializeField] private int puzzlesPerRun = 4;

    [Header("Events")]
    [Tooltip("Invoked when ALL active puzzles are completed.")]
    public UnityEvent OnAllPuzzlesCompleted;

    private readonly List<BasePuzzle> activePuzzles = new List<BasePuzzle>();
    private int completedCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnRandomPuzzles();
    }

    private void SpawnRandomPuzzles()
    {
        activePuzzles.Clear();
        completedCount = 0;

        if (availablePuzzlePrefabs.Count == 0 || puzzleSlots.Length == 0)
        {
            Debug.LogWarning("PuzzleManager: No puzzle prefabs or slots set.");
            return;
        }

        int countToSpawn = Mathf.Min(
            puzzlesPerRun,
            availablePuzzlePrefabs.Count,
            puzzleSlots.Length
        );

        // Temporary copy of list so we can pick without replacement
        List<BasePuzzle> pool = new List<BasePuzzle>(availablePuzzlePrefabs);

        for (int i = 0; i < countToSpawn; i++)
        {
            int index = Random.Range(0, pool.Count);
            BasePuzzle prefab = pool[index];
            pool.RemoveAt(index); // no duplicates this run

            Transform slot = puzzleSlots[i];

            BasePuzzle newPuzzle = Instantiate(prefab, slot);
            newPuzzle.transform.localPosition = Vector3.zero;
            newPuzzle.transform.localRotation = Quaternion.identity;
            newPuzzle.transform.localScale = Vector3.one;

            newPuzzle.Init(this);
            activePuzzles.Add(newPuzzle);
        }
    }

    public void HandlePuzzleCompleted(BasePuzzle puzzle)
    {
        if (!activePuzzles.Contains(puzzle))
            return;

        completedCount++;

        Debug.Log($"Puzzle completed: {puzzle.name} ({completedCount}/{activePuzzles.Count})");

        if (completedCount >= activePuzzles.Count)
        {
            Debug.Log("All puzzles completed!");
            OnAllPuzzlesCompleted?.Invoke();
        }
    }
}
