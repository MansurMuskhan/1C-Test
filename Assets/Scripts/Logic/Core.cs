using System.Collections.Generic;

public class Core
{
    public static Dictionary<int, IUnit> UnitCache = new Dictionary<int, IUnit>(); // All IUnit Elements in the Scene
    public static IUnit GetUnitByID(int ID)
    {
        if (UnitCache.ContainsKey(ID))
        {
            return UnitCache[ID];
        }
        return null;
    }
}

