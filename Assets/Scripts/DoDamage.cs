using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public float damage;
    public Fighter owner;

    void Start()
    {
        owner = gameObject.GetComponentInParent<Fighter>();
    }

    void OnTriggerEnter(Collider other)
    {
        Fighter somebody = other.gameObject.GetComponent<Fighter>();
        if (somebody != owner && somebody != null)
        {
            somebody.Hurt(damage);
        }
    }
}
