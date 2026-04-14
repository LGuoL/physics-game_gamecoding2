using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Highlight Setting")]
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;

    private Renderer _renderer;
    private Color _OriginalEmissionColor;
    private bool _isHighlighted = false;

    protected virtual void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if ( _renderer != null )
        {
            _OriginalEmissionColor = _renderer.material.GetColor("_EmissionColor");
        }
    }

    public virtual void Highlight()
    {
        if(_isHighlighted || _renderer == null) return;

        _isHighlighted = true;
        _renderer.material.EnableKeyword("EMISSION");
        _renderer.material.SetColor("_EmissionColor", highlightColor * highlightIntensity);
    }    

    public virtual void Unhighlight()
    {
        if (_isHighlighted || _renderer == null) return;

        _isHighlighted = false;
        _renderer.material.SetColor("_EmissionColor", _OriginalEmissionColor);

        _renderer.material.DisableKeyword("_EMISSON");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}