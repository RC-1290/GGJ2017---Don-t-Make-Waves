using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameDirector : MonoBehaviour {

    public GameObject personPrefab;
	public LayerMask playerLayer;
	public Transform startPoint;
    public Transform target;

	public string LeftTriggerAxisName = "LeftTrigger";
	public string RightTriggerAxisName = "RightTrigger";

	public Level1 level1;

    protected List<GameObject> people = new List<GameObject>();

    protected bool bored = true;
	protected bool gameOver = false;

    void Start()
    {

    }

    void Update()
    {
        if (bored)
        {
			level1.RunLevel(this);
            bored = false;
        }
		if (gameOver)
		{
			if (Input.GetAxis(LeftTriggerAxisName) >= 1.0f || Input.GetAxis(RightTriggerAxisName) >= 1.0f)
			{
				ResetScene();
			}
		}
    }

    public void DudeGotAngry()
    {
		gameOver = true;
    }

	public void AddPerson(Vector3 spawn, Quaternion rotation, Vector3 target)
	{
		GameObject person = Instantiate(personPrefab, spawn, rotation);
        people.Add(person);
		person.name = "Person " + people.Count;

		Mood personMood = person.GetComponent<Mood>();
        personMood.director = this;
		
        NavMeshAgent agent = person.GetComponent<NavMeshAgent>();
		if (spawn != target)
		{ agent.SetDestination(target); }

		PushOver pushover = person.GetComponent<PushOver>();
		pushover.director = this;
	}

	protected void ResetScene()
	{
		 // delete all people and restart
        for (int i = 0; i < people.Count; i++)
        {
            GameObject person = people[i];
            Destroy(person);
        }
        people.Clear();
        bored = true;
		gameOver = false;
	}

	public bool isPlayerLayer(int gameObjectLayerNumber)
    {
        return ((1 << gameObjectLayerNumber) & playerLayer.value) > 0;
    }

}
