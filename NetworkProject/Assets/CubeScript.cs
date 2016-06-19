﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CubeScript : NetworkBehaviour
{

	struct CubeState
	{
		public float x;
		public float y;
	}

	[SyncVar]
	CubeState state;

	void Awake()
	{
		InitState();
	}

	void Update()
	{
		if (isLocalPlayer)
		{
			/*		KeyCode[] arrowKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow };
					foreach (KeyCode arrowKey in arrowKeys)
					{
						if (!Input.GetKeyDown(arrowKey)) continue;
						(arrowKey);
					} */
			CmdMoveOnServer();
		}
		SyncState();
	}
	[Server]
	void InitState()
	{
		state = new CubeState
		{
			x = 0,
			y = 0
		};
	}

	[Command]
	void CmdMoveOnServer()
	{
		state = Move();
	}

	void SyncState()
	{
		transform.position = new Vector2(state.x, state.y);
	}


	CubeState Move()
	{
		int dx = 0;
		int dy = 0;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			dx -= 1;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			dx += 1;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			dy += 1;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			dy -= 1;
		}
		/*
		switch (arrowKey)
		{
			case KeyCode.UpArrow:
				dy = 1;
				break;
			case KeyCode.DownArrow:
				dy = -1;
				break;
			case KeyCode.RightArrow:
				dx = 1;
				break;
			case KeyCode.LeftArrow:
				dx = -1;
				break;
		}
		*/
		return new CubeState
		{
			x = dx * Time.deltaTime + state.x,
			y = dy * Time.deltaTime + state.y
		};
	}
	/*
	int moveX = 0;
	int moveY = 0;
	float moveSpeed = 0.2f;

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		// input handling for local player only
		int oldMoveX = moveX;
		int oldMoveY = moveY;

		moveX = 0;
		moveY = 0;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			moveX -= 1;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			moveX += 1;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			moveY += 1;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			moveY -= 1;
		}
		if (moveX != oldMoveX || moveY != oldMoveY)
		{
			CmdMove(moveX, moveY);
		}
	}

	[Command]
	public void CmdMove(int x, int y)
	{
		moveX = x;
		moveY = y;
		//isDirty = true;
	}

	public void FixedUpdate()
	{
		if (NetworkServer.active)
		{
			transform.Translate(moveX * moveSpeed, moveY * moveSpeed, 0);
		}
	}*/
}
