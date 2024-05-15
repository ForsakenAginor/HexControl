using System.Collections.Generic;
using System.Linq;

public class ColorAssignment
{
    private static Dictionary<CellSprite, List<CellSprite>> _colors;

    static ColorAssignment()
    {
        Dictionary<CellSprite, List<CellSprite>> dictionary = new();
        List<CellSprite> list = new List<CellSprite>()
        {
            CellSprite.Red,
            CellSprite.Green,
            CellSprite.Blue,
            CellSprite.Yellow,
            CellSprite.Orange,
            CellSprite.Grey,
            CellSprite.Cyan,
        };

        for (int i = 0; i < list.Count; i++)
            dictionary.Add(list[i], list.Except(new CellSprite[1] { list[i] }).ToList());

        _colors = dictionary;
    }

    public static IEnumerable<CellSprite> GetEnemyColors(CellSprite ownerColor)
    {
        return _colors[ownerColor];
    }
}
