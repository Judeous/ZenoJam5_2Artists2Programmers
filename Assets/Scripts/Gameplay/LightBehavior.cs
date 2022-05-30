using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    private Light _light;
    private bool _disabling;
    public bool Disabling
    {
        set { _disabling = value; }
    }

    public float Intensity
    {
        get { return _light.intensity; }
        set { _light.intensity = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_disabling)
        {
            _light.intensity -= 0.1f;
        }
    }
}
