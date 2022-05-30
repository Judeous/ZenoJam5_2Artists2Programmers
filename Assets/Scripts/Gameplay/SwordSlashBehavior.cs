using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwordSlashBehavior : MonoBehaviour
{
    public GameObject Owner;
    private float _duration = 0.75f;
    private int _hitsLeft;
    private Rigidbody _rigidbody;
    private float _rotation;

    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    public int Pierce
    {
        get { return _hitsLeft; }
        set { _hitsLeft = value; }
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

        //Rotate
        _rotation += 4;
        transform.rotation = Quaternion.Euler(0, _rotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Make sure what was hit was not the owner
        if (other.gameObject == Owner)
            return;

        //Check to see if what got hit has a HealthBehavior
        HealthBehavior otherHealth = other.GetComponent<HealthBehavior>();
        //If it does, deal damage to it
        if (otherHealth)
        {
            otherHealth.TakeDamage();

            //Decrement the Pierce
            _hitsLeft--;
            //If the pierce has reached zero, destroy itself
            if (_hitsLeft < 0)
                Destroy(gameObject);

            //Check to see if other is a bug
            //If it is then make it flee
        }

        ////If the object has a HealthBehavior
        //if (other.TryGetComponent(out HealthBehavior otherHealthBehavior))
        //    //If the object this collided with is not the owner, deal damage to it
        //    if (other != Owner)
        //        otherHealthBehavior.TakeDamage(_damage);
    }
}
