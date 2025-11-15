using UnityEngine;

public class RoadTextureScroll : MonoBehaviour
{
    public Renderer roadRenderer;
    public float scrollSpeed = 0.5f;

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        roadRenderer.material.mainTextureOffset = new Vector2(offset * -1, 0);
    }
}
