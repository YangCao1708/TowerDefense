using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Battery;
    public Transform BatteryFilling;

    private int m_battery;


    private void Awake()
    {
        if (Battery != null)
        {
            Destroy(Battery);
        }
        Battery = this;
    }

    void Start()
    {
        m_battery = 3;
        UpdateBattery();
        InvokeRepeating("IncreaseBattery", 4.0f, 4.0f);
    }

    void Update()
    {
        UpdateBattery();
    }

    public void UpdateBattery()
    {
        BatteryFilling.localScale = new Vector3(m_battery, 1, 1);
    }

    private void IncreaseBattery()
    {
        if (m_battery < Constants.MaxBat)
        {
            m_battery++;
        }
    }

    public void DecreaseBattery(int bat)
    {
        m_battery = m_battery - bat;
    }

    public bool EnoughBattery(int cost)
    {
        return m_battery - cost >= 0;
    }
}
