using System;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {
	public Player player; 						//Reference to Player Object
	private static GameController controller;	//Singleton instance of GameController
	private static System.Random rng;			//Only need one random number generator
	private int killcount;						//Number of enemies killed

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
	//Global reference to Player object
	public Player getPlayer(){
		return player;
	}
	//Static reference to Singleton instance
	public static GameController get(){
		return controller;
	}
	//Static reference to random number generator
	public static int Random(int max){
		return rng.Next (max);
	}
	//Called to report vehicle destruction
	public void Death(Vehicle killed)
	{
		if(killed == player){
			Message("Game Over","Reset Esc to restart");
		}
		if(!killed.friendly){
			killcount+=1;
		}
	}
	//TODO message to display on UI
	public static void Message(String message, String submessage){

	}
}
