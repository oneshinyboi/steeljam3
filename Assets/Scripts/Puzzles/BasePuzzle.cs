using UnityEngine;

public class BasePuzzle : MonoBehaviour
{
    protected PuzzleManager manager;
    public bool IsCompleted { get; private set; }

    public virtual void Init(PuzzleManager owningManager)
    {
        manager = owningManager;
        IsCompleted = false;
    }

    protected void CompletePuzzle()
    {
        if (IsCompleted) return;

        IsCompleted = true;

        if (manager != null)
        {
            manager.HandlePuzzleCompleted(this);
        }
        else
        {
            Debug.LogWarning($"{name} completed but has no PuzzleManager reference.");
        }
    }
}
