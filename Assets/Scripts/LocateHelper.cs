using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateHelper : MonoBehaviour {

    public Transform LocatorTopLeft;
    public Transform LocatorBtmRight;
    public Transform LocatorMonster;
    public GameObject TestObj;

    private List<Vector3> m_targetLocations;
    private List<Vector3> m_monsterTargetLocations;

    private void Start()
    {
        GenerateGrid();
        GenerateMonsterStartPositions();
    }

    void GenerateGrid() {
        m_targetLocations = new List<Vector3>();
        float xDis = (LocatorBtmRight.localPosition.x - LocatorTopLeft.localPosition.x) / (Constants.ColumnNum - 1);
        float yDis = (LocatorBtmRight.localPosition.y - LocatorTopLeft.localPosition.y) / (Constants.RowNum - 1);

        for (int i = 0; i < Constants.ColumnNum; i++)
        {
            for (int j = 0; j < Constants.RowNum ; j++)
            {
                Vector3 pos = LocatorTopLeft.localPosition + Vector3.right * xDis * i + Vector3.up * yDis * j;
                //GameObject test = Instantiate<GameObject>(TestObj, this.transform);
                //test.transform.localPosition = pos;
                m_targetLocations.Add(pos);
            }
        }
    }

    public Vector3 GetPositionByIndex(int index)
    {
        if (index < 0 || index >= m_targetLocations.Count) return Vector3.zero;
        return m_targetLocations[index];
    }

    void GenerateMonsterStartPositions()
    {
        m_monsterTargetLocations = new List<Vector3>();
        for (int i = 0; i < Constants.RowNum; i++)
        {
            Vector3 pos = new Vector3(LocatorMonster.localPosition.x, m_targetLocations[i].y, 0);
            //GameObject test = Instantiate<GameObject>(TestObj, this.transform);
            //test.transform.localPosition = pos;
            m_monsterTargetLocations.Add(pos);
        }
    }

    public Vector3 GetMonsterPositionByIndex(int index)
    {
        if (index < 0 || index >= m_monsterTargetLocations.Count) return Vector3.zero;
        return m_monsterTargetLocations[index];
    }
}
