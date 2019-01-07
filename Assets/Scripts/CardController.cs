using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class CardController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,
    IEndDragHandler
{
    //public Camera Cam;
    public RectTransform Canvas;          //得到canvas的ugui坐标
    public GameObject ItemPrefab;
    public GameObject WeaponPrefab;
    private RectTransform m_movingTransform;
    private RectTransform m_imgRect;        //得到图片的ugui坐标
    Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值

    // Use this for initialization
    void Start()
    {
        m_imgRect = GetComponent<RectTransform>();

    }

    //当鼠标按下时调用 接口对应  IPointerDownHandler
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
        //out mouseUguiPos：返回转换后的ugui坐标
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)   //如果在
        {
            m_movingTransform = Instantiate<GameObject>(ItemPrefab, Canvas.transform).GetComponent<RectTransform>();
            m_movingTransform.anchoredPosition = mouseUguiPos;
            //计算图片中心和鼠标点的差值
            offset = m_imgRect.anchoredPosition - mouseUguiPos;
        }
    }

    //当鼠标拖动时调用   对应接口 IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
        Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
        //和上面类似
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);

        if (isRect)
        {
            m_movingTransform.anchoredPosition = uguiPos;
            //设置图片的ugui坐标与鼠标的ugui坐标保持不变
            //m_imgRect.anchoredPosition = offset + uguiPos;
        }
    }

    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_movingTransform != null)
        {
            //TODO
            Destroy(m_movingTransform.gameObject);
            m_movingTransform = null;
            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            int rowIndex = GetRow(mousePosWorld.y);
            int colIndex = GetCol(mousePosWorld.x);
            GameManager.Instance.CreateWeapon(WeaponPrefab, rowIndex, colIndex);
        }
    }

    //当鼠标结束拖动时调用   对应接口  IEndDragHandler
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
        } else if (xCoor < -0.6)
        {
            return 1;
        } else if (xCoor < 0)
        {
            return 2;
        } else if (xCoor < 0.6)
        {
            return 3;
        } else if (xCoor < 1.3)
        {
            return 4;
        } else if (xCoor < 1.9)
        {
            return 5;
        } else if (xCoor < 2.6)
        {
            return 6;
        } else if (xCoor < 3.2)
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
        } else if (yCoor < -0.4)
        {
            return 3;
        } else if (yCoor < 0.4)
        {
            return 2;
        } else if (yCoor < 1.1)
        {
            return 1;
        } else if (yCoor < 1.8)
        {
            return 0;
        }
        return -1;
    }

}
