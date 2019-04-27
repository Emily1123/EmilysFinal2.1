using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColider : MonoBehaviour
{
    public string punchName;
    public float damage;

    public Fighter owner;

    void OnTriggerEnter(Collider other)
    {
        Fighter somebody = other.gameObject.GetComponent<Fighter>();
        if (owner.Attacking)
        {
            if (somebody != null && somebody != owner)
            {
                somebody.Hurt(damage);
            }
        }
    }
}
