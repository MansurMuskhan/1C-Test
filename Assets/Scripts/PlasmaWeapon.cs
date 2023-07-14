using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaWeapon : Weapon
{
    [SerializeField] GameObject bulletObject;
    [SerializeField] ParticleSystem effect;
    [SerializeField]
    float
        bulletSpeed = 10,
        bulletAmount = 3,
        bulletsDelta = 0.1f,
        bulletsFigure = 0.1f,
        fireRadius = 10;
    private protected override float rateFire { get; set; }
    private protected override float damage { get; set; }
    private protected override void Fire()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, fireRadius, 1 << 6);
        if (enemies.Length == 0) return;
        Transform target = CalculateNearObject(enemies);
        Vector2 targetDir = (target.position - transform.position).normalized;
        int begin = (int)bulletAmount / 2;
        for (int i = -begin; i < bulletAmount - begin; i++)
        {
            Vector3 f = targetDir * (begin - Mathf.Abs(i)) * bulletsFigure;
            float n = i;
            Vector2 d = RotateNormal(targetDir, n * bulletsDelta);
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletObject, transform.position + f, Quaternion.Euler(0, 0, angle));

            effect.Play();
            effect.transform.rotation = Quaternion.Euler(-angle, 90, 0);

            StartCoroutine(BulletMove(bullet.transform, d));
        }
        SoundsService.Play(AudioName.fire);
    }

    Transform CalculateNearObject(Collider2D[] enemies)
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        Transform closest = null;
        foreach (var go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go.transform;
                distance = curDistance;
            }
        }
        return closest;
    }

    private IEnumerator BulletMove(Transform bullet, Vector3 fireDir)
    {
        float t = 5;
        while (t > 0)
        {
            bullet.position += fireDir * bulletSpeed * Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(bullet.position, fireDir, .5f, 1 << 6);
            Debug.DrawRay(bullet.position, fireDir * 1, Color.red);
            if (hit)
            {
                Core.GetUnitByID(hit.transform.GetInstanceID())?.Unit_Damage(damage);
                break;
            }
            t -= Time.deltaTime;
            yield return null;
        }
        Destroy(bullet.gameObject);
    }
    Vector2 RotateNormal(Vector2 point, float angle)
    {
        Vector2 rotatedPoint;
        rotatedPoint.x = point.x * Mathf.Cos(angle) - point.y * Mathf.Sin(angle);
        rotatedPoint.y = point.x * Mathf.Sin(angle) + point.y * Mathf.Cos(angle);
        return rotatedPoint;
    }
    protected override void Init()
    {
        ActionsService.ValuesUpdate += UpdateValues;
    }

    void UpdateValues()
    {
        fireRadius = Settings.settingsValues[SettingType.RadiusDestruction];
        rateFire = Settings.settingsValues[SettingType.PlayerFireRate];
        damage = Settings.settingsValues[SettingType.BulletDamage];
        bulletSpeed = Settings.settingsValues[SettingType.BulletSpeed];
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, fireRadius);
    }
}
