using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject TestWeapon;
    public GameObject TestMonster;
    public LocateHelper Helper;
    public Transform WeaponParent;
    public Transform MonsterParent;
    public GameObject HealthBarPrefab;

    private Dictionary<int, List<WeaponController>> m_weaponDict;
    private List<WeaponController> m_weaponList;
    private List<MonsterController> m_monsterList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        m_weaponDict = new Dictionary<int, List<WeaponController>>();
        //m_weaponList = new List<WeaponController>();
        m_monsterList = new List<MonsterController>();

        //StartCoroutine(TestCreateWeapon());
        StartCoroutine(TestCreateMonsters());
	}

    public void CreateWeapon(GameObject prefab, int rowNum, int colNum)
    {
        if (rowNum < 0 || colNum < 0)
        {
            return;
        }

        if (!m_weaponDict.ContainsKey(rowNum))
        {
            List<WeaponController> newRow = new List<WeaponController>();
            for (int i = 0; i < Constants.ColumnNum; i++)
            {
                newRow.Add(null);
            }
            m_weaponDict.Add(rowNum, newRow);
        }
        List<WeaponController> row = m_weaponDict[rowNum];

        if (row[colNum]==null)
        {
            int index = colNum * Constants.RowNum + rowNum;
            Vector3 pos = Helper.GetPositionByIndex(index);
            GameObject weapon = Instantiate<GameObject>(prefab, WeaponParent);
            weapon.transform.localPosition = pos;
            weapon.GetComponent<WeaponController>().SetIndex(rowNum, colNum);
            row[colNum] = weapon.GetComponent<WeaponController>();
            m_weaponDict[rowNum] = row;
        }

        //m_weaponList.Add(weapon.GetComponent<WeaponController>());
    }

    IEnumerator TestCreateWeapon()
    {
        yield return new WaitForSeconds(2);
        CreateWeapon(TestWeapon, 0, 7);
        CreateWeapon(TestWeapon, 1, 7);
        CreateWeapon(TestWeapon, 2, 7);
        CreateWeapon(TestWeapon, 3, 7);
        CreateWeapon(TestWeapon, 4, 7);
    }

    // TODO
    void CreateMonsters()
    {
        int index = Random.Range(0, Constants.RowNum);
        Vector3 pos = Helper.GetMonsterPositionByIndex(index);
        GameObject monster = Instantiate<GameObject>(TestMonster, MonsterParent);
        monster.transform.localPosition = pos;
        m_monsterList.Add(monster.GetComponent<MonsterController>());
    }

    IEnumerator TestCreateMonsters()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            CreateMonsters();
            yield return new WaitForSeconds(5);
        }
    }

    // TODO
    public void UpdateShootingStatus(int rowIndex, bool shouldShoot)
    {
        if (!m_weaponDict.ContainsKey(rowIndex)) return;

        Debug.Log("Got here! " + rowIndex + " " + shouldShoot);
        List<WeaponController> row = m_weaponDict[rowIndex];
        for (int i = 0; i < Constants.ColumnNum; i++)
        {
            if (row[i] != null)
            {
                row[i].UpdateShootStatus(shouldShoot);
            }
        }
        //foreach(WeaponController weapon in m_weaponDict[rowIndex])
        //{
        //    weapon.UpdateShootStatus(shouldShoot);
        //}
    }

    public void RemoveWeapon(int r, int c)
    {
        List<WeaponController> row = m_weaponDict[r];
        row[c] = null;
    }

}
