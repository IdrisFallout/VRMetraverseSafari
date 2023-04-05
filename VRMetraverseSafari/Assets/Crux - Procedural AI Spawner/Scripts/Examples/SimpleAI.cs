using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleAI : MonoBehaviour 
{
	int WanderCheck = 10;
	public int wanderRange = 40;
	public bool UseAnimations = true;
	UnityEngine.AI.NavMeshAgent Agent;
	Vector3 startPosition;
	Vector3 destination;
	Animation Anim;
	
	void Start () 
	{
		WanderCheck = Random.Range(8, 16);
		InvokeRepeating("Wander", 1.0f, WanderCheck);
		InvokeRepeating("UpdateAnimations", 1.0f, 0.5f);
		Anim = GetComponent<Animation>();
		Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		startPosition = this.transform.position;
		Agent.acceleration = 20;

		if (Anim == null)
		{
			UseAnimations = false;
		}
	}

	void Wander () 
	{
		destination = startPosition + new Vector3(Random.Range ((int)-wanderRange * 0.5f - 2, (int)wanderRange * 0.5f + 2), 0, Random.Range ((int)-wanderRange * 0.5f - 2, (int)wanderRange * 0.5f + 2));

		if (Agent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid){
			Agent.SetDestination(destination);
		}
	}

	void UpdateAnimations ()
	{
		if (UseAnimations)
		{
			if (Agent.velocity.sqrMagnitude <= 0)
			{
				Anim.CrossFade("Idle");
			}
			else if (Agent.velocity.sqrMagnitude > 0)
			{
				Anim.CrossFade("Move");
			}
		}
	}
}
