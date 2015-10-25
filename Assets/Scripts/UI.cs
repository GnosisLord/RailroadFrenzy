using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {
	public GameObject maintext;
	public GameObject subtext;
	public GameObject healthbar;
	public GameObject fuelbar;
	public Player player;
	private float messagetime;
	private bool textdisplay;
	private float refreshdelay;

	// Use this for initialization
	void Start () {
		messagetime = 0;
		refreshdelay = 0;
		if (player == null) {
			player = GameController.get ().getPlayer();
		}
	}
	// Update is called once per frame
	void Update () {
		if (player != null && refreshdelay <=0) {
			healthbar.GetComponent<RectTransform>().localPosition = new Vector3((200f*player.hp/player.hpmax)-200f,0f,0f);
			fuelbar.GetComponent<RectTransform>().localPosition = new Vector3(200f-(200f*player.fuel/player.fuelmax),0f,0f);
			refreshdelay = .1f;
		}
		if (player == null && refreshdelay <= 0) {
			healthbar.GetComponent<RectTransform>().localPosition = new Vector3(-200f,0f,0f);
		}
		if (messagetime > 0) {
			messagetime -= Time.deltaTime;
		} else if(textdisplay){
			maintext.GetComponent<Text> ().enabled = false;
			subtext.GetComponent<Text> ().enabled = false;
			textdisplay = false;
		}
		refreshdelay -= Time.deltaTime;
	}
	public void Message(string main, string sub, float time){
		maintext.GetComponent<Text> ().enabled = true;
		subtext.GetComponent<Text> ().enabled = true;
		maintext.GetComponent<Text> ().text = main;
		subtext.GetComponent<Text> ().text = sub;
		messagetime = time;
		textdisplay = true;
	}
}
