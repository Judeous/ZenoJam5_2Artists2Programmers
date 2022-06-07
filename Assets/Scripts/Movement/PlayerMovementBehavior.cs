using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    [SerializeField] public float MovementSpeed = 2;
    private Camera _camera;
    private Vector3 _touchMoveDirection;
    private bool _touchMoving;

    public Camera Camera
    {
        set { _camera = value; }
    }

    public Vector3 Velocity
    {
        get { return _velocity; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void TouchMove()
    {
        //_touchMoveDirection = direction;
        _touchMoving = true;
    }

    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        //If not moving by touch
        if (!_touchMoving)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            //The direction the player is moving in is set to the input values for the horizontal and vertical axis
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

            // For some reason, doing this makes movement drag on after releasing input
            //Normalize the direction vector to prevent moving faster diagonally
            //direction.Normalize();
            
            //The move direction is scaled by the movement speed to get velocity
            Vector3 velocity = direction * MovementSpeed * Time.deltaTime;
            //Call to make the rigidbody smoothly move to the desired position
            _rigidbody.MovePosition(transform.position + velocity);


            //Create a Ray from the camera based on the cursor's position on the screen
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //Get a point at a specified distance from the camera
            Vector3 point = ray.GetPoint(13);
            //Find the direction from the player to the point
            Vector3 lookDir = new Vector3(point.x, transform.position.y, point.z) - transform.position;
            //Create a rotation to the look direction
            Quaternion rotation = Quaternion.LookRotation(lookDir);

            ////Lerp to the new rotation
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, rotation, 0.15f);
        }
        //If moving by touch
        else
        {
            //Create a ray that starts at a screen point
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //Create a point a certain distance away from the camera
            Vector3 point = ray.GetPoint(13);
            //Find the direction the player should move towards
            Vector3 touchDirection = point - transform.position;
            //Zero out the y so the character does not try to fly/noclip into the ground
            touchDirection.y = 0;
            //Normalize the magnitude
            if (touchDirection.magnitude != 1) touchDirection.Normalize();

            //The move direction is scaled by the movement speed to get velocity
            Vector3 velocity = touchDirection * MovementSpeed * Time.deltaTime;

            //Call to make the rigidbody smoothly move to the desired position
            _rigidbody.MovePosition(transform.position + velocity);

            //Look towards the new position
            transform.forward = Vector3.Lerp(transform.forward, touchDirection, 0.2f);

            //Set touchMoving to false to prevent sliding in the this touch direction after input stops
            _touchMoving = false;
        }
    }
}
