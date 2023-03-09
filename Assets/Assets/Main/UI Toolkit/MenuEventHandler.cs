using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuEventHandler : MonoBehaviour
{
    [SerializeField]
    private UIDocument _uiDocument;

    public GameObject Address;
    
    public void Start()
    {
        VisualElement root = _uiDocument.rootVisualElement;
        
        // Buttons
        Button Campaign = (Button)root.Q("Campaign");
        Campaign.RegisterCallback<ClickEvent>(CampaignClick);

        Button Multiplayer = (Button)root.Q("Multiplayer");
        Multiplayer.RegisterCallback<ClickEvent>(MultiplayerClick);

        Button MapEditor = (Button)root.Q("MapEditor");
        MapEditor.RegisterCallback<ClickEvent>(MapEditorClick);

        Button Quit = (Button)root.Q("Quit");
        
        // Multiplayer selection
        Button Connect = (Button)root.Q("Connect");
        Connect.RegisterCallback<ClickEvent>(MultiplayerOnlineClick);

        Button Localhost = (Button)root.Q("Localhost");
        Localhost.RegisterCallback<ClickEvent>(MultiplayerLocalhostClick);
        
        Button Back = (Button)root.Q("Back");
        Back.RegisterCallback<ClickEvent>(MultiplayerBackClick);
    }

    private void CampaignClick(ClickEvent e)
    {
        VisualElement root = _uiDocument.rootVisualElement;
        Button Campaign = (Button)root.Q("Campaign");

        Campaign.text = "Not implemented";
    }

    private void MultiplayerClick(ClickEvent e)
    {
        VisualElement root = _uiDocument.rootVisualElement;
        VisualElement Buttons = root.Q("Buttons");
        Buttons.style.display = DisplayStyle.None;

        VisualElement MultiplayerButtons = root.Q("MultiplayerButtons");
        MultiplayerButtons.style.display = DisplayStyle.Flex;
    }

    private void MultiplayerOnlineClick(ClickEvent e)
    {
        VisualElement root = _uiDocument.rootVisualElement;
        Button Connect = (Button)root.Q("Connect");
        TextField IPField = (TextField)root.Q("IPField");

        if (IPField.text != "Server IP")
            Address.GetComponent<PersistentAddress>().IPAddress = IPField.text;
        
        SceneManager.LoadScene("TestMulti1");
    }

    private void MultiplayerLocalhostClick(ClickEvent e)
    {
        Address.GetComponent<PersistentAddress>().IsHost = true;
        SceneManager.LoadScene("TestMulti1");
    }

    private void MultiplayerBackClick(ClickEvent e)
    {
        VisualElement root = _uiDocument.rootVisualElement;
        VisualElement Buttons = root.Q("Buttons");
        Buttons.style.display = DisplayStyle.Flex;

        VisualElement MultiplayerButtons = root.Q("MultiplayerButtons");
        MultiplayerButtons.style.display = DisplayStyle.None;
    }

    private void MapEditorClick(ClickEvent e)
    {
        SceneManager.LoadScene("MapEditor");
    }
}
