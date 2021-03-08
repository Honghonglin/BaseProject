using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///</summary>
public class Main1 : MonoBehaviour 
{
    private void Start()
    {
        BagMgr.GetInstance().InitItemsInfo();
        UIManager.GetInstance().ShowPanel<BagPanel>("BagPanel");
    }
}

