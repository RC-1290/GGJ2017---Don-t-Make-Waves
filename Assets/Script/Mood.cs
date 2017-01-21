using UnityEngine;

public class Mood : MonoBehaviour {
    public float angriness = 0.0f;
    public float angrinessPerFixedUpdate = 0.01f;

    public LayerMask playerLayer;

    public Color startingColor;
    public Color angryColor;

    public GameDirector director;

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
        if (angriness > 1.0f)
        {
            director.DudeGotAngry();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer.value) > 0)
        {
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            hittingPlayer = false;
        }
    }
}
