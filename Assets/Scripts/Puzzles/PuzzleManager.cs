using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Definitions")]
    [SerializeField] private List<PuzzleDefinition> availablePuzzles;

    [Header("Door Icon Setup")]
    [SerializeField] private Transform[] iconSlots;             // slots on the door
    [SerializeField] private PuzzleIconController iconPrefab;   // icon prefab

    [Header("Puzzle UI Setup")]
    [SerializeField] private Transform puzzleUiParent;          // e.g. a panel on your main Canvas
    [SerializeField] private GameObject puzzleBackdrop;         // dim overlay behind puzzles (optional)

    [Header("Gameplay")]
    [SerializeField] private int puzzlesPerRun = 4;
    public UnityEvent OnAllPuzzlesCompleted;

    private readonly List<PuzzleIconController> activeIcons = new();
    private int completedCount = 0;

    private BasePuzzle activePuzzle = null;
    private PuzzleIconController activeIcon = null;

    private void Start()
    {
        SpawnRandomIcons();
    }

    private void SpawnRandomIcons()
    {
        completedCount = 0;
        activeIcons.Clear();

        if (availablePuzzles.Count == 0 || iconSlots.Length == 0)
        {
            Debug.LogWarning("PuzzleManager: No puzzle definitions or icon slots set.");
            return;
        }

        int countToSpawn = Mathf.Min(
            puzzlesPerRun,
            availablePuzzles.Count,
            iconSlots.Length
        );

        // simple random without replacement
        List<PuzzleDefinition> pool = new List<PuzzleDefinition>(availablePuzzles);

        for (int i = 0; i < countToSpawn; i++)
        {
            int index = Random.Range(0, pool.Count);
            PuzzleDefinition def = pool[index];
            pool.RemoveAt(index);

            Transform slot = iconSlots[i];
            PuzzleIconController icon = Instantiate(iconPrefab, slot);
            icon.transform.localPosition = Vector3.zero;
            icon.transform.localRotation = Quaternion.identity;
            icon.transform.localScale = Vector3.one;

            icon.Init(this, def);
            activeIcons.Add(icon);
        }

        if (puzzleBackdrop != null)
            puzzleBackdrop.SetActive(false);
    }

    /// <summary>
    /// Called by an icon when the player clicks it.
    /// </summary>
    public void OpenPuzzle(PuzzleDefinition def, PuzzleIconController icon)
    {
        // Only one puzzle open at a time
        if (activePuzzle != null)
            return;

        if (def == null || def.puzzlePrefab == null)
        {
            Debug.LogWarning("PuzzleManager.OpenPuzzle: invalid definition or prefab.");
            return;
        }

        activeIcon = icon;
        activeIcon.SetInteractable(false); // disable double-clicks

        if (puzzleBackdrop != null)
            puzzleBackdrop.SetActive(true);

        // Instantiate the actual puzzle UI
        activePuzzle = Instantiate(def.puzzlePrefab, puzzleUiParent);
        activePuzzle.transform.localPosition = Vector3.zero;
        activePuzzle.transform.localRotation = Quaternion.identity;
        activePuzzle.transform.localScale = Vector3.one;

        activePuzzle.Init(this);
    }

    /// <summary>
    /// Called by puzzles when they complete (BasePuzzle.CompletePuzzle).
    /// </summary>
    public void HandlePuzzleCompleted(BasePuzzle puzzle)
    {
        if (puzzle != activePuzzle)
        {
            // technically other puzzles could exist, but our flow uses only one at a time
            return;
        }

        // Clean up puzzle UI
        Destroy(activePuzzle.gameObject);
        activePuzzle = null;

        if (puzzleBackdrop != null)
            puzzleBackdrop.SetActive(false);

        // Mark icon solved
        if (activeIcon != null)
        {
            activeIcon.SetSolved(true);
            activeIcon = null;
        }

        completedCount++;
        Debug.Log($"Puzzle completed! {completedCount}/{activeIcons.Count}");

        if (completedCount >= activeIcons.Count)
        {
            Debug.Log("All puzzles completed!");
            OnAllPuzzlesCompleted?.Invoke();
        }
    }
}

[System.Serializable]
public class PuzzleDefinition
{
    public string id;                    // e.g. "FuseSlider"
    public Sprite iconSprite;            // what shows on the door
    public BasePuzzle puzzlePrefab;      // the actual UI mini-game prefab
}
