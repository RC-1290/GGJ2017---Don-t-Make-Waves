using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameDirector : MonoBehaviour {

    public GameObject personPrefab;
    public Transform target;

    protected List<GameObject> people = new List<GameObject>();

    protected bool bored = true;

    void Start()
    {

    }

    void Update()
    {
        if (bored)
        {
            // add character
            GameObject person = Instantiate(personPrefab, transform.position, transform.rotation);
            people.Add(person);
            Mood personMood = person.GetComponent<Mood>();
            personMood.director = this;
            NavMeshAgent agent = person.GetComponent<NavMeshAgent>();
            agent.SetDestination(target.position);


            bored = false;
        }
    }

    public void DudeGotAngry()
    {
        // delete all people and restart
        for (int i = 0; i < people.Count; i++)
        {
            GameObject person = people[i];
            Destroy(person);
        }
        people.Clear();
        bored = true;
    }

}
