using UnityEngine;
using UnityEngine.UI;

public class ColorTransition : MonoBehaviour
{
    public Material material; // Materia³, którego kolor ma siê zmieniaæ
    public Color[] colors; // Tablica kolorów
    public Slider slider; // Referencja do slidera

    private void Start()
    {
        // Subskrybuj zmiany wartoœci slidera
        slider.onValueChanged.AddListener(UpdateColor);
        // Ustaw pocz¹tkowy kolor
        UpdateColor(slider.value);
    }

    private void UpdateColor(float value)
    {
        if (colors.Length < 2)
        {
            Debug.LogError("Musisz dodaæ co najmniej dwa kolory do tablicy colors!");
            return;
        }

        // Przekszta³æ wartoœæ slidera (zakres 0-100) na zakres 0-1
        float normalizedValue = Mathf.Clamp01(value / 100f);

        // Oblicz indeksy kolorów i interpolacjê
        float scaledValue = normalizedValue * (colors.Length - 1);
        int currentColorIndex = Mathf.FloorToInt(scaledValue);
        int nextColorIndex = Mathf.Clamp(currentColorIndex + 1, 0, colors.Length - 1);

        float t = scaledValue - currentColorIndex;

        // Zmieñ kolor materia³u
        material.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);
    }

    private void OnDestroy()
    {
        // Usuñ subskrypcjê, aby zapobiec wyciekowi pamiêci
        slider.onValueChanged.RemoveListener(UpdateColor);
    }
}
