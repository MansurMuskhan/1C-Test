using System;
public interface IUnit
{
    abstract void Unit_Init<T>(T t);

    void Unit_Damage(float damage);
}