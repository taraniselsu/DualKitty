using UnityEngine;

public class GameStarterScript : MonoBehaviour
{
    [SerializeField] GameObject kitty;

    [SerializeField] GameObject menuStartPosition;
    [SerializeField] GameObject windowStartPosition;
    [SerializeField] GameObject catTreeStartPosition;
    [SerializeField] GameObject furnitureHuntStartPosition;
    [SerializeField] GameObject raceStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (JamGameManager.instance)
        {
            switch (JamGameManager.instance.lastCatMinigame)
            {
                case LastCatMinigame.Menu:
                    kitty.transform.position = menuStartPosition.transform.position;
                    break;
                case LastCatMinigame.Window:
                    kitty.transform.position = windowStartPosition.transform.position;
                    break;
                case LastCatMinigame.CatTree:
                    kitty.transform.position = catTreeStartPosition.transform.position;
                    break;
                case LastCatMinigame.FurnitureHunt:
                    kitty.transform.position = furnitureHuntStartPosition.transform.position;
                    break;
                case LastCatMinigame.ZoomiesRace:
                    kitty.transform.position = raceStartPosition.transform.position;
                    break;
            }
        }
        else
        {
            kitty.transform.position = menuStartPosition.transform.position;
        }

        if (AudioManager.instance)
        {
            AudioManager.instance.PlayHouseMusic();
        }
    }
}
