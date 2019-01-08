using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Battery;
    public Transform BatteryFilling;

    private int m_battery = 3;


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
        InvokeRepeating("IncreaseBattery", 5.0f, 5.0f);
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

    public bool DecreaseBattery(int bat)
    {
        int newBat = m_battery - bat;
        Debug.Log(newBat);
        if (newBat < 0)
        {
            return false;
        } else
        {
            m_battery = newBat;
            return true;
        }
    }
}
