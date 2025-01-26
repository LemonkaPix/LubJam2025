using System.Collections.Generic;
using Managers.Sounds;
using NaughtyAttributes;
using UnityEngine;

public class Levels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sm = SoundManager.Instance;
        sm.StopAll();
        sm.PlayClip(sm.MusicSource, sm.MusicCollection.clips[0], true);
        print(sm);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
