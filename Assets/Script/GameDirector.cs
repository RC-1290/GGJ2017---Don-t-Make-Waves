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
			AddPerson();
            bored = false;
        }
		if (gameOver)
		{
			Debug.Log("Trigger value:" +Input.GetAxis(LeftTriggerAxisName));
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

	protected void AddPerson()
	{
		GameObject person = Instantiate(personPrefab, startPoint.position, transform.rotation);
        people.Add(person);

		Mood personMood = person.GetComponent<Mood>();
        personMood.director = this;
		
        NavMeshAgent agent = person.GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);

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
