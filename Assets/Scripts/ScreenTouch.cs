using UnityEngine;
using UnityEngine.EventSystems;
public class ScreenTouch : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        Manager.Instance.gold += Manager.Instance.value;
        Manager.Instance.UpdateUI();
    }
}