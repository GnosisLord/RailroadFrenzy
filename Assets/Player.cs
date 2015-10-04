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
			Fire(0f);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Fire((float)(270f));
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			Fire((float) 180f);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			Fire((float) 90f);
		}
    }
}
