using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_InputField command;
    [SerializeField] private TextMeshProUGUI decision;
    [SerializeField] private Button useDecisionButton;
    [SerializeField] private RubikIDA ida;

    [SerializeField] private Rubik.Rubik rubik;

    private void OnEnable()
    {
        command.onEndEdit.AddListener(UpdateCommand);
        useDecisionButton.onClick.AddListener(UseDecision);
    }

    private void OnDisable()
    {
        command.onEndEdit.RemoveListener(UpdateCommand);
        useDecisionButton.onClick.RemoveListener(UseDecision);
    }

    private void UseDecision()
    {
        ida.RunTest();
    }
    
    private void UpdateCommand(string commandStr)
    {
        rubik.Command = commandStr;
        rubik.MixUpCube();
    }

    private void FixedUpdate()
    {
        decision.text = rubik.Decision;
    }
}
