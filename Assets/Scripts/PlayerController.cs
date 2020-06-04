using System;
using System.Linq;
using Rubik;
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
    [SerializeField] private TMP_InputField timeout;
    [SerializeField] private TMP_Dropdown settings;
    [SerializeField] private RubikIDA ida;

    [SerializeField] private HeuristicSettings heuristicSettings;
    
    [SerializeField] private Rubik.Rubik rubik;

    private void OnEnable()
    {
        command.onEndEdit.AddListener(UpdateCommand);
        resetRubikButton.onClick.AddListener(ResetRubik);
        mixRubikButton.onClick.AddListener(MixRubik);
        useDecisionButton.onClick.AddListener(UseDecision);
        solveRubikButton.onClick.AddListener(SolveRubik);
        settings.AddOptions(Enum.GetNames(typeof(HeuristicType)).ToList());
        
        settings.onValueChanged.AddListener(ChangedSettings);
        heuristicSettings.heuristicType = (HeuristicType) 0;
        
        timeout.onEndEdit.AddListener(TimeoutChanged);
    }

    private void TimeoutChanged(string arg0)
    {
        if (float.TryParse(arg0, out float timeout))
            ida.SetTimeout(timeout);
    }

    private void ChangedSettings(int numSettings)
    {
        heuristicSettings.heuristicType = (HeuristicType) numSettings;
    }

    private void OnDisable()
    {
        command.onEndEdit.RemoveListener(UpdateCommand);
        resetRubikButton.onClick.RemoveListener(ResetRubik);
        mixRubikButton.onClick.RemoveListener(MixRubik);
        useDecisionButton.onClick.RemoveListener(UseDecision);
        solveRubikButton.onClick.RemoveListener(SolveRubik);
        
        heuristicSettings.heuristicType = (HeuristicType) 0;
        
        timeout.onValueChanged.RemoveListener(TimeoutChanged);
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
        decision.text = "In progress";
        decision.text = ida.RunTest();
    }

    private void UpdateCommand(string commandStr)
    {
        rubik.Command = commandStr;
    }
}
