using UnityEngine;
using UnityEngine.UI;

public class CarLockPuzzle : BasePuzzle
{
    [System.Serializable]
    public class RotatingLock
    {
        public int targetValue;
        public int currentValue;
    }

    [SerializeField] private RotatingLock[] locks = new RotatingLock[3];

    public override void Init(PuzzleManager owningManager)
    {
        base.Init(owningManager);

        SetupLocks();
        CheckIfSolved();
    }

    public void SetupLocks()
    {

        foreach (var rotatingLock in locks)
        {
            rotatingLock.currentValue = 0;
            rotatingLock.targetValue = Random.Range(0, 9);
        }

    }

    private void OnLockValueChanged(int _)
    {

        CheckIfSolved();
    }


    public void CheckIfSolved()
    {

        for (int i = 0; i < locks.Length; i++)
        {
            if (locks[i].currentValue != locks[i].targetValue)
            {
                return;
            }

        }
        CompletePuzzle();
    }
}
