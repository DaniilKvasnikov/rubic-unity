using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_InputField command;
    [SerializeField] private TextMeshProUGUI decision;
    [SerializeField] private Button resetRubikButton;
    [SerializeField] private Button mixRubikButton;
    [SerializeField] private Button useDecisionButton;
    [SerializeField] private Button solveRubikButton;
    [SerializeField] private RubikIDA ida;

    [SerializeField] private Rubik.Rubik rubik;

    private void OnEnable()
    {
        command.onEndEdit.AddListener(UpdateCommand);
        resetRubikButton.onClick.AddListener(ResetRubik);
        mixRubikButton.onClick.AddListener(MixRubik);
        useDecisionButton.onClick.AddListener(UseDecision);
        solveRubikButton.onClick.AddListener(SolveRubik);
    }

    private void OnDisable()
    {
        command.onEndEdit.RemoveListener(UpdateCommand);
        resetRubikButton.onClick.RemoveListener(ResetRubik);
        mixRubikButton.onClick.RemoveListener(MixRubik);
        useDecisionButton.onClick.RemoveListener(UseDecision);
        solveRubikButton.onClick.RemoveListener(SolveRubik);
    }

    private void ResetRubik()
    {
        rubik.ResetCube();
    }

    private void MixRubik()
    {
        rubik.MixUpCube();
    }

    private void UseDecision()
    {
        rubik.UseDecision();
    }

    private void SolveRubik()
    {
        ida.RunTest();
    }

    private void UpdateCommand(string commandStr)
    {
        rubik.Command = commandStr;
    }

    private void FixedUpdate()
    {
        decision.text = rubik.Decision;
    }
}
