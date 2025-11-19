using System;
using UnityEngine;

public class PerspectiveSwitch : MonoBehaviour
{
    public static PerspectiveSwitch current;
    public GameObject[] views;
    public float[] rotations;


    public GameObject gameOverScreen;
    public GameObject winScreen;

    private int currentIndex = 0;

    private void Awake()
    {
        current = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCarController.current.PlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
        // Ensure only the first object starts active
        SetActiveObject(currentIndex);
    }

    void GameOverScreen()
    {
        PlayerCarController.current.PlayerDied -= GameOverScreen;
        Time.timeScale = 0;
        DisableAllViews();
        gameOverScreen.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0;
        DisableAllViews();
        winScreen.SetActive(true);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentIndex++;

            if (currentIndex >= views.Length) 
                currentIndex = 0;

            SetActiveObject(currentIndex);
        }
    }

    void SetActiveObject(int index)
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].SetActive(i == index);
        }
        Camera.main.transform.rotation = Quaternion.Euler(0, rotations[index], 0);

    }

    void DisableAllViews()
    {
        for (int i = 0; i < views.Length; i++)
        {
            views[i].SetActive(false);
        }
    }
}
