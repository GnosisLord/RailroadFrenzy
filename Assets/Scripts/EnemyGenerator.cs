using UnityEngine;
using System.Collections;

public class EnemyGenerator : Enemy {
	//Generator Variables
	public bool generator; //Spawns additional enemies
	public GameObject child; //Enemy spawned if Generator
	public float spawntimer; //Time between enemy spawns
	private float spawning; //Time until next enemy spawn

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if (aggroed) {
			if (generator) {
				if (spawning > 0) {
					spawning -= Time.deltaTime;
				} else {
					Instantiate (child, transform.position, transform.rotation);
					spawning = spawntimer;
				}
			}
		}
	}
}
