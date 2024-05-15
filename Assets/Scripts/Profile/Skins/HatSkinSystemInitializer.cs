using UnityEngine;

[RequireComponent(typeof(HatsCollection))]
public class HatSkinSystemInitializer : MonoBehaviour
{
    [SerializeField] private HatModelApplier _skinApplier;
    [SerializeField] private HatSkinChoser _skinChoser;
    [SerializeField] private SkinGetter _skinGetter;

    private HatsCollection _skinCollection;

    private void Awake()
    {
        _skinCollection = GetComponent<HatsCollection>();
    }

    private void Start()
    {
        Hatter hatter = new(_skinCollection);
        _skinApplier.Init(hatter);
        _skinChoser.Init(hatter);
        _skinGetter.Init(hatter);
    }
}
