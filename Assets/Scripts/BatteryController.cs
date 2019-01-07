using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{

    public Transform BatteryFilling;

    private int m_battery = 3;

    // Start is called before the first frame update
    void Start()
    {
        m_battery = 3;
        UpdateBattery();
        InvokeRepeating("IncreaseBattery", 5.0f, 5.0f);
    }

    // Update is called once per frame
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

    private bool DecreaseBattery(int bat)
    {
        int newBat = m_battery - bat;
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
