
using System;
using UnityEngine;
public class EnemyDrop : Enemy
{
	public GameObject[] drops;
	
	public void Destroy ()
	{
		GameObject.Instantiate (drops [GameController.Random (drops.Length)], transform.position,new Quaternion(0f,0f,0f,0f));
	}
}

