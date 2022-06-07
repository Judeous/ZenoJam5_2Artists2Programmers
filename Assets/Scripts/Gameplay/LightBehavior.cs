using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    private Light _light;

    private float _idleIntensity = 0.4f;
    private float _inWaveIntensity = 0.9f;
    private float _outWaveIntensity = 0.5f;

    private float _fadeSpeed = 0.01f;

    private bool _disabling = false;
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
        _light.intensity = _idleIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        //If the light is not being disabled, and has been activated
        if (!_disabling && _activated)
        {
            //If in a wave and the intensity is less than the inWave intensity
            if (_inWave && _light.intensity < _inWaveIntensity)
            {
                //Raise the light intensity, and clamp it to the inWave intensity
                _light.intensity += _fadeSpeed;
                if (_light.intensity > _inWaveIntensity)
                    _light.intensity = _inWaveIntensity;
            }
            //If not in a wave and the intensity is greater than the outWaveIntensity
            else if (_light.intensity > _outWaveIntensity)
            {
                //Lower the light intensity, and clamp it to the outWave intensity
                _light.intensity -= _fadeSpeed;
                if (_light.intensity < _outWaveIntensity)
                    _light.intensity = _outWaveIntensity;
            }
        }
        //If the light is being disabled
        else if (_disabling && _light.intensity > 0)
        {
            //Lower the intensity, and clamp it to zero
            _light.intensity -= _fadeSpeed;
            if (_light.intensity < 0)
                _light.intensity = 0;
        }
    }
}
