using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasManager _mainCanvasReference;

    public CanvasManager MainCanvasReference => _mainCanvasReference;

    public bool IsActive => gameObject.activeInHierarchy;

    public void SetMainCanvasReference(CanvasManager mainCanvas)
    {
        _mainCanvasReference = mainCanvas;
        OnPanelActive();
    }

    public abstract void OnPanelStart();

    public abstract void OnPanelActive();

    public void SetPanelActiveStatus(bool newStatus)
    {
        gameObject.SetActive(newStatus);
    }

    public void ForcePanelClose()
    {
        gameObject.SetActive(false);
    }
}
