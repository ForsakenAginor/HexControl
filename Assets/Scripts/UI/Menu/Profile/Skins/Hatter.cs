using System;
using System.Collections.Generic;
using System.Linq;

public class Hatter
{
    private Hat _activeHat;
    private List<Hat> _ownedHats = new();
    private IEnumerable<Hat> _hatsList;

    public Hatter(HatsCollection collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        _hatsList = collection.Hats;
        _activeHat = _hatsList.First(o => o.Type == HatSkinData.Instance.ActiveHat);
        var ownedHats = HatSkinData.Instance.OwnedHats;
        _ownedHats = _hatsList.Where(o => ownedHats.Contains(o.Type)).ToList();
    }

    public event Action<Hat> HatAdded;
    public event Action ActiveHatChanged;

    public Hat ActiveHat => _activeHat;
    public IEnumerable<Hat> Hats => _ownedHats;

    public bool TryEarnRandomHat(out Hat hat)
    {
        var stillNotAllowedHats = _hatsList.Except(_ownedHats);

        if(stillNotAllowedHats.Count() == 0)
        {
            hat = null;
            return false;
        }

        hat = stillNotAllowedHats.ToArray()[UnityEngine.Random.Range(0, stillNotAllowedHats.Count())];
        _ownedHats.Add(hat);
        HatAdded?.Invoke(hat);
        HatSkinData.Instance.SaveChanges(_ownedHats.Select(o =>o.Type), _activeHat.Type);
        return true;
    }

    public void SetActiveHat(Hat hat)
    {
        if(hat == null)
            throw new ArgumentNullException(nameof(hat));

        if(_ownedHats.Contains(hat) == false)
            throw new ArgumentOutOfRangeException(nameof(hat));

        _activeHat = hat;
        ActiveHatChanged?.Invoke();
        HatSkinData.Instance.SaveChanges(_ownedHats.Select(o => o.Type), _activeHat.Type);
    }
}
