using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum AttackType { punch = 0, kick = 1, dodge = 2 };
public class Combos : MonoBehaviour
{
    public Fighter owner;
    public Attack punchAttack;
    public Attack kickAttack;
    public Attack dodgeAttack;
    public List<Combo> combos;
    public float comboLeeway;
    public GameObject projectile;
    public GameObject projectileStart;
    public float projectileSpeed;
    Attack curAttack = null;
    ComboInput lastInput = null;
    List<int> currentCombos = new List<int>();
    public Animator animator;
    float timer = 0;
    float leeway = 0;
    bool skip = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        PrimeCombos();
    }

    public void SpawnFire()
    {
        GameObject fireball = Instantiate(projectile, projectileStart.transform.position, projectileStart.transform.rotation) as GameObject;
        fireball.transform.parent = gameObject.transform;
        fireball.GetComponent<Rigidbody>().AddForce(projectileSpeed, 0, 0);
    }

    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputted.AddListener(() =>
            {
                skip = true;
                Attack(c.comboAttack);
                animator.SetTrigger(owner.axisPrefix + c.comboAttack.name);
                //animator.ResetTrigger(owner.axisPrefix + c.comboAttack.name);
                ResetCombos();
            });
        }
    }

    void Update()
    {
        if (curAttack != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                curAttack = null;
            }
            return;
        }

        if (currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;
            if (leeway >= comboLeeway)
            {
                if (lastInput != null)
                {
                    Attack(getAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        }
        else
        {
            leeway = 0;
        }

        ComboInput input = null;

        if (Input.GetButtonDown(owner.axisPrefix + "Punch"))
        {
            input = new ComboInput((AttackType.punch));
        }
        if (Input.GetButtonDown(owner.axisPrefix + "Kick"))
        {
            input = new ComboInput((AttackType.kick));
        }
        if (Input.GetButtonDown(owner.axisPrefix + "Dodge"))
        {
            input = new ComboInput((AttackType.dodge));
        }
        if (input == null)
        {
            return;
        }

        lastInput = input;

        List<int> remove = new List<int>();
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            if (c.ContinueCombo(input))
            {
                leeway = 0;
            }
            else
            {
                remove.Add(i);
            }
        }

        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i))
            {
                continue;
            }
            if (combos[i].ContinueCombo(input))
            {
                currentCombos.Add(i);
                leeway = 0;
            }
        }

        foreach (int i in remove)
        {
            currentCombos.RemoveAt(i);
        }

        if (currentCombos.Count <= 0)
        {
            Attack(getAttackFromType(input.type));
        }
    }

    void ResetCombos()
    {
        leeway = 0;
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }
        currentCombos.Clear();
    }

    void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(att.name))
        {
            animator.Play(att.name);
        }
    }

    Attack getAttackFromType(AttackType t)
    {
        if (t == AttackType.punch)
        {
            return punchAttack;
        }
        if (t == AttackType.kick)
        {
            return kickAttack;
        }
        if (t == AttackType.dodge)
        {
            return dodgeAttack;
        }
        return null;
    }
}

[Serializable]
public class Attack
{
    public string name;
    public float length;
}

[Serializable]
public class ComboInput
{
    public AttackType type;

    public ComboInput(AttackType t)
    {
        type = t;
    }

    public bool IsSameAs(ComboInput test)
    {
        return (type == test.type);
    }
}
[Serializable]
public class Combo
{
    public string name;
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInputted;
    int curInput = 0;

    public bool ContinueCombo(ComboInput i)
    {
        if (inputs[curInput].IsSameAs(i))
        {
            curInput++;
            if (curInput >= inputs.Count)
            {
                onInputted.Invoke();
                curInput = 0;
            }
            return true;
        }
        else
        {
            curInput = 0;
            return false;
        }
    }

    public ComboInput CurrentComboInput()
    {
        if (curInput >= inputs.Count)
        {
            return null;
        }
        return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}
