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


	private bool haveTouch = false;
	private float lastSpeed = 0;
	private float idle1MaxTime = 5.0f;
	private float idle1Time = 0.0f;
	private Rigidbody rigdbody;
	private float distToGround;

	private IGameInput _gameInput;

	private IPlayerHealthState _healthState;
	private GameManager _gameManager;


	private void Awake()
	{
		_gameInput = new GameInput();
		_healthState = new NormalPlayerHealthState(this);
	}

	public void ChangeHealthState(IPlayerHealthState newState)
	{
		if( _healthState != newState )
		{
			newState.SetupState();
			_healthState = newState;
		}
	}

	private void ChangeIdle()
	{
		idle1Time += Time.fixedDeltaTime;
		if (idle1Time > idle1MaxTime)
		{
			idle1Time = 0;
			audioFx.PlayIdle();
			animator.SetBool("Idle1", true);
		}
		else
		{
			animator.SetBool("Idle1", false);
		}
	}

	// Use this for initialization
	void Start () {
		GameManager = GameObject.Find("GameManager");
		rigdbody = GetComponent<Rigidbody>();
		distToGround = GetComponent<Collider>().bounds.extents.y;
		_gameManager = GameManager.GetComponent<GameManager>();
	}


	// Update is called once per frame
	void Update () {

		if (_healthState != null)
		{
			_healthState.UpdatelHealthState();
		}

		if (!GameManager.GetComponent<GameManagerSettings>().GameOver)
		{ 
			if (life == 0)
			{
				animator.SetBool("isGameOver", true);
				animator.SetBool("IsDead", true);

				GameManager.GetComponent<GameManagerSettings>().GameOver = true;
				return;
			}

			if( IsGrounded() )
			{
				animator.SetBool("IsGround", true);

				if ( _gameInput.HoldJump() )
				{
					animator.SetBool("LoadJump", true);
					if (Acumulate <= 0)
					{
						audioFx.PlayCharging();
					}
					if (MaxAcumulate > Acumulate)
					{
						Acumulate += Time.fixedDeltaTime * MaxAcumulate / 2;
					}
					idle1Time = 0;
				}

				if ( _gameInput.ReleaseJump() )
				{
					animator.SetBool("LoadJump", false);
					audioFx.StopCharging();
					audioFx.PlayJump();
					rigdbody.velocity = new Vector3(0, Height + Acumulate, 0);
					Acumulate = 0;
				}
				else
				{
					ChangeIdle();
				}
				
			}
			else
			{
				animator.SetBool("IsHigh", rigdbody.velocity.y < 1);
				animator.SetBool("IsGround", false);
				Acumulate = 0;
			}
			
		}
		else
		{
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|dead"))
			{
				animator.Stop();
				animator.SetBool("IsDead", true);
			}
			else
			{
				if (animator.GetBool("IsDead"))
				{ 
					animator.SetBool("IsDead", false);
				}
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

	public void TakeAHit()
	{
		ChangeHealthState(new InvulnerablePlayerHealthState(this));
	}
}
