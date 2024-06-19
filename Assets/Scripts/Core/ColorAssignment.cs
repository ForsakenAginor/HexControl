using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.HexGrid;

namespace Assets.Scripts.Core
{
    public class ColorAssignment
    {
        private static readonly Dictionary<CellSprite, List<CellSprite>> _enemiesByColor;

        static ColorAssignment()
        {
            Dictionary<CellSprite, List<CellSprite>> dictionary = new ();
            List<CellSprite> list = new ()
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

            _enemiesByColor = dictionary;
        }

        public static IEnumerable<CellSprite> GetEnemyColors(CellSprite ownerColor)
        {
            return _enemiesByColor[ownerColor];
        }
    }
}