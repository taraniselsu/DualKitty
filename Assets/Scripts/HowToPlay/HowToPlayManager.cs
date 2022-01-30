using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowToPlayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI instructions;

    private const string HOUSE_INSTRUCTIONS = "<align=\"center\"><u>House</u></align>\nFind the portals to the minigames. Use the WASD keys to move (Shift to run). Press E when standing in the appropriate spot to access each minigame.";

    private const string CAT_TREE_INSTRUCTIONS = "<align=\"center\"><u>Cat Tree</u></align>\nClimb the giant beanstalk! Press the Space Bar to jump. Use the A and D keys to move left and right.";

    private const string FURNITURE_HUNT_INSTRUCTIONS = "<align=\"center\"><u>Furniture Hunt</u></align>\nAttack the giant feet! Use the A and D keys to move side to side. Use the E key or click the mouse to attack.";

    private const string WINDOW_INSTRUCTIONS = "<align=\"center\"><u>Window</u></align>\nTurn on your audio and enjoy the experience! You'll automatically return to the house afterwards.";

    private const string ZOOMIES_INSTRUCTIONS = "<align=\"center\"><u>Zoomies</u></align>\nGotta go fast! Use the W key to accelerate and the A and D keys to dodge towering hazards.";

    private void Start() {
        // We have to display something when the scene first loads
        HouseButton();
    }

    public void HouseButton() {
        SetInstructionsText(HOUSE_INSTRUCTIONS);
    }

    public void CatTreeButton()
    {
        SetInstructionsText(CAT_TREE_INSTRUCTIONS);
    }

    public void FurnitureHuntButton()
    {
        SetInstructionsText(FURNITURE_HUNT_INSTRUCTIONS);
    }

    public void WindowButton()
    {
        SetInstructionsText(WINDOW_INSTRUCTIONS);
    }

    public void ZoomiesButton()
    {
        SetInstructionsText(ZOOMIES_INSTRUCTIONS);
    }

    private void SetInstructionsText(string text) {
        instructions.text = text;
    }
}
