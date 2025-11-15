using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public float velocity = 15.0f;
    public float minX = -10.0f;
    public float maxX = 10.0f;
    private Collider selfCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selfCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(selfCollider.bounds.center, selfCollider.bounds.extents, transform.rotation);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider != selfCollider) // Don't detect collision with self
            {
                Debug.Log($"Player car collided with: {hitCollider.gameObject.name}");
            }
        }

        var position = transform.position;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += Time.deltaTime * velocity;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x += Time.deltaTime * -velocity;
        }

        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;
    }
}
