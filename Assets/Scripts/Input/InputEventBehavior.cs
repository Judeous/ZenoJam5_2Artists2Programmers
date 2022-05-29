using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventBehavior : MonoBehaviour
{
    private PlayerMovementBehavior _playerMovementBehavior;
    [SerializeField] SlashSpawnerBehavior _splashSpawnerBehavior;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovementBehavior = GetComponent<PlayerMovementBehavior>();
    }

    public void OnMove()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Get movement input
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Normalize movement
        if (movementInput.magnitude > 1) movementInput.Normalize();
        //Actually move
        _playerMovementBehavior.Move(movementInput);

        //Get fire input
        float swordslash = Input.GetAxis("Fire1");
        if (swordslash != 0)
        { _splashSpawnerBehavior.TryFire(); }
    }
}
