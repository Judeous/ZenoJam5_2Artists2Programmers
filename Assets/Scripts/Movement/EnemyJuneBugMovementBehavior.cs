using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyJuneBugMovementBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;

    [SerializeField] private bool _fleeing = false;
    [SerializeField] private float _maxTimeFleeing = 3;
    private float _currentTimeSinceFleeing = 0;

    public bool Fleeing { get { return _fleeing; } set { _fleeing = value; } }
    public GameObject Target { get { return _target; } set { _target = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the direction to the target
        Vector3 toTarget = _target.transform.position - transform.position;

        //Create a ray that starts at a screen point
        RaycastHit hit;
        Ray ray = new Ray(transform.position, toTarget);
        Debug.DrawRay(transform.position, toTarget);
        //Checks to see if the ray hits something that is not a Maze and not a Bug
        if (Physics.Raycast(ray, out hit) && !hit.collider.CompareTag("Maze") && !hit.collider.CompareTag("Bug"))
        {
            //If not fleeing, add velocity towards the target
            //Otherwise, add velocity away from the target
            Vector3 force = _fleeing ? toTarget * -_speed : toTarget * _speed;
            force.y += 1.1f;
            //Normalize the movement so they don't move at light speed
            force *= Time.deltaTime;
            //Add the force to the rigidbody
            _rigidbody.AddForce(force, ForceMode.Acceleration);

            if (_fleeing)
            {
                //Increment the timer
                _currentTimeSinceFleeing += Time.deltaTime;
                //If the time fleeing has reached the max
                if (_currentTimeSinceFleeing > _maxTimeFleeing)
                {
                    //Set fleeing to false and reset the timer
                    _fleeing = false;
                    _currentTimeSinceFleeing = 0;
                }
            }
        }


    }
}
