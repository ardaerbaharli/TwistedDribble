using UnityEngine;

public class InstructionsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;

    public void OnBackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}