using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace.Puzzles
{
    public class NewPuzzleManager : MonoBehaviour
    {
        public CarLockPuzzle carLockPuzzle;

        private void Awake()
        {
            carLockPuzzle.GameObject().SetActive(false);
        }

        public void OnCarLockButton()
        {
            carLockPuzzle.gameObject.SetActive(!carLockPuzzle.gameObject.activeSelf);
        }

    }
}