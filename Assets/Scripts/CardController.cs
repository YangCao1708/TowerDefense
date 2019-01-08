using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class CardController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,
    IEndDragHandler
{

    public RectTransform Canvas;
    public GameObject ItemPrefab;
    public GameObject WeaponPrefab;
    private RectTransform m_movingTransform;

    private int m_cost;
    private bool m_canDrag;

    void Start()
    {
        m_cost = WeaponPrefab.GetComponent<WeaponController>().Cost;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;
        Vector2 mouseUguiPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)
        {
            m_canDrag = BatteryController.Battery.EnoughBattery(m_cost);
            if (!m_canDrag)
            {
                return;
            }
            m_movingTransform = Instantiate<GameObject>(ItemPrefab, Canvas.transform).GetComponent<RectTransform>();
            m_movingTransform.anchoredPosition = mouseUguiPos;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDrag = eventData.position;
        Vector2 uguiPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);

        if (isRect && m_canDrag)
        {
            m_movingTransform.anchoredPosition = uguiPos;
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_movingTransform != null)
        {
            Destroy(m_movingTransform.gameObject);
            m_movingTransform = null;
            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            int rowIndex = GetRow(mousePosWorld.y);
            int colIndex = GetCol(mousePosWorld.x);
            if (rowIndex>=0 && colIndex >= 0)
            {
                BatteryController.Battery.DecreaseBattery(m_cost);
                GameManager.Instance.CreateWeapon(WeaponPrefab, rowIndex, colIndex);
            }
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_movingTransform != null)
        {
            Destroy(m_movingTransform.gameObject);
            m_movingTransform = null;
        }
    }


    private int GetCol(float xCoor)
    {
        if (xCoor < -1.3 && xCoor >= -1.9)
        {
            return 0;
        } else if (xCoor < -0.6 && xCoor >= -1.3)
        {
            return 1;
        } else if (xCoor < 0 && xCoor >= -0.6)
        {
            return 2;
        } else if (xCoor < 0.6 && xCoor >= 0)
        {
            return 3;
        } else if (xCoor < 1.3 && xCoor >= 0.6)
        {
            return 4;
        } else if (xCoor < 1.9 && xCoor >= 1.3)
        {
            return 5;
        } else if (xCoor < 2.6 && xCoor >= 1.9)
        {
            return 6;
        } else if (xCoor < 3.2 && xCoor >= 2.6)
        {
            return 7;
        }
        return -1;
    }

    private int GetRow(float yCoor)
    {
        if (yCoor < -1.1 && yCoor >= -1.8)
        {
            return 4;
        } else if (yCoor < -0.4 && yCoor >= -1.1)
        {
            return 3;
        } else if (yCoor < 0.4 && yCoor >= -0.4)
        {
            return 2;
        } else if (yCoor < 1.1 && yCoor >= 0.4)
        {
            return 1;
        } else if (yCoor < 1.8 && yCoor >= 1.1)
        {
            return 0;
        }
        return -1;
    }

}
