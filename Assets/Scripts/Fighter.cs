using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fighter : MonoBehaviour
{
    public enum PlayerIndex { Player1, Player2, PlayerAI };
    
    public AIManager manager;
    public static float MAX_HEALTH = 100f;
    
    public float health = MAX_HEALTH;
    public string fighterName;
    public bool enable;
    public string axisPrefix;
    public Collider[] colliders;
    public PlayerIndex playerIndex;
    public FighterStates currentState = FighterStates.Idle;
    public Fighter opponent;
    public Animator animator;
    
    private Rigidbody _myBody;
    private AudioSource _audioPlayer;

    void Start()
    {
        //StartCoroutine(DisableAfterDelay(0f, null));
        _myBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _audioPlayer = GetComponent<AudioSource>();
        switch (playerIndex)
        {
            case PlayerIndex.Player1:
                axisPrefix = "P1";
                break;

            case PlayerIndex.Player2:
                axisPrefix = "P2";
                break;

            case PlayerIndex.PlayerAI:
                axisPrefix = "AI";
                GetComponent<Combos>().enabled = false;
                break;
        }
    }

    void EnableColliders(bool status)
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = status;
        }
    } 

    public void playSound(AudioClip sound)
    {
        AudioManager.PlaySound(sound, _audioPlayer);
    }

    public void UpdateHumanInput()
    {
        if (Input.GetAxis(axisPrefix + "Horizontal") > 0)
        {
            animator.SetBool((axisPrefix + "Walk"), true);
        }
        else
        {
            animator.SetBool((axisPrefix + "Walk"), false);
        }

        if (Input.GetAxis(axisPrefix + "Horizontal") < 0)
        {
            animator.SetBool((axisPrefix + "WalkBack"), true);
        }
        else
        {
            animator.SetBool((axisPrefix + "WalkBack"), false);
        }

        if (Input.GetAxis(axisPrefix + "Vertical") < -0)
        {
            animator.SetBool((axisPrefix + "Duck"), true);
        }
        else
        {
            animator.SetBool((axisPrefix + "Duck"), false);
        }

        if (Input.GetButtonDown(axisPrefix + "Dodge"))
        { 
            //animator.SetTrigger(axisPrefix + "Dodge");
        }

        if (Input.GetButtonDown((axisPrefix + "Punch")))
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "punch")
                {
                    collider.enabled = true;
                    StartCoroutine(DisableAfterDelay(1f, collider));
                }
            }
            //animator.SetTrigger(axisPrefix + "Punch");
        }

        if (Input.GetButtonDown((axisPrefix + "Kick")))
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "kick")
                {
                    collider.enabled = true;
                    StartCoroutine(DisableAfterDelay(1f, collider));
                }
            }
            //animator.SetTrigger(axisPrefix + "Kick");
        }

        if (Input.GetButtonDown((axisPrefix + "Ultimate")))
        {
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "ultimate")
                {
                    collider.enabled = true;
                    StartCoroutine(DisableAfterDelay(1f, collider));
                }
            }
            animator.SetTrigger(axisPrefix + "Ultimate");
        }
    }

    public void UpdateAiInput()
    {
        manager.AIInput();
    }

    public IEnumerator DisableAfterDelay(float seconds, Collider col)
    {
        yield return new WaitForSeconds(seconds);
        col.enabled = false;
    }

    void Update()
    {
        animator.SetFloat(axisPrefix + "Health", HealthPercent);

        if (opponent != null)
        {
            animator.SetFloat(axisPrefix + "OpponentHealth", opponent.HealthPercent);
        }
        else
        {
            animator.SetFloat(axisPrefix + "OpponentHealth", 1);
        }

        if (enable)
        {
            if (playerIndex == PlayerIndex.PlayerAI)
            {
                UpdateAiInput();
            }
            else
            {
                UpdateHumanInput();
            }

        }

        if (health <= 0 && currentState != FighterStates.Dead)
        {
            animator.SetTrigger(axisPrefix + "Dead");
        }
    }

    public virtual void Hurt(float damage)
    {
        if (!Invulnerable)
        {
            if (health >= damage)
            {
                health -= damage;
            }
            else
            {
                health = 0;
            }

            if (health > 0)
            {
                animator.SetTrigger(axisPrefix + "TakeHit");
            }
        }
    }

    public bool Invulnerable
    {
        get
        {
            return currentState == FighterStates.TakeHit || currentState == FighterStates.Dead;
        }
    }

    public bool Attacking
    {
        get
        {
            return currentState == FighterStates.Attack;
        }
    }

    public float HealthPercent
    {
        get
        {
            return health / MAX_HEALTH;
        }
    }

    public Rigidbody Body
    {
        get
        {
            return this._myBody;
        }
    }
}