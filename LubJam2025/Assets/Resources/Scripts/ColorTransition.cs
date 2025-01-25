using UnityEngine;
using UnityEngine.UI;

public class ColorTransition : MonoBehaviour
{
    public Image image;
    public Color[] colors;
    public Slider slider;

    private void Start()
    {

    }

    private void Update()
    {
        if (colors.Length < 2)
        {
            Debug.LogError("Musisz dodaæ co najmniej dwa kolory do tablicy colors!");
            return;
        }

        float normalizedValue = Mathf.Clamp01(slider.value / slider.maxValue);

        float scaledValue = normalizedValue * (colors.Length - 1);
        int currentColorIndex = Mathf.FloorToInt(scaledValue);
        int nextColorIndex = Mathf.Clamp(currentColorIndex + 1, 0, colors.Length - 1);

        float t = scaledValue - currentColorIndex;

        image.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);
    }
}
