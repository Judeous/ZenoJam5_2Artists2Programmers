using UnityEngine;

public class SlashSpawnerBehavior : MonoBehaviour
{
    /// <summary>
    /// The GameObject that will be spawned by the Fire function. Needs to have SwordSlashBehavior attached
    /// </summary>
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _duration = 0.25f;
    [SerializeField] private float _speed = 0.4f;

    [SerializeField] private float _cooldown = 0.25f;
    [SerializeField] private float _timeSinceFire = 10;
    /// <summary>
    /// The GameObject that will not be hit by the slash
    /// </summary>
    [SerializeField] private GameObject _owner;
    /// <summary>
    /// The Camera to check a raycast hit from to determine the direction to send the slash in
    /// </summary>
    private Camera _camera;
    public Camera Camera
    {
        set { _camera = value; }
    }

    // Update is called once per frame
    public void Update()
    {
        _timeSinceFire += Time.deltaTime;
    }

    public void TryFire()
    {
        if (_timeSinceFire > _cooldown)
        {
            Fire();
        }
    }

    public void Fire()
    {
        //Get a raycasthit from the camera based on the mouse position
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 direction;
        Vector3 force;
        //If the cursor hit something
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = ray.GetPoint(13);
            //Get a vector from the current position to the hit position
            direction = new Vector3(point.x - transform.position.x, 0, point.z - transform.position.z).normalized;
        }
        else
        {
            //Otherwise, make the direction forwards
            direction = transform.forward;
        }
        //Calculate the force to give the slash
        force = direction * _speed;

        //Create an instance of a Slash
        GameObject slash = Instantiate(_projectile, transform.position, transform.rotation);
        
        //Get the slashbehavior component
        if (slash.TryGetComponent<SwordSlashBehavior>(out var slashBehavior))
        {
            //Get the rigidbody component
            if (slash.TryGetComponent(out Rigidbody slashRigidbody))
            {
                //Add the force and set the owner and duration
                slashRigidbody.AddForce(force, ForceMode.Impulse);
                slashBehavior.Owner = _owner;
                slashBehavior.Duration = _duration;
                _timeSinceFire = 0;
            }

        }
    }
}
