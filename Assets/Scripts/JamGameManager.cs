using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LastCatMinigame { Menu, CatTree, FurnitureHunt, Window, ZoomiesRace }

public class JamGameManager : MonoBehaviour
{
    public static JamGameManager instance;
    public LastCatMinigame lastCatMinigame = LastCatMinigame.Menu;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneChanger.instance.LoadMenuScene();
    }
}
