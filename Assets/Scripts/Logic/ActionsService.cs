using System;

public class ActionsService
{
    public static Action<float> PlayerGetDamage { set; get; }
    public static Action EnemyKill { set; get; }
    public static Action Win { set; get; }
    public static Action GameOver { set; get; }
    public static Action RestartGame { set; get; }
    public static Action ValuesUpdate { set; get; }

}
