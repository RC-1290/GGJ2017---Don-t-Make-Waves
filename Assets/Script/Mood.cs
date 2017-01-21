using UnityEngine;

public class Mood : MonoBehaviour {
    public float angriness = 0.0f;
    public float angrinessPerFixedUpdate = 0.01f;

    public Collider playerCollider;
    public Color startingColor;
    public Color angryColor;

    protected Material moodColorMat;
    protected int materialMoodId = -1;
    protected bool hittingPlayer = false;

    private void Start()
    {
        angriness = 0.0f;
        hittingPlayer = false;
        moodColorMat = GetComponent<MeshRenderer>().material;
        materialMoodId = Shader.PropertyToID("_Mood");
    }

    private void FixedUpdate()
    {
        if (hittingPlayer)
        {
            angriness += angrinessPerFixedUpdate;
            moodColorMat.SetFloat(materialMoodId, angriness);
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
