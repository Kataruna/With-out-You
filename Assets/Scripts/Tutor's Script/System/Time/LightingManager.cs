using System;
using DG.Tweening;
using UnityEngine;

[ExecuteAlways, DefaultExecutionOrder(-1)]
public class LightingManager : MonoBehaviour
{
    public static LightingManager Instance;
    
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [Range(0, 24)] public float TimeOfDay;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Preset == null)
            return;
    
        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            // TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    public void UpdateTime(float scheduleTime)
    {
        bool overMidnight = TimeOfDay > scheduleTime;
        float timeToMidnight = overMidnight ? (24 - TimeOfDay): scheduleTime - TimeOfDay;
        float timeFromMidnight = overMidnight ? scheduleTime : 24 - scheduleTime;

        timeToMidnight /= 2f;
        timeFromMidnight /= 2f;
        
        if (overMidnight)
            DOTween.To(() => TimeOfDay, x => TimeOfDay = x, 24f, timeToMidnight)
                .OnComplete(() =>TimeOfDay = 0f)
                .OnComplete(() => DOTween.To(() => TimeOfDay, x => TimeOfDay = x, scheduleTime, timeFromMidnight));
        else
            DOTween.To(() => TimeOfDay, x => TimeOfDay = x, scheduleTime, timeToMidnight);
    }
    
    public void UpdateTime(float scheduleTime, int round)
    {
        DOTween.To(() => TimeOfDay, x => TimeOfDay = x, 24f, 1f)
            .OnComplete(() => TimeOfDay = 0f)
            .OnComplete(() => DOTween.To(() => TimeOfDay, x => TimeOfDay = x, 24f, 0.5f).SetLoops(round, LoopType.Restart))
            .OnComplete(() => DOTween.To(() => TimeOfDay, x => TimeOfDay = x, scheduleTime, 1f)); 
    }
    
    public void UpdateTimeRound(float scheduleTime)
    {
        DOTween.To(() => TimeOfDay, x => TimeOfDay = x, 24f, 1f)
            .OnComplete(() => TimeOfDay = 0f)
            .OnComplete(() => DOTween.To(() => TimeOfDay, x => TimeOfDay = x, scheduleTime, 1f)); 
    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}