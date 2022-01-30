using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI departmentDetails;

    Dictionary<string, string> hyperlinks = new Dictionary<string, string>
        {
            { "ID_RYE", "https://twitter.com/ryetaran" },
            { "ID_TULIE", "https://twitter.com/NMitiuriev" },
            { "ID_DAVID", "https://www.wonderful-music.com/" },
            { "ID_RICK", "https://trensharo.itch.io/" },
            { "ID_SCOTT", "https://lihingsoftware.itch.io/" },
            { "ID_TARANIS", "https://taraniselsu.github.io/" }
        };

    private const string ART_TEXT = "<align=\"center\"><u>Art</u></align>"
        + "\nAgriades"
        + "\nDebbie \"RyeTaran\" Trader <link=\"ID_RYE\"><u><color=#0000EE>@Ryetaran</color></u></link>"
        + "\nNatalia \"Tulie\" Mitiuriev <link=\"ID_TULIE\"><u><color=#0000EE>@NMitiuriev</color></u></link>"
        + "\nRob \"ZAMUS\" Samson"
        ;

    private const string AUDIO_TEXT = "<align=\"center\"><u>Audio</u></align>"
        + "\nDavid Rubenstein - <link=\"ID_DAVID\"><u><color=#0000EE>https://www.wonderful-music.com/</color></u></link>"
        + "\nJTuba"
        ;

    private const string CODE_TEXT = "<align=\"center\"><u>Code</u></align>"
        + "\nRick Williams - <link=\"ID_RICK\"><u><color=#0000EE>https://trensharo.itch.io/</color></u></link>"
        + "\nScott Hiroshige - <link=\"ID_SCOTT\"><u><color=#0000EE>https://lihingsoftware.itch.io/</color></u></link>"
        + "\nTaranis Elsu - <link=\"ID_TARANIS\"><u><color=#0000EE>https://taraniselsu.github.io/</color></u></link>"
        ;

    private string debugText;

    private void Start()
    {
        // We have to display something when the scene first loads
        ArtButton();
    }

    public void ArtButton()
    {
        SetInstructionsText(ART_TEXT);
    }

    public void AudioButton()
    {
        SetInstructionsText(AUDIO_TEXT);
    }

    public void CodeButton()
    {
        SetInstructionsText(CODE_TEXT);
    }

    private void SetInstructionsText(string text)
    {
        departmentDetails.text = text;
    }

    void OpenURL(string theURL) {
        Application.OpenURL(theURL);
    }

    public void OnTMProLinkClick(string clickedLinkID, string clickedLinkText, int clickedLinkIndex) {
        if (hyperlinks.TryGetValue(clickedLinkID, out string theURL)) {
            OpenURL(theURL);
        }
    }
}
