using UnityEngine;

public class Mood : MonoBehaviour {
    public float angriness = 0.0f;
    public float angerSpeed = 0.01f;

    

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
            MakeMoreAngry();
        }
        if (angriness > 1.0f)
        {
            director.DudeGotAngry();
        }
    }

    public void MakeMoreAngry()
    {
        angriness += angerSpeed;
        moodColorMat.SetFloat(materialMoodId, angriness);
    }

}
