using System;
using UnityEngine;

public class Player : Vehicle
{
    

    public void Start()
    {
        base.Start();
		friendly = true;
    }
    public void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.W))
        {
            Move(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(new Vector3(0, 0, -1));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(new Vector3(1, 0, 0));
        }
		if (Input.GetKey(KeyCode.UpArrow))
		{
			Face(0f);
			Fire ();
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Face((float)(270f));
			Fire ();
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Face((float) 180f);
			Fire ();
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Face((float) 90f);
			Fire ();
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Demo");
		}
    }
}
