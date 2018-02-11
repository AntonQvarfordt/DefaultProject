using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public FiniteStateMachine FiniteStateMachine { set; get; }
    public string CurrentState = "";

    public Button OptionsButton;
    public Button BackButton;

    public GameObject OptionsGameObject;
    public GameObject MainGameObject;

    private void Awake()
    {
        FSMInitialize();
    }

    private void FSMInitialize()
    {
        //create the finite state machine
        FiniteStateMachine = new FiniteStateMachine();

        //create states and add them to the finite state machine we just created
        FiniteStateMachine.AddState(new OptionsMenuState("Options", OptionsGameObject));
        FiniteStateMachine.AddState(new MainMenuState("Main", MainGameObject));

        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("Main", "Options", new IFSMTransitionCondition[1] { new ClickMenuCondition(OptionsButton) });
        FiniteStateMachine.CreateFSMStateToAnotherFSMStateTransition("Options", "Main", new IFSMTransitionCondition[1] { new ClickMenuCondition(BackButton) });

        FiniteStateMachine.SetDefaultState("Main");

        FiniteStateMachine.OnInitialize();
    }

    private void Update()
    {
        CurrentState = FiniteStateMachine.OutputCurrentStateName();
        FiniteStateMachine.OnUpdate();

    }
}
