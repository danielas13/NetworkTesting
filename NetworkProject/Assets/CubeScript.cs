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
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				CmdMoveOnServer(KeyCode.LeftArrow);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				CmdMoveOnServer(KeyCode.RightArrow);
			}
			if (Input.GetKey(KeyCode.UpArrow))
			{
				CmdMoveOnServer(KeyCode.UpArrow);
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				CmdMoveOnServer(KeyCode.DownArrow);
			}
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
	void CmdMoveOnServer(KeyCode arrowKey)
	{
		state = Move(state, arrowKey);
	}

	void SyncState()
	{
		transform.position = new Vector2(state.x, state.y);
	}


	CubeState Move(CubeState previous, KeyCode arrowKey)
	{
		int dx = 0;
		int dy = 0;
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
		return new CubeState
		{
			x = dx*Time.deltaTime + previous.x,
			y = dy*Time.deltaTime + previous.y
		};
	}
		/*
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
						} * /
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
			state = Move(state);
		}

		void SyncState()
		{
			transform.position = new Vector2(state.x, state.y);
		}


		CubeState Move(CubeState previous)
		{
			int dx = 0;
			int dy = 0;

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				dx -= 2;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				dx += 2;
			}
			if (Input.GetKey(KeyCode.UpArrow))
			{
				dy += 2;
			}
			if (Input.GetKey(KeyCode.DownArrow))
			{
				dy -= 2;
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
			* /
			return new CubeState
			{
				x = dx + previous.x,
				y = dy + previous.y
			};
		}*/
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
