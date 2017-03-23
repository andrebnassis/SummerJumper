using Assets.Scripts.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.Core;
using System;

public class Player : MonoBehaviour {

	public AudioFX audioFx;
	public Animator animator;
	public float MaxAcumulate = 3;
	public float Acumulate = 0.0f;
	public float Height = 4;
	public int life = 4;
	public float Idle1MaxTime = 5.0f;
	public GameObject GameManager;

	public float timeInvunerable = 3f;

	public GameObject menina;
	public GameObject boiaL;
	public GameObject boiaR;
	public GameObject boiaPato;
	public GameObject parteCorpor1;
	public GameObject parteCorpor2;
	public GameObject parteCorpor3;
	public GameObject parteCorpor4;
	public Rigidbody Rigidbody { get; private set; }

	private bool haveTouch = false;
	private float lastSpeed = 0;
	
	private float idle1Time = 0.0f;

	private float distToGround;

	private IGameInput _gameInput;

	private IPlayerHealthState _healthState;
	private GameManager _gameManager;
	private IPlayerActionState _actionState;

	private void Awake()
	{
		_gameInput = new GameInput();
		_healthState = new NormalPlayerHealthState(this);
		_actionState = new IdlePlayerActionState(this);
	}

	// Use this for initialization
	void Start () {
		GameManager = GameObject.Find("GameManager");
		Rigidbody = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider>().bounds.extents.y;
		_gameManager = GameManager.GetComponent<GameManager>();
	}


	// Update is called once per frame
	void Update () {

        if (_healthState != null)
        {
            _healthState.UpdatelHealthState();
        }

        if (_actionState != null)
        {
            _actionState.UpdateAction();
        }

        if (!GameManager.GetComponent<GameManagerSettings>().GameOver)
		{
			if (_gameInput.HoldJump())
			{
				ChangeActionState(new HoldJumpPlayerActionState(this));
			}

			if (_gameInput.ReleaseJump())
			{
				ChangeActionState(new JumpPlayerActionState(this));
			}

			if (life == 0)
			{
				GameManager.GetComponent<GameManagerSettings>().GameOver = true;
				return;
			}
		}


	}
	
	public bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	public bool IsNearToGround(float distance)
	{
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + distance);
	}

	public void AddScore()
	{
		if(_healthState is NormalPlayerHealthState)
		{
			_gameManager.AddScore();
		}
	}

	public bool IsHigh()
	{
		Debug.Log("Velocity - " + Rigidbody.velocity.y);
		return Rigidbody.velocity.y < 1;
	}

	public void TakeAHit()
	{
		ChangeHealthState(new InvulnerablePlayerHealthState(this));
	}

	public void Jump()
	{
		Rigidbody.velocity = new Vector3(0, Height + Acumulate, 0);
	}

	public void ChangeActionState(IPlayerActionState newState)
	{
		if( !_actionState.Equals(newState) )
		{
			newState.SetupAction();
			_actionState = newState;
		}
	}
	
	public void ChangeHealthState(IPlayerHealthState newState)
	{
		if ( !_healthState.Equals(newState) )
		{
			newState.SetupState();
			_healthState = newState;
		}
	}
}
