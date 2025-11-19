using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CarLockPuzzle : MonoBehaviour
{
    [System.Serializable]
    public class RotatingLock
    {
        public int targetValue;
        public int currentValue;
        public TMP_Text display;
    }

    public TMP_Text codeDisplay;

    [SerializeField] private RotatingLock[] locks = new RotatingLock[3];

    private void Start()
    {
        SetupLocks();
    }

    public void SetupLocks()
    {
        foreach (var rotatingLock in locks)
        {
            rotatingLock.currentValue = 0;
            rotatingLock.targetValue = Random.Range(0, 9);
        }

        codeDisplay.text = locks[0].targetValue.ToString() + locks[1].targetValue.ToString() + locks[2].targetValue;
    }

    public void Increment(int index)
    {
        locks[index].currentValue += 1;
        locks[index].display.SetText(locks[index].currentValue.ToString());
        CheckIfSolved();
    }

    public void Decrement(int index)
    {
        locks[index].currentValue -= 1;
        locks[index].display.SetText(locks[index].currentValue.ToString());
        CheckIfSolved();

    }

    public void CheckIfSolved()
    {
        bool solved = true;
        for (int i = 0; i < locks.Length; i++)
        {
            if (locks[i].currentValue == locks[i].targetValue)
            {
                locks[i].display.color = Color.green;
            }
            else
            {
                locks[i].display.color = Color.red;
                solved = false;
            }
        }

        if (solved)
        {
            PerspectiveSwitch.current.Win();

        }
    }
}
