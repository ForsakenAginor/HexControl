using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HatSkinChoser : MonoBehaviour
{
    [SerializeField] private Transform _holder;
    [SerializeField] private HatSkinView _prefab;

    private Hatter _hatter;
    private Dictionary<HatSkinView, Hat> _skins = new();
    private HatSkinView _selectedSkin;

    private void OnDestroy()
    {
        foreach (var skin in _skins.Keys)
            skin.Selected -= OnSelected;

        _hatter.HatAdded -= OnHatAdded;
    }

    public void Init(Hatter hatter)
    {
        _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));

        _selectedSkin = Instantiate(_prefab, _holder);
        _selectedSkin.Init(_hatter.ActiveHat.Image);
        _selectedSkin.Select();
        _skins.Add(_selectedSkin, _hatter.ActiveHat);
        _selectedSkin.Selected += OnSelected;

        var hats = _hatter.Hats.Where(o => o != _hatter.ActiveHat);

        foreach (var hat in hats)        
            CreateHatSkinView(hat);        

        _hatter.HatAdded += OnHatAdded;
    }

    private void OnSelected(HatSkinView skin)
    {
        _selectedSkin.Deselect();
        _selectedSkin = skin;
        _hatter.SetActiveHat(_skins[skin]);
    }

    private void OnHatAdded(Hat hat)
    {
        CreateHatSkinView(hat);
    }

    private void CreateHatSkinView(Hat hat)
    {
        var skin = Instantiate(_prefab, _holder);
        skin.Init(hat.Image);
        _skins.Add(skin, hat);
        skin.Selected += OnSelected;
    }
}
