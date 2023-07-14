using UnityEngine;

public class Enemy : Unit
{
    [SerializeField]
    GameObject dieEffect;

    private float _moveSpeed;

    protected override void Awake()
    {
        base.Awake();
        ActionsService.RestartGame += RestartGame;
    }
    private void Start()
    {
    }
    protected override void Die()
    {
        Destroy(gameObject);
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2);
        SoundsService.Play(AudioName.enemyDie);
        ActionsService.EnemyKill();
    }

    private void Update()
    {
        Vector3 velocity = Vector3.zero;
        velocity.y = -_moveSpeed * Time.deltaTime;
        transform.position += velocity;

        if(transform.position.y < Settings.ZONE_LINE)
        {
            ActionsService.PlayerGetDamage.Invoke(1);
            Destroy(gameObject);
            GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2);
            SoundsService.Play(AudioName.enemyDie);
        }
    }

    public override void Unit_Init<T>(T t)
    {
        EnemyData data = t as EnemyData;
        Unit_Health = data.MaxHealth;
        _moveSpeed = data.MoveSpeed;
    }

    void RestartGame()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ActionsService.RestartGame -= RestartGame;
    }
}
