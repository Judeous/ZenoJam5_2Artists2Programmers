using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Tooltip("The maximum range the player can be at before the this spawner activates")]
    public float RangeToStart = 5;
    private bool _active = false;
    private bool _manuallyDeactivated = false;

    public bool Active
    {
        get { return _active; }
        set { _manuallyDeactivated = value; }
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
    [SerializeField] private float _waveDuration;
    private float _timeSinceWaveStart = 0;
    [Tooltip("The length of the pause between waves")]
    [SerializeField] private float _delayBetweenWaves;
    private float _timeSinceWaveEnd = 0;

    //Wave variables
    [Tooltip("The maximum delay between two enemy spawns")]
    [SerializeField] private float _minDelayBetweenSpawns;
    [Tooltip("The minimum delay between two enemy spawns")]
    [SerializeField] private float _maxDelayBetweenSpawns;
    private float _timeUntilNextSpawn;
    private float _timeSinceSpawn = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        _spawnerBehavior = _spawner.GetComponent<EnemySpawnerBehavior>();
        _previousSpawnPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //If the spawner has been activated and there are still more to spawn
        if (_active && _totalToSpawn > 0)
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
        //If the spawner has not been activated and has not been manually deactivated
        else if (!_manuallyDeactivated)
        {
            //Debug.Log("Spawner Not Active");
            //If the distance to the player is lower than the activation range
            if ((_target.transform.position - transform.position).magnitude < RangeToStart)
            {
                //Debug.Log("Spawner Activated");
                //Activate the spawner
                _active = true;
            }
        }
    }

    private void InWave()
    {
        _timeSinceSpawn += Time.deltaTime;

        if (_timeSinceSpawn > _timeUntilNextSpawn)
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

            //Decrement total count, reset the counter, and get a random delay for the next spawn
            _totalToSpawn--;
            _timeSinceSpawn = 0;
            _timeUntilNextSpawn = Random.Range(_minDelayBetweenSpawns, _maxDelayBetweenSpawns);
        }

        //Increment counters and swap to being out of a wave if the wave duration is up
        _timeSinceWaveStart += Time.deltaTime;
        if (_timeSinceWaveStart > _waveDuration)
        {
            _inWave = false;
            _timeSinceWaveEnd = 0;
        }
    }

    private void OutOfWave()
    {
        //Increment counters and swap to being in a wave if the wave duration is up
        _timeSinceWaveEnd += Time.deltaTime;
        if (_timeSinceWaveEnd > _delayBetweenWaves)
        {
            _inWave = true;
            _timeSinceWaveStart = 0;
        }
    }


}
