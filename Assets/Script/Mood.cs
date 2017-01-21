using UnityEngine;

public class Mood : MonoBehaviour {
    public float angriness = 0.0f;
    public float angrinessPerFixedUpdate = 0.01f;

    public Collider playerCollider;

    protected bool hittingPlayer = false;

    private void Start()
    {
        angriness = 0.0f;
        hittingPlayer = false;
    }

    private void FixedUpdate()
    {
        if (hittingPlayer)
        {
            angriness += angrinessPerFixedUpdate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            hittingPlayer = false;
        }
    }
}
