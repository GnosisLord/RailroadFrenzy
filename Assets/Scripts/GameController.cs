using System;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {
	public Player player;
	private static GameController controller;
	private static System.Random rng;
	private int killcount;

	// Use this for initialization
	void Start () {
		controller = this;
		rng = new System.Random ();
		killcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Demo");
		}
	}
	public Player getPlayer(){
		return player;
	}
	public static GameController get(){
		return controller;
	}
	public static int Random(int max){
		return rng.Next (max);
	}
	public void Death(Vehicle killed)
	{
		if(killed == player){
			Message("Game Over","Reset Esc to restart");
		}
		if(!killed.friendly){
			killcount+=1;
		}
	}
	public static void Message(String message, String submessage){

	}
}
