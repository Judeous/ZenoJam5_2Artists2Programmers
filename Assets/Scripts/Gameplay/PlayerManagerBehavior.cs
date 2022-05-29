using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerBehavior : MonoBehaviour
{
    [SerializeField] private Transform respawnPosition;
    private HealthBehavior _healthBehavior;

    // Start is called before the first frame update
    void Start()
    {
        _healthBehavior = GetComponent<HealthBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        transform.position = respawnPosition.position;
        transform.rotation = respawnPosition.rotation;

    }
}
