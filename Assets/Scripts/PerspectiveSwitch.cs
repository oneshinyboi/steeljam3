using UnityEngine;

public class PerspectiveSwitch : MonoBehaviour
{
    public GameObject[] views;
    public float[] rotations;
    public int currentIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure only the first object starts active
        SetActiveObject(currentIndex);
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
}
