using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private PlayListPanel _playListPanel;
    [SerializeField] private KaraokePanel _karaokePanel;
    [SerializeField] private MainMenuPanel _mainMenuPanel;
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private MicrophoneSelectionPanel _microphoneSelectionPanel;

    public PlayListPanel PlayListPanel => _playListPanel;
    public KaraokePanel KaraokePanel => _karaokePanel;
    public MainMenuPanel MainMenuPanel => _mainMenuPanel;
    public SettingsPanel SettingsPanel => _settingsPanel;
    public MicrophoneSelectionPanel MicrophoneSelectionPanel => _microphoneSelectionPanel;

    private BasePanel _activePanel;
    private BasePanel _lastActivePanel;

    private string _cachedSongPath;
    public string CachedSongPath { get => _cachedSongPath; set => _cachedSongPath = value; }

    private void Start()
    {
        _settingsPanel.SetMainCanvasReference(this);
        _playListPanel.SetMainCanvasReference(this);
        _karaokePanel.SetMainCanvasReference(this);
        _mainMenuPanel.SetMainCanvasReference(this);
        _microphoneSelectionPanel.SetMainCanvasReference(this);

        _settingsPanel.ForcePanelClose();
        _playListPanel.ForcePanelClose();
        _karaokePanel.ForcePanelClose();
        _mainMenuPanel.ForcePanelClose();
        _microphoneSelectionPanel.ForcePanelClose();

        SetActiveNewPanel(MainMenuPanel);
    }

    public void SetActiveNewPanel(BasePanel newWindow)
    {
        if (_activePanel != null) { _lastActivePanel = _activePanel; _activePanel.SetPanelActiveStatus(false); };
        newWindow.SetPanelActiveStatus(true);
        _activePanel = newWindow;

        _activePanel.OnPanelStart();
    }
}

