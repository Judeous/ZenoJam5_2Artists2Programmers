using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehavior : MonoBehaviour
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth = 20;

    [SerializeField] private bool _isPlayer = false;
    private PlayerManagerBehavior _playerManager;

    private float _invincibilityTime = 0.25f;
    private float _timeSinceDamaged = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;

        //Store a PlayerManager component if isPlayer is true
        if (_isPlayer)
        {
            _playerManager = GetComponent<PlayerManagerBehavior>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceDamaged += Time.deltaTime;

        if (!_isPlayer && _currentHealth <= 0)
        {
            //If this is a bug, make it flee
            EnemyJuneBugMovementBehavior behavior = GetComponent<EnemyJuneBugMovementBehavior>();
            if (behavior)
                behavior.Fleeing = true;

            //Lower the scale of the gameObject
            gameObject.transform.localScale *= 0.99f;
            //If it gets low enough, destroy it
            if (gameObject.transform.localScale.magnitude < 0.10f)
                Destroy(gameObject);
        }
    }

    public void TakeDamage(int value = 1)
    { 
        if (_timeSinceDamaged > _invincibilityTime)
        {
            //Lower health
            _currentHealth -= value;
            _timeSinceDamaged = 0;

            //If health has dropped to or below zero
            if (_currentHealth <= 0)
            {
                if (_isPlayer)
                {
                    //Respawn the player with the PlayerManager component
                    _playerManager.Respawn();
                    //Restore some health to the player
                    Heal(10, false, true);
                }
                else
                {
                    //May replace with making it flee, depending on whether this is an enemy or not and the artist wants it to flee
                    EnemyJuneBugMovementBehavior behavior = GetComponent<EnemyJuneBugMovementBehavior>();
                    if (behavior)
                        behavior.Fleeing = true;
                    //Destroy(gameObject);
                }
            }
        }
    }

    public void Heal(int value = 5, bool allowOverHeal = false, bool healHalfIfPossible = false)
    {
        //Calculate half of the player's missing health
        int halfOfMissingHealth = (_maxHealth - _currentHealth) / 2;

        //If half the missing health is greater than the passed in healing amount
        if (healHalfIfPossible && halfOfMissingHealth > value)
        {
            //Add half of the missing health
            _currentHealth += halfOfMissingHealth;
            return;
        }
        //Otherwise, add the passed in heal amount
        _currentHealth += value;

        //If this Heal isn't allowed to overheal and the Heal surpassed the max health
        if (!allowOverHeal && _currentHealth > _maxHealth)
        {
            //Clamp the health to the maximum
            _currentHealth = _maxHealth;
        }
    }
}
