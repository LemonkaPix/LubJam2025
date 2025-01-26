using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixer audioMixer2;
    [SerializeField] private AudioMixer audioMixer3;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text value;

    private void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        float volume = slider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 30);
        audioMixer2.SetFloat("Volume", Mathf.Log10(volume) * 30);
        audioMixer3.SetFloat("Volume", Mathf.Log10(volume) * 30 - 18);
        value.text = ((int)(volume * 25)).ToString();
    }
}
