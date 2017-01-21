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

    protected bool isPlayerLayer(int gameObjectLayerNumber)
    {
        return ((1 << gameObjectLayerNumber) & playerLayer.value) > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerLayer(other.gameObject.layer))
        {
            hittingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlayerLayer(other.gameObject.layer))
        {
            hittingPlayer = false;
        }
    }
}
