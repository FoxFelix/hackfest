using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_head_AI : MonoBehaviour {
	public Transform MoveRange;
	public Vector3 randomMove;
	public GameObject[] Characters;
	public GameObject Model;
	public GameObject Target;
	public float Speed;
	public Status State;
	public bool isPlaying;
	public enum Status
	{
		Normal,
		Attacking,
		Stuck
	}
	// Use this for initialization
	void Start () 
	{
		GetCharactor ();
		SetRandom ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (State == Status.Normal) 
		{
			for (int i = 0; i < Characters.Length; i++) 
			{
				if (Vector3.Distance (Target.transform.position, Characters[i].transform.position) < Target.GetComponent<SphereCollider>().radius) 
				{
					SetRandom ();
					Model.GetComponent<Animator> ().Play ("Fly");
				}
			}
			if (Vector3.Distance (Target.transform.position, Model.transform.position) < 0.5) 
			{
				Model.GetComponent<Animator> ().Play ("Idle1");
			} 
		}
		else if (State == Status.Attacking) 
		{
//			Target.transform.position = Characters [0].transform.position;
			if (Vector3.Distance (Target.transform.position, Model.transform.position) > 2) 
			{
				Model.GetComponent<Animator> ().Play ("Fly");
			} 
			else
			{
				Model.GetComponent<Animator> ().Play ("Attack1");
			}
		}
		else if(State == Status.Stuck)
		{ 
			Model.GetComponent<Animator> ().SetBool ("BeAttacked", true);
		}
		Model.transform.position = Vector3.Lerp (Model.transform.position,Target.transform.position, Speed*Time.deltaTime);
		Model.transform.LookAt (Target.transform.position);
	}
	public void SetRandom()
	{
		randomMove = new Vector3(Random.Range(MoveRange.position.x - MoveRange.localScale.x/2,MoveRange.position.x + MoveRange.localScale.x/2),
			Random.Range(MoveRange.position.y - MoveRange.localScale.y/2,MoveRange.position.y + MoveRange.localScale.y/2),
			Random.Range(MoveRange.position.z - MoveRange.localScale.z/2,MoveRange.position.z + MoveRange.localScale.z/2));
		Target.transform.position = new Vector3(randomMove.x,Target.transform.position.y,randomMove.z);
	}
	public void GetCharactor()
	{
		Characters = GameObject.FindGameObjectsWithTag ("Character");
	}
}
