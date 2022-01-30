using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStarterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMenuMusic();
        JamGameManager.instance.lastCatMinigame = LastCatMinigame.Menu;
    }

}
