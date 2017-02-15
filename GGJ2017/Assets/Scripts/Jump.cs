using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public AudioFX audioFx;
    public Animator animator;
    public float MaxAcumulate = 3;
    public float Acumulate = 0.0f;
    public float Height = 4;
    public bool HitHead = false;
    private Rigidbody rigdbody;
    private float distToGround;
    public int life = 4;
    public GameObject GameManager;
    private bool haveTouch = false;
    private float lastSpeed = 0;
    public bool isInvulnerable = false;
    public float timeInvunerable = 3f;
    private float periodInvulnerable = 0f;
    public GameObject menina;
    public GameObject boiaL;
    public GameObject boiaR;
    public GameObject boiaPato;
    public GameObject parteCorpor1;
    public GameObject parteCorpor2;
    public GameObject parteCorpor3;
    public GameObject parteCorpor4;
    private float idle1MaxTime = 5.0f;
    private float idle1Time = 0.0f;


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
    }


	// Update is called once per frame
	void Update () {

        if (!GameManager.GetComponent<GameManagerSettings>().GameOver)
        { 
            if (life == 0)
            {
                animator.SetBool("isGameOver", true);
                animator.SetBool("IsDead", true);

                GameManager.GetComponent<GameManagerSettings>().GameOver = true;
                return;
            }
            if( HitHead )
            {
                if(life > 1) { 
                SetInvunerableState();
                
                periodInvulnerable += Time.fixedDeltaTime;

                if (life == 4)
                {
                    boiaPato.SetActive(false);
                }
                else if (life == 3)
                {
                    boiaL.SetActive(false);
                }
                else if (life == 2)
                {
                    boiaR.SetActive(false);
                }

                    if (periodInvulnerable > timeInvunerable)
                    {
                        periodInvulnerable = 0;
                        HitHead = false;
                        life--;


                        SetVunerableState();

                    }
                }else
                {
                    life--;
                    menina.SetActive(true);
                    parteCorpor1.SetActive(true);
                    parteCorpor2.SetActive(true);
                    parteCorpor3.SetActive(true);
                    parteCorpor4.SetActive(true);
                    return;
                }
            
            }
            

            if( IsGrounded() )
            {
                animator.SetBool("IsGround", true);

                if ( JumpInput(true) )
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

                if (JumpInput(false))
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

            menina.SetActive(true);
            parteCorpor1.SetActive(true);
            parteCorpor2.SetActive(true);
            parteCorpor3.SetActive(true);
            parteCorpor4.SetActive(true);
        }
    }

    private bool JumpInput(bool hold)
    {
        if( hold )
        {
            return Input.GetButton("Jump") || TouchDown();
        }


        return Input.GetButtonUp("Jump") || TouchRelease();
    }

    private bool TouchRelease()
    {
        bool b = false;
        for (int i = 0; i < Input.touches.Length; i++)
        {
            b = Input.touches[i].phase == TouchPhase.Ended;
            if (b)
                break;
        }

        return b;
    }

    private bool TouchDown()
    {
        return Input.touchCount > 0;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public bool IsNearToGround(float distance)
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + distance);
    }

    private void SetInvunerableState() 
    {
        isInvulnerable = true;
        if (menina.activeSelf)
        {
            menina.SetActive(false);
            parteCorpor1.SetActive(false);
            parteCorpor2.SetActive(false);
            parteCorpor3.SetActive(false);
            parteCorpor4.SetActive(false);
        }
        else
        {
            menina.SetActive(true);
            parteCorpor1.SetActive(true);
            parteCorpor2.SetActive(true);
            parteCorpor3.SetActive(true);
            parteCorpor4.SetActive(true);
        }
       

        
    }

    private void SetVunerableState()
    {
        parteCorpor1.SetActive(true);
        parteCorpor2.SetActive(true);
        parteCorpor3.SetActive(true);
        parteCorpor4.SetActive(true);
        menina.SetActive(true);
        isInvulnerable = false;

    }

}
