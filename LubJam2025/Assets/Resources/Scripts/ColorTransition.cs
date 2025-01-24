using UnityEngine;
using UnityEngine.UI;

public class ColorTransition : MonoBehaviour
{
    public Material material; // Materia�, kt�rego kolor ma si� zmienia�
    public Color[] colors; // Tablica kolor�w
    public Slider slider; // Referencja do slidera

    private void Start()
    {
        // Subskrybuj zmiany warto�ci slidera
        slider.onValueChanged.AddListener(UpdateColor);
        // Ustaw pocz�tkowy kolor
        UpdateColor(slider.value);
    }

    private void UpdateColor(float value)
    {
        if (colors.Length < 2)
        {
            Debug.LogError("Musisz doda� co najmniej dwa kolory do tablicy colors!");
            return;
        }

        // Przekszta�� warto�� slidera (zakres 0-100) na zakres 0-1
        float normalizedValue = Mathf.Clamp01(value / 100f);

        // Oblicz indeksy kolor�w i interpolacj�
        float scaledValue = normalizedValue * (colors.Length - 1);
        int currentColorIndex = Mathf.FloorToInt(scaledValue);
        int nextColorIndex = Mathf.Clamp(currentColorIndex + 1, 0, colors.Length - 1);

        float t = scaledValue - currentColorIndex;

        // Zmie� kolor materia�u
        material.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);
    }

    private void OnDestroy()
    {
        // Usu� subskrypcj�, aby zapobiec wyciekowi pami�ci
        slider.onValueChanged.RemoveListener(UpdateColor);
    }
}
