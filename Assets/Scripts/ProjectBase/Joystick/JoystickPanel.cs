using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum E_JoystickType
{
    //固定摇杆
    Normal,
    //可改变位置遥感
    CanChangePos,
    //可移动遥感
    CanMove,
}
public class JoystickPanel : BasePanel 
{
    //当前的遥感类型
    public E_JoystickType type = E_JoystickType.Normal;
    //用来作为事件的触发背景
    private Image imgTouchRect;
    //拖动后面的背景
    private Image imgBk;
    //拖动的控件
    private Image imgControl;
    //背景的半径
    public int maxLen = 280;
    private void Start()
    {
        imgTouchRect = GetControl<Image>("imgTouchRect");
        imgBk = GetControl<Image>("imgBk");
        imgControl = GetControl<Image>("imgControl");
        //添加unity 事件监听方法
        UIManager.AddCustomEventListener(imgTouchRect, EventTriggerType.PointerDown, PointerDown);
        UIManager.AddCustomEventListener(imgTouchRect, EventTriggerType.PointerUp, PointerUp);
        UIManager.AddCustomEventListener(imgTouchRect, EventTriggerType.Drag, Drag);
        if (type != E_JoystickType.Normal)
        {
            imgBk.gameObject.SetActive(false);
        }
    }
    private void PointerDown(BaseEventData data)
    {
        imgBk.gameObject.SetActive(true);
        //这里是将bg的位置更新到点击的位置
        if (type != E_JoystickType.Normal)
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                imgTouchRect.rectTransform,//这个要写要改变物体的父对象
                (data as PointerEventData).position,//这是当前屏幕的鼠标位置
                (data as PointerEventData).pressEventCamera,//这是UI的摄像机位置
                out localPos);//这里会返回一个转换来的相对坐标
                              //实时更新位置
            imgBk.transform.localPosition = localPos;
        }
    }
    private void PointerUp(BaseEventData data)
    {
        if (type != E_JoystickType.Normal)
        {
            imgBk.gameObject.SetActive(false);
        }
        //复位位置   
        imgControl.transform.localPosition = Vector3.zero;
        EventCenter.GetInstance().EventTrigger<Vector2>("Jotstick", Vector2.zero);
    }
    private void Drag(BaseEventData data)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgBk.rectTransform,//这个要写要改变物体的父对象
            (data as PointerEventData).position,//这是当前屏幕的鼠标位置
            (data as PointerEventData).pressEventCamera,//这是UI的摄像机位置
            out localPos);//这里会返回一个转换来的相对坐标
        //实时更新按钮的位置
        imgControl.transform.localPosition = localPos;
        //限制范围
        if (localPos.magnitude > maxLen)
        {
            if (type == E_JoystickType.CanMove)
            {
                //如果是按钮随着拖动会动，那么原理就是将背景也跟着imgControl相同方向移动(localPos.magnitude - maxLen)长度，那么他们的相对位置仍然看着一样，但是位置都变了
                imgBk.transform.localPosition += (Vector3)localPos.normalized * (localPos.magnitude - maxLen);
            }
            imgControl.transform.localPosition = localPos.normalized * maxLen;
        }
        //分发实时遥感方向
        EventCenter.GetInstance().EventTrigger<Vector2>("Joystick", localPos.normalized);
    }
}

