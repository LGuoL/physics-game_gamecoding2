using UnityEngine;
using TMPro;

public class SlotReel : MonoBehaviour
{
    public int symbolCount = 6;
    public int CurrentSymbol { get; private set; }

    public float visualSpinSpeed = 900f;
    public float symbolChangeInterval = 0.06f;

    public TextMeshPro symbolText;

    private bool spinning = false;
    private float timer = 0f;

    void Update()
    {
        if (!spinning) return;

        transform.Rotate(Vector3.right, visualSpinSpeed * Time.deltaTime, Space.Self);

        timer += Time.deltaTime;
        if (timer >= symbolChangeInterval)
        {
            timer = 0f;
            CurrentSymbol = Random.Range(0, symbolCount);
            UpdateVisual();
        }
    }

    public void StartSpin()
    {
        spinning = true;
        timer = 0f;
    }

    public void StopSpin()
    {
        spinning = false;
        CurrentSymbol = Random.Range(0, symbolCount);
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (symbolText != null)
            symbolText.text = CurrentSymbol.ToString();
    }
}