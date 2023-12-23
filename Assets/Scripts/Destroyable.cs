using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }
}
