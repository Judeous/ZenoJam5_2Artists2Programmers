using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventBehavior : MonoBehaviour
{
    private PlayerMovementBehavior _playerMovementBehavior;

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
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (movementInput.magnitude > 1) movementInput.Normalize();

        _playerMovementBehavior.Move(movementInput);
    }
}
