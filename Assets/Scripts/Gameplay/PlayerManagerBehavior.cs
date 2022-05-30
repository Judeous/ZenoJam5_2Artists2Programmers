using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerBehavior : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private SlashSpawnerBehavior _slashSpawnerBehavior;
    private HealthBehavior _healthBehavior;
    private PlayerMovementBehavior _movementBehavior;

    // Start is called before the first frame update
    void Start()
    {
        _healthBehavior = GetComponent<HealthBehavior>();
        _movementBehavior = GetComponent<PlayerMovementBehavior>();
        _movementBehavior.Camera = _camera;
        _slashSpawnerBehavior.Camera = _camera;
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
