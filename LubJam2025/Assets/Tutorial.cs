using System.Collections;
using System.Collections.Generic;
using Managers.Sounds;
using NaughtyAttributes;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [Button]
    void Start()
    {
        var sm = SoundManager.Instance;
        sm.PlayClip(sm.MusicSource, sm.MusicCollection.clips[1], true);
        print(sm);

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
