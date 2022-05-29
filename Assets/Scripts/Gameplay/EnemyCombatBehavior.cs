using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyJuneBugMovementBehavior))]
[RequireComponent(typeof(HealthBehavior))]
public class EnemyCombatBehavior : MonoBehaviour
{
    private EnemyJuneBugMovementBehavior _behavior;

    // Start is called before the first frame update
    void Start()
    {
        _behavior = GetComponent<EnemyJuneBugMovementBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If what was collided with is the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            //Check to see if it has a HealthBehavior
            HealthBehavior otherHealth = collision.gameObject.GetComponent<HealthBehavior>();
            if (otherHealth)
            {
                //Damage the other thing
                otherHealth.TakeDamage();
                //Temporarily flee
                _behavior.Fleeing = true;
            }
        }
    }
}