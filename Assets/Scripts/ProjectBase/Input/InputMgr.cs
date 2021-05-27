﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ControlKey
{
    LeftKey,
    RightKey
}
/// <summary>
/// 1.Input类
/// 2.事件中心模块
/// 3.公共Mono模块的使用
/// </summary>
public class InputMgr : BaseManager<InputMgr>
{

    private bool isStart = true;
    //private float lastkeydown = 0f;
    public KeyCode LeftKey = KeyCode.LeftArrow;
    public KeyCode RightKey = KeyCode.RightArrow;
    public KeyCode KickKey = KeyCode.Space;
    public KeyCode GrabKey = KeyCode.Z;
    /// <summary>
    /// 构造函数中 添加Updata监听
    /// </summary>
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    /// <summary>
    /// 是否开启或关闭 我的输入检测
    /// </summary>
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    /// <summary>
    /// 用来检测按键抬起按下 分发事件的
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(KeyCode key)
    {
        //事件中心模块 分发按下抬起事件
        if (Input.GetKeyDown(key))
            EventCenter.GetInstance().EventTrigger("某键按下", key);
        //事件中心模块 分发按下抬起事件
        if (Input.GetKeyUp(key))
            EventCenter.GetInstance().EventTrigger("某键抬起", key);
        if (Input.GetKey(key))
            EventCenter.GetInstance().EventTrigger("某键按着", key);
    }

    private void MyUpdate()
    {

        //没有开启输入检测 就不去检测 直接return
        if (!isStart)
            return;
        CheckKeyCode(LeftKey);
        CheckKeyCode(RightKey);
        CheckKeyCode(KickKey);
        CheckKeyCode(GrabKey);
    }

    /// <summary>
    /// 改键
    /// </summary>
    /// <param name="key"></param>
    /// <param name="keyCode"></param>
    public void ChangeKey(ControlKey key, KeyCode keyCode)
    {
        switch (key)
        {
            case ControlKey.LeftKey:
                LeftKey = keyCode;
                break;
            case ControlKey.RightKey:
                RightKey = keyCode;
                break;
            default:
                break;
        }
    }

}
