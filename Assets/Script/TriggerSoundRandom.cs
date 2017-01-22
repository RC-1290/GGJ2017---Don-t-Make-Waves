using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundRandom : MonoBehaviour {

	public float minWait = 10.0f;
	public float maxWait = 60.0f;

	protected AudioSource source;
	protected bool waiting = false;
	protected float resumeTime = 0.0f;


	private void Start()
	{
		source = GetComponent<AudioSource>();
	}


	private void Update()
	{
		if (!source.isPlaying)
		{
			if (waiting)
			{
				if (Time.timeSinceLevelLoad >= resumeTime)
				{
					source.Play();
				}
			}
			else
				{
				resumeTime = Time.timeSinceLevelLoad + Random.Range(minWait, maxWait);
				waiting = true;
			}
		}
	}
}
