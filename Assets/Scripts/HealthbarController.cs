using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarController : MonoBehaviour {

    public Transform HealthFilling;

	public void UpdateHealthBar(float healthRatio)
    {
        HealthFilling.localScale = new Vector3(healthRatio * 10, 1, 1);
    }
}
