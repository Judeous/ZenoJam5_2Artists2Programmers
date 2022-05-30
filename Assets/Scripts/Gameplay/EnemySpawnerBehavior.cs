using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(GameObject spawn, Vector3 location, GameObject target, bool flee)
    {
        Instantiate(spawn, location, new Quaternion());

        EnemyJuneBugMovementBehavior behavior = spawn.GetComponent<EnemyJuneBugMovementBehavior>();
        if (behavior)
        {
            behavior.Target = target;
            behavior.Fleeing = flee;
        }
    }
}
