using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehavior : MonoBehaviour
{
    /// <summary>
    /// The GameObject to follow, presumably the player
    /// </summary>
    [SerializeField] private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the target position
        Vector3 targetPosition = new Vector3(_player.transform.position.x,
            _player.transform.position.y + 11,
            _player.transform.position.z - 6);

        //Lerp to target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);

        //Move to the target position
        //transform.position = targetPosition;
    }
}
