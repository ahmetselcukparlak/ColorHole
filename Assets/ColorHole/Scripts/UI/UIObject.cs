using UnityEngine;

[DisallowMultipleComponent]
public class UIObject : MonoBehaviour
{
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
