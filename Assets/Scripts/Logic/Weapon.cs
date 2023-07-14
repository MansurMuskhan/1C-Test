using System.Collections;
using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    private protected abstract float rateFire { get; set; }
    private protected abstract float damage { get; set; }

    private protected abstract void Fire();
    private IEnumerator fireCycle;
    private void OnEnable()
    {
        fireCycle = fireCycle != null ? fireCycle : FireCycle();
        StartCoroutine(fireCycle);
    }
    private void OnDisable()
    {
        StopCoroutine(fireCycle);
    }
    private void OnDestroy()
    {
        StopCoroutine(fireCycle);
    }
    IEnumerator FireCycle()
    {
        while (enabled)
        {
            Fire();
            yield return new WaitForSeconds(rateFire);
        }
    }
    private void Awake()
    {
        Init();
    }
    protected abstract void Init();
}