using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {

	public List<Transform> spawns;
	public List<Transform> targets;

	public void RunLevel(GameDirector director)
	{
		for (int i = 0; i < spawns.Count; i++)
		{
			director.AddPerson(spawns[i].position, spawns[i].rotation, targets[i].position);
		}
	}

	void Update ()
	{
		
	}
}
