using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Tooltip("The maximum range the player can be at before the this spawner activates")]
    public float RangeToStart = 5;
    private bool _active = false;
    private bool _complete = false;

    [SerializeField] private Light _light;
    private LightBehavior _lightBehavior;

    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    //Spawn variables
    [Tooltip("The GameObject to spawn")]
    [SerializeField] private GameObject _enemy;
    [Tooltip("The GameObject for the enemies to target")]
    [SerializeField] private GameObject _target;
    [Tooltip("The things that will spawn the enemies")]
    [SerializeField] private GameObject _spawner;
    private EnemySpawnerBehavior _spawnerBehavior;
    [Tooltip("The total number of enemies to spawn before stopping all spawning")]
    [SerializeField] private int _totalToSpawn;

    //Position variables
    [Tooltip("Vectors for positions to spawn the enemies")]
    [SerializeField] private Transform[] _spawnPositions;
    private Transform _previousSpawnPosition;

    //Wave occurance variables
    private bool _inWave = true;
    [Tooltip("The duration of a wave before pausing spawning")]
    private float _waveDuration;
    [Tooltip("The length of the pause between waves")]
    private float _calmDuration;

    //Wave variables
    [Tooltip("The maximum delay between two enemy spawns")]
    private float _minDelayBetweenSpawns;
    [Tooltip("The minimum delay between two enemy spawns")]
    private float _maxDelayBetweenSpawns;
    private float _timeUntilNextSpawn;

    
    // Start is called before the first frame update
    void Start()
    {
        _spawnerBehavior = _spawner.GetComponent<EnemySpawnerBehavior>();
        _lightBehavior = _light.GetComponent<LightBehavior>();

        _previousSpawnPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //If there are still more to spawn and the spawner has been activated
        if (!_complete && _active)
        {
            //If in a wave
            if (_inWave)
            {
                //Debug.Log("In Wave");
                //Do wave stuff
                InWave();
            }
            //Otherwise
            else
            {
                //Debug.Log("Out of Wave");
                //Do out of wave stuff
                OutOfWave();
            }
        }
        //If the spawner has not been activated
        else if (!_active)
        {
            //If the distance to the player is lower than the activation range
            if ((_target.transform.position - transform.position).magnitude < RangeToStart)
            {
                //Debug.Log("Spawner Activated");
                //Activate the spawner
                _active = true;
                _lightBehavior.Active = true;
            }
        }
        //If the spawner is finished
        else
        {
            _lightBehavior.Active = false;
            _lightBehavior.Disabling = true;
            _active = false;
        }

        //_light.intensity = _lightIntensity;
    }

    private void InWave()
    {
        _timeUntilNextSpawn -= Time.deltaTime;

        if (_timeUntilNextSpawn <= 0)
        {
            //As long if there are more than one possible spawn location
            //Calculate a random location for an enemy to spawn that is not the last one that was spawned at
            Transform locationToSpawn;
            do {
                locationToSpawn = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
            } while (_spawnPositions.Length > 1 && locationToSpawn.position == _previousSpawnPosition.position);
            //Set the previous spawn position to be the determined one so the next spawn isn't in the same spot
            _previousSpawnPosition.position = locationToSpawn.position;

            bool flee = Random.value > 0.5f;

            //Spawn a bug at the position
            _spawnerBehavior.SpawnEnemy(_enemy, locationToSpawn.position, _target, flee);

            //Decrement total count
            _totalToSpawn--;
            //If the spawners has spawned the total to spawn
            if (_totalToSpawn <= 0)
            {
                //Enable complete to deactivate the spawner
                _complete = true;
            }
            else
            {
                //Reset the timer by getting a random delay for the next spawn
                _timeUntilNextSpawn = Random.Range(_minDelayBetweenSpawns, _maxDelayBetweenSpawns);
            }
        }

        //Increase Light intensity
        _lightBehavior.InWave = true;
        //Decrement timer and swap to being out of a wave if the wave duration is up
        _waveDuration -= Time.deltaTime;
        if (_waveDuration <= 0)
        {
            _inWave = false;
            //Reset timer for next wave duration
            _waveDuration = Random.Range(.25f, 1.25f);
        }
    }

    private void OutOfWave()
    {
        //Lower Light intensity
        _lightBehavior.InWave = false;
        //Decrement timer and swap to being in a wave if the time is up
        _calmDuration -= Time.deltaTime;
        if (_calmDuration <= 0)
        {
            _inWave = true;
            //Reset timer for the next calm duration
            _calmDuration = Random.Range(10, 18);
        }
    }


}
