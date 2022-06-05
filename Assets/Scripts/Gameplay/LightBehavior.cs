using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    private Light _light;
    private float _inWaveIntensity = 0.9f;
    private float _outWaveIntensity = 0.65f;
    private float _idleIntensity = 0.5f;
    private bool _disabling;
    private bool _inWave = false;
    private bool _activated = false;

    public bool Active
    {
        set { _activated = true; }
    }

    public bool InWave
    {
        set { _inWave = value; }
    }

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
        //If the light has been activated
        if (_activated)
        {
            //If in a wave and the intensity is less than the inWave intensity
            if (_inWave && _light.intensity < _inWaveIntensity)
            {
                //Raise the light intensity, and clamp it to the inWave intensity
                _light.intensity += 0.05f;
                if (_light.intensity > _inWaveIntensity)
                    _light.intensity = _inWaveIntensity;
            }
            //If not in a wave and the intensity is greater than the outWaveIntensity
            else if (_light.intensity > _outWaveIntensity)
            {
                //Lower the light intensity, and clamp it to the outWave intensity
                _light.intensity -= 0.05f;
                if (_light.intensity < _outWaveIntensity)
                    _light.intensity = _outWaveIntensity;
            }
        }
        //If the light is being disabled
        else if (_disabling && _light.intensity > 0)
        {
            //Lower the intensity, and clamp it to zero
            _light.intensity -= 0.5f;
            if (_light.intensity < 0)
                _light.intensity = 0;
        }
        else
        {
            _light.intensity = _idleIntensity;
        }
    }
}
