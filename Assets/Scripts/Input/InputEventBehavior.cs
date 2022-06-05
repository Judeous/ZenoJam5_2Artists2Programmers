using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventBehavior : MonoBehaviour
{
    private PlayerMovementBehavior _playerMovementBehavior;
    [SerializeField] SlashSpawnerBehavior _splashSpawnerBehavior;
    private Camera _camera;

    private float _timeFireHeld = 0;
    private float _timeBeforeFireMoves = 0.1f;

    public Camera Camera
    {
        set { _camera = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerMovementBehavior = GetComponent<PlayerMovementBehavior>();
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        //Get movement input
        Vector3 keyboardInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Get touch input
        float touchInput = Input.GetAxis("Fire1");
        bool movingWithKeyboard = false;

        //If the keyboard movement is being used
        if (keyboardInput.magnitude != 0)
        {
            //Normalize movement
            //if (keyboardInput.magnitude != 1) keyboardInput.Normalize();

            //Move in the calculated direction
            //_playerMovementBehavior.Move(keyboardInput);
            movingWithKeyboard = true;

            //Debug.Log("KeyboardMove: " + keyboardInput);
        }
        //If the touch input is being used
        if (touchInput != 0)
        {
            //If the time that Fire has been held is greater than the time it takes to start moving
            _timeFireHeld += Time.deltaTime;
            if (!movingWithKeyboard && _timeFireHeld > _timeBeforeFireMoves )
            {
                //Move the player based on the position of the touch
                _playerMovementBehavior.TouchMove();

                //Debug.Log("CursorMove: " + touchDirection);
            }
            return;
        }
        //If Fire input is released, reset the time holding it
        else
        {
            //If the player just tapped and released Fire, shoot
            if (_timeFireHeld != 0 && _timeFireHeld < _timeBeforeFireMoves)
            {
                _splashSpawnerBehavior.TryFire();
            }
            _timeFireHeld = 0;
        }
    }
}
