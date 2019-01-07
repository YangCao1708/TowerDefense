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
        m_weaponList = new List<WeaponController>();
        m_monsterList = new List<MonsterController>();

        //StartCoroutine(TestCreateWeapon());
        StartCoroutine(TestCreateMonsters());
	}

    // TODO 
    public void CreateWeapon(GameObject prefab, int rowNum, int colNum)
    {
        if (rowNum < 0 || colNum < 0)
        {
            return;
        }
        int index = colNum * Constants.RowNum + rowNum;
        Vector3 pos = Helper.GetPositionByIndex(index);
        GameObject weapon = Instantiate<GameObject>(prefab, WeaponParent);
        weapon.transform.localPosition = pos;
        //for (int i = 0; i < Constants.ColumnNum; i++)
        //{
            //List<WeaponController> newRow = new List<WeaponController>();
            //for (int j = 0; j < Constants.RowNum; j++)
            //{
                //newRow.Add(null);
            //}
            //newRow[colNum] = 
            //m_weaponList[rowNum] = newRow;
        //}

        if (!m_weaponDict.ContainsKey(rowNum))
        {
            List<WeaponController> newRow = new List<WeaponController>();
            
            newRow.Add(weapon.GetComponent<WeaponController>());
            m_weaponDict.Add(rowNum, newRow);
        } else
        {
            List<WeaponController> row = m_weaponDict[rowNum];
            row.Add(weapon.GetComponent<WeaponController>());
        }

        m_weaponList.Add(weapon.GetComponent<WeaponController>());
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

    public void UpdateShootingStatus(int rowIndex, bool shouldShoot)
    {
        if (!m_weaponDict.ContainsKey(rowIndex)) return;
        
        foreach(WeaponController weapon in m_weaponDict[rowIndex])
        {
            weapon.UpdateShootStatus(shouldShoot);
        }
    }

}
