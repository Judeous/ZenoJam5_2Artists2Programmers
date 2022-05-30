using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    [SerializeField] public float MovementSpeed = 1;
    private Camera _camera;
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

    public void Move(Vector3 direction)
    {
        //_velocity = direction * MovementSpeed * Time.deltaTime;
    }

    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        //The direction the player is moving in is set to the input values for the horizontal and vertical axis
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //The move direction is scaled by the movement speed to get velocity
        Vector3 velocity = moveDir * MovementSpeed * Time.deltaTime;

        //Call to make the rigidbody smoothly move to the desired position
        _rigidbody.MovePosition(transform.position + velocity);

        //Create a ray that starts at a screen point
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        //Checks to see if the ray hits any object in the world
        if (Physics.Raycast(ray, out hit))
        {
            //Find the direction the player should look towards
            Vector3 lookDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
            //Create a rotation from the player's forward to the look direction
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            //Set the rotation to be the new rotation found
            _rigidbody.MoveRotation(rotation);
        }
        else
        {
            //Look towards velocity
            transform.forward = Vector3.Lerp(transform.forward, _velocity.normalized, 0.1f);
        }
    }
}
