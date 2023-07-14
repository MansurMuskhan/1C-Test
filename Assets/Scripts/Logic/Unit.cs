using System;
using System.Collections;
using UnityEngine;
public abstract class Unit : MonoBehaviour, IUnit
{
    public virtual float Unit_Health { get; protected set; }
    public SpriteRenderer Unit_SpriteRenderer { get; set; }
    public void Unit_Damage(float damage)
    {
        if (!flash) StartCoroutine(Flash());
        Unit_Health -= damage;
        if (Unit_Health <= 0)
        {
            Die();
        }
        SoundsService.PlayRange(AudioRangeName.enemyDamage);
    }
    protected virtual void Die() { }
    
    bool flash;
    protected IEnumerator Flash()
    {
        flash = true;
        Unit_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.05f);
        float t = .1f;
        while (t > 0)
        {
            Unit_SpriteRenderer.color = Interpolate(Unit_SpriteRenderer.color, Color.white, 10f * Time.deltaTime);
            t -= Time.deltaTime;
            yield return null;
        }
        Unit_SpriteRenderer.color = Color.white;
        flash = false;
    }
    Color Interpolate(Color a, Color b, float t)
    {
        return a * (1 - t) + b * t;
    }
    protected virtual void Awake()
    {
        Core.UnitCache.Add(transform.GetInstanceID(), this);
        Unit_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnDestroy()
    {
        Core.UnitCache.Remove(transform.GetInstanceID());
    }

    public abstract void Unit_Init<T>(T t);
}