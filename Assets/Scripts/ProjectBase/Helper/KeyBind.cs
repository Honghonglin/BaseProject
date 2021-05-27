using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(ISubmitHandler))]
public class KeyBind : MonoBehaviour
{
    public KeyCode keyCode;
    private EventSystem system;

    private void Start()
    {
        system = EventSystem.current;
    }
    private void Update()
    {
        if(Input.GetKeyDown(keyCode)&& system.currentSelectedGameObject == gameObject)
        {
            gameObject.GetComponent<ISubmitHandler>().OnSubmit(new BaseEventData(system));
        }   
    }
}
