using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightFlicker : MonoBehaviour
{
    private Light2D _light;

    [Header("Intensity")]
    [Tooltip("Enables Intensity Flicker")] public bool flickIntensity;
    private float _baseIntensity;
    [Tooltip("Intensity Range: new intensity is picked in default +/- range")] public float intensityRange;
    [Tooltip("Time range lower limit: flicking time is picked in (min, max) seconds")] public float intensityTimeMin = 0.05f;
    [Tooltip("Time range upper limit: flicking time is picked in (min, max) seconds")] public float intensityTimeMax = 0.5f;

    [Header("Position")]
    [Tooltip("Enables Position Flicker")] public bool flickPosition;
    private Vector3 _basePosition;
    [Tooltip("Movement Range Radius: new positon is picked in a circular sector")] public float positionRadius;
    [Tooltip("Movement Angular Range: new positon is picked in a circular sector")] public float angle;
    [Tooltip("Time range lower limit: flicking time is picked in (min, max) seconds")] public float positionTimeMin = 0.05f;
    [Tooltip("Time range upper limit: flicking time is picked in (min, max) seconds")] public float positionTimeMax = 0.5f;

    [Header("Color")]
    [Tooltip("Enables Position Flicker")] public bool flickColor;
    private Color _baseColor;
    private Color _color;
    private Vector3 _colorVector = Vector3.zero;
    [Tooltip("Color Range Radius: new color is picked in a sphere around the default color. USE WITH CAUTION!")] [Range(0f,1f)]public float colorRadius;
    [Tooltip("Time range lower limit: flicking time is picked in (min, max) seconds")] public float colorTimeMin = 0.05f;
    [Tooltip("Time range upper limit: flicking time is picked in (min, max) seconds")] public float colorTimeMax = 0.5f;

    void Start()
    {
        _light = GetComponent<Light2D>();

        _baseIntensity = _light.intensity;
        StartCoroutine(FlickIntensity());

        _basePosition = transform.position;
        StartCoroutine(FlickPosition());

        _baseColor = _light.color;
        _color = Color.black;
        ColorToVector3(_baseColor);
        StartCoroutine(FlickColor());
    }

    #region Intensity Flicker
    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickIntensity)
            {
                t0 = Time.time;
                float r = Random.Range(_baseIntensity - intensityRange, _baseIntensity + intensityRange);
                _light.intensity = r;
                t = Random.Range(intensityTimeMin, intensityTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
    #endregion
    
    #region Position Flicker
    private IEnumerator FlickPosition()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        Vector3 shift = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickPosition)
            {
                t0 = Time.time;
                float r = Random.Range(0f, positionRadius);
                float theta = Random.Range(0.5f * (Mathf.PI - angle * Mathf.Deg2Rad), 0.5f * (Mathf.PI + angle * Mathf.Deg2Rad));
                shift.x = r * Mathf.Cos(theta);
                shift.y = r * Mathf.Sin(theta);
                transform.position = _basePosition + shift;
                t = Random.Range(positionTimeMin, positionTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
    #endregion

    #region Color Flicker
    void Vector3ToColor(Vector3 v)
    {
        _color.r = v.x;
        _color.g = v.y;
        _color.b = v.z;
    }

    void ColorToVector3(Color c)
    {
        _colorVector.x = c.r;
        _colorVector.y = c.g;
        _colorVector.z = c.b;
    }

    private IEnumerator FlickColor()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickColor)
            {
                t0 = Time.time;
                Vector3ToColor(Random.insideUnitSphere * colorRadius + _colorVector);
                _light.color = _color;
                t = Random.Range(colorTimeMin, colorTimeMax);
                yield return wait;
                _light.color = _baseColor;
            }
            else yield return null;
        }
    }
    #endregion
}