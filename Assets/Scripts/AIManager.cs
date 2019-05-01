using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public GameSetting Setting;
    GameSetting.DifficultyLevel difficultyLevel;

    public GameObject opponent;
    public Fighter opponentFighter;
    public Fighter fighter;
    public float stoppingDistance;

    private float timeLeft;
    private int AttackMove;
    private int PreviousAttackMove;

    void Start()
    {
        if (SelectMode.Instance.isthereP2 == false)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animation/AI Animation Controller") as RuntimeAnimatorController;
            difficultyLevel = Setting.Get_DifficultyLevel();
            ResetTime();
            StartCoroutine("LoseTime");
        }
    }

    void ResetTime()
    {
        timeLeft = 10;
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    public void AIInput()
    {
        if (difficultyLevel == GameSetting.DifficultyLevel.Easy)
        {
            if (timeLeft <= 6)
            {
                DoAnimations();
            }
        }
        if (difficultyLevel == GameSetting.DifficultyLevel.Normal)
        {
            if (timeLeft <= 7)
            {
                DoAnimations();
            }
        }
        if (difficultyLevel == GameSetting.DifficultyLevel.Hard)
        {
            if (timeLeft <= 9)
            {
                DoAnimations();
            }
        }
    }

    bool IsMoving()
    {
        float dist = Vector3.Distance(opponent.transform.position, this.transform.position);

        if (dist <= stoppingDistance)
        {
            return false;
        }
        return true;
    }

    void DoAnimations()
    {
        fighter.animator.SetBool("Walk", IsMoving());
        if (!IsMoving())
        {
            AttackMove = Random.Range(1, 5);
            if (difficultyLevel == GameSetting.DifficultyLevel.Normal)
            {
                if(AttackMove == PreviousAttackMove)
                {
                    AttackMove = Random.Range(1, 5);
                }
            }
            if (difficultyLevel == GameSetting.DifficultyLevel.Hard)
            {
                if (AttackMove == PreviousAttackMove)
                {
                    if(opponentFighter.HealthPercent <= 50)
                    {
                        AttackMove = Random.Range(1, 3);
                    }
                }
            }
            ResetAllAnims();
            switch (AttackMove)
            {
                case (1):
                    PreviousAttackMove = 1;
                    fighter.animator.SetTrigger("Ultimate");
                    foreach (Collider collider in fighter.colliders)
                    {
                        if (collider.tag == "punch")
                        {
                            collider.enabled = true;
                            StartCoroutine(fighter.DisableAfterDelay(1f, collider));
                        }
                    }
                    ResetTime();
                    break;
                case (2):
                    PreviousAttackMove = 2;
                    fighter.animator.SetTrigger("Kick");
                    foreach (Collider collider in fighter.colliders)
                    {
                        if (collider.tag == "punch")
                        {
                            collider.enabled = true;
                            StartCoroutine(fighter.DisableAfterDelay(1f, collider));
                        }
                    }
                    ResetTime();
                    break;
                case (3):
                    PreviousAttackMove = 3;
                    fighter.animator.SetTrigger("Punch");
                    foreach (Collider collider in fighter.colliders)
                    {
                        if (collider.tag == "punch")
                        {
                            collider.enabled = true;
                            StartCoroutine(fighter.DisableAfterDelay(1f, collider));
                        }
                    }
                    ResetTime();
                    break;
                case (4):
                    PreviousAttackMove = 4;
                    fighter.animator.SetTrigger("Dodge");
                    ResetTime();
                    break;
                case (5):
                    PreviousAttackMove = 5;
                    fighter.animator.SetBool("Duck", true);
                    ResetTime();
                    break;
            }
        }
    }

    void ResetAllAnims()
    {
        fighter.animator.ResetTrigger("Ultimate");
        fighter.animator.ResetTrigger("Kick");
        fighter.animator.ResetTrigger("Punch");
        fighter.animator.ResetTrigger("Dodge");
        fighter.animator.SetBool("Duck", false);
    }
}
