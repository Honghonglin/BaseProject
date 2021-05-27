using UnityEngine;
    using System.Collections;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
/// <summary>
/// UGUI Tab键切换InputField
/// </summary>
public class KeyNavigator : MonoBehaviour
{
    private EventSystem system;
    void Start()
    {
        system = EventSystem.current;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&&system.currentSelectedGameObject!=null)
        {
            Selectable next = null;
            var sec = system.currentSelectedGameObject.GetComponent<Selectable>();
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                next = sec.FindSelectableOnUp();
                if (next == null)
                    next = sec;
            }
            else
            {
                next = sec.FindSelectableOnDown();
                if (next == null)
                    next = sec;
            }
            if (next != null)
            {
                var selectable = next.GetComponent<Selectable>();
                if (selectable == null) return;
                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }
}
