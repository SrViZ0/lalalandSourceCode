using UnityEngine;

public class DisableOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject target;

    private void OnDestroy()
    {
        if (target is null) return;
        target.SetActive(false);
    }
}
