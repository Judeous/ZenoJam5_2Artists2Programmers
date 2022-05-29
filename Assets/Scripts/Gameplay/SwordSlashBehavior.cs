using UnityEngine;

public class SwordSlashBehavior : MonoBehaviour
{
    public GameObject Owner;
    private float _duration = 0.75f;
    private int _damage;
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public float Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Decrement the time left
        _duration -= Time.deltaTime;

        //If the duration reached zero, destroy itself
        if (_duration < 0) { Destroy(gameObject); }

        //Face the direction it's moving in
        transform.forward = _rigidbody.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the object has a HealthBehavior
        HealthBehavior otherHealthBehavior = other.GetComponent<HealthBehavior>();
        if (otherHealthBehavior)
            //If the object this collided with is not the owner, deal damage to it
            if (other != Owner)
                other.GetComponent<HealthBehavior>().TakeDamage(_damage);
    }
}
