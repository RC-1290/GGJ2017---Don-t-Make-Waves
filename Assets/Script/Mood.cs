using UnityEngine;

public class Mood : MonoBehaviour {
    public float angriness = 0.0f;

    public Collider playerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            angriness += 0.1f;
        }
    }
}
