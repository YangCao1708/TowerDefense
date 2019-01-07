using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public float MaxHealth;
    public float Damage;
    public float Speed;
    public float AttackInterval;

    protected float CurrentHealth;
    protected HealthbarController HealthBar;

    protected void Start()
    {

        CurrentHealth = MaxHealth;

        if (this is MonsterController || this is WeaponController)
        {
            GameObject go = Instantiate<GameObject>(GameManager.Instance.HealthBarPrefab, this.transform);
            HealthBar = go.GetComponent<HealthbarController>();
            go.transform.localPosition = new Vector3(0, 0.25f, 0);
        }
        
    }
}
