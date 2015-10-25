using UnityEngine;
using System.Collections;

public class EnemyGenerator : Enemy {
	//Generator Variables
	public bool generator; //Spawns additional enemies
	public GameObject child; //Enemy spawned if Generator
	public float spawntimer; //Time between enemy spawns
	private float spawning; //Time until next enemy spawn
	public Vector3 spawnoffset = new Vector3 (1f,0f,0f);

	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		if (aggroed) {
			if (generator) {
				if (spawning > 0) {
					spawning -= Time.deltaTime;
				} else {
					Instantiate (child, transform.position+spawnoffset, transform.rotation);
					spawning = spawntimer;
				}
			}
		}
	}
}
