using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HUDSystem : MonoBehaviour
{
    [SerializeField] private GameObject healthPointPrefab;
    [SerializeField] private GameObject crashPointPrefab;
    
    [SerializeField] private GameObject[] healthPoints;
    [SerializeField] private GameObject[] crashPoints;

    [SerializeField] private Transform healthPointsParent;
    [SerializeField] private Transform crashPointsParent;

    [SerializeField] private Sprite healthPointActive;
    [SerializeField] private Sprite healthPointDisabled;
    [SerializeField] private Sprite crashPointActive;
    [SerializeField] private Sprite crashPointDisabled;

    private Image[] _healthPointsImageComponents;
    private Image[] _crashPointsImageComponents;

    public static HUDSystem Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void AllocateHealthPoints(int healthPointsQuantity)
    {
        if (healthPoints.Length > 0)
        {
            for (int o = 0; o < healthPoints.Length; o++)
            {
                Destroy(healthPoints[o].gameObject);
                Destroy(_healthPointsImageComponents[o].gameObject);
            }
        }
        
        healthPoints = new GameObject[healthPointsQuantity];
        _healthPointsImageComponents = new Image[healthPointsQuantity];
        
        for (int i = 0; i < healthPointsQuantity; i++)
        {
            healthPoints[i] = Instantiate(healthPointPrefab, healthPointsParent);
            _healthPointsImageComponents[i] = healthPoints[i].GetComponent<Image>();
            _healthPointsImageComponents[i].sprite = healthPointActive;
        }
    }

    public void AllocateCrashPoints(int crashPointsQuantity)
    {
        if (crashPoints.Length > 0)
        {
            for (int o = 0; o < crashPoints.Length; o++)
            {
                Destroy(crashPoints[o].gameObject);
                Destroy(_crashPointsImageComponents[o].gameObject);
            }
        }
        
        crashPoints = new GameObject[crashPointsQuantity];
        _crashPointsImageComponents = new Image[crashPointsQuantity];
        
        for (int i = 0; i < crashPointsQuantity; i++)
        {
            crashPoints[i] = Instantiate(crashPointPrefab, crashPointsParent);
            _crashPointsImageComponents[i] = crashPoints[i].GetComponent<Image>();
            _crashPointsImageComponents[i].sprite = crashPointActive;
        }
    }

    public void UpdateHealthPoints(int currentHealthPoints)
    {
        for (int i = healthPoints.Length-1; i >= currentHealthPoints; i--)
        {
            _healthPointsImageComponents[i].sprite = healthPointDisabled;
        }
    }

    public void UpdateCrashPoints(int currentCrashPoints)
    {
        for (int i = crashPoints.Length-1; i >= currentCrashPoints; i--)
        {
            _crashPointsImageComponents[i].sprite = crashPointDisabled;
        }
    }
}
