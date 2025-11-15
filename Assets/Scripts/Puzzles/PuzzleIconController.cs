using UnityEngine;
using UnityEngine.UI;

public class PuzzleIconController : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;
    [SerializeField] private GameObject solvedOverlay; // optional tick/checkmark

    private PuzzleManager manager;
    private PuzzleDefinition definition;
    private bool isSolved = false;

    public void Init(PuzzleManager mgr, PuzzleDefinition def)
    {
        manager = mgr;
        definition = def;

        if (iconImage != null && def.iconSprite != null)
        {
            iconImage.sprite = def.iconSprite;
        }

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClicked);
        }

        SetSolved(false);
    }

    private void OnClicked()
    {
        if (isSolved) return;              // already done, do nothing
        if (manager == null) return;

        manager.OpenPuzzle(definition, this);
    }

    public void SetInteractable(bool value)
    {
        if (button != null)
            button.interactable = value;
    }

    public void SetSolved(bool value)
    {
        isSolved = value;
        SetInteractable(!value);

        if (solvedOverlay != null)
            solvedOverlay.SetActive(value);
    }
}
