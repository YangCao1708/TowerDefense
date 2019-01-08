using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject BallPrefab;
    public GameObject ControllerPrefab;
    public GameObject FriesPrefab;
    public GameObject PhonePrefab;
    public GameObject PillowPrefab;

    public LocateHelper Helper;
    public Transform WeaponParent;
    public Transform MonsterParent;
    public GameObject HealthBarPrefab;

    private Dictionary<int, List<WeaponController>> m_weaponDict;
    private List<WeaponController> m_weaponList;
    private List<GameObject> m_monsterPool;

    private bool m_checkWin;
    private int m_monsterCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    void Start () {

        m_checkWin = false;
        m_monsterCount = 0;

        m_weaponDict = new Dictionary<int, List<WeaponController>>();
        m_monsterPool = new List<GameObject>();
        m_monsterPool.Add(FriesPrefab);
        m_monsterPool.Add(PillowPrefab);

        StartCoroutine(TestCreateMonsters());
	}

    private void Update()
    {
        if (m_checkWin && m_monsterCount==0)
        {
            TriggerController.Trigger.Won();
        }
    }

    public void CreateWeapon(GameObject prefab, int rowNum, int colNum)
    {

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
    }


    void CreateMonsters()
    {
        int index = Random.Range(0, Constants.RowNum);
        Vector3 pos = Helper.GetMonsterPositionByIndex(index);
        int randIdx = Random.Range(0, m_monsterPool.Count);
        GameObject monster = Instantiate<GameObject>(m_monsterPool[randIdx], MonsterParent);
        monster.transform.localPosition = pos;
        m_monsterCount++;
    }

    IEnumerator TestCreateMonsters()
    {
        yield return new WaitForSeconds(2);
        float start_time = Time.time;
        float time_diff = 0;
        while (time_diff < 35)
        {
            CreateMonsters();
            yield return new WaitForSeconds(5);
            time_diff = Time.time - start_time;
        }
        m_monsterPool.Add(BallPrefab);
        m_monsterPool.Add(PhonePrefab);
        while (time_diff < 65)
        {
            CreateMonsters();
            yield return new WaitForSeconds(4);
            time_diff = Time.time - start_time;
        }
        m_monsterPool.Add(ControllerPrefab);
        while (time_diff < 90)
        {
            CreateMonsters();
            yield return new WaitForSeconds(3);
            time_diff = Time.time - start_time;
        }

        m_checkWin = true;
    }



    public void UpdateShootingStatus(int rowIndex, bool shouldShoot)
    {
        if (!m_weaponDict.ContainsKey(rowIndex)) return;

        List<WeaponController> row = m_weaponDict[rowIndex];
        for (int i = 0; i < Constants.ColumnNum; i++)
        {
            if (row[i] != null)
            {
                row[i].UpdateShootStatus(shouldShoot);
            }
        }
    }

    public void RemoveWeapon(int r, int c)
    {
        List<WeaponController> row = m_weaponDict[r];
        row[c] = null;
    }

    public void RemoveMonster()
    {
        m_monsterCount--;
    }

}
