using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the Finite State Machine class that each AI or GameObject in your game must have in order to use the framework. 
/// It stores the AI's States in a Dictionary, has methods to add and delete a state 
/// </summary>
public class FiniteStateMachine
{
    /// <summary>
    ///  This Dictionary with pairs (statename-state) showing
    ///  which state the FSM should be if a transition is fired while this state
    ///  is the current state.
    /// </summary>
    private Dictionary<string, FSMState> m_FSMStateDic = new Dictionary<string, FSMState>();

    /// <summary>
    /// This state is the first state when the finite state machine system run,and you can not change it
    /// </summary>
    private FSMState m_Entry;

    /// <summary>
    /// This state is the exit state when the finite state machine system enter to it.
    /// One the system enter this state,the finite state machine system can not switch to other state,
    /// represent the finite state machine is stop running;
    /// </summary>
    private FSMState m_Exit;

    /// <summary>
    /// The state which will runs after the entry state
    /// </summary>
    private FSMState m_DefaultFSMState;

    /// <summary>
    /// The end state,the state which will run before enter the exit state
    /// </summary>
    private FSMState m_EndFSMState;

    /// <summary>
    /// The current state which the Finite State Machine updates
    /// </summary>
    private FSMState m_CurrentFSMState;


    /// <summary>
    /// The Initialize function before the finite state machine system running 
    /// </summary>
    public void OnInitialize()
    {
        m_Entry = new FSMEntry("FSMEntry");

        IFSMTransitionCondition[] entryFSMTransitionConditionArray = new IFSMTransitionCondition[1] { new FSMDefaultTransitionCondition() };

        m_Entry.AddTransition(m_DefaultFSMState, entryFSMTransitionConditionArray);

        AddState(m_Entry);

        this.m_CurrentFSMState = m_Entry;
    }

    /// <summary>
    /// The Update Function of the finite state machine
    /// </summary>
    public void OnUpdate()
    {
        ExecuteCurrentFSMStateOnCheckTransition();

        ExecuteCurrentFSMStateOnUpdate();
    }

    /// <summary>
    /// Get the state name,then convert it to hash string as the key, and the state as the value,
    /// Add them to the dictionary
    /// </summary>
    /// <param name="fsmState">the state</param>
    public void AddState(FSMState fsmState)
    {
        if (fsmState == null)
        {
            Debug.LogError("the fsmState can not be null");

            return;
        }

        string fsmStateName = fsmState.FSMStateName;

        //convert the state name to hash value,to improve performance
        string fsmStateNameHash = HashTool.StringToHash(fsmStateName);

        if (m_FSMStateDic.ContainsKey(fsmStateNameHash) == false)
        {
            m_FSMStateDic.Add(fsmStateNameHash, fsmState);
        }
        else
        {
            Debug.LogErrorFormat("The finite state machine already have the state:{0}", fsmState.FSMStateName);
        }

    }

    /// <summary>
    /// Delete state from the dictionary according to its name
    /// </summary>
    /// <param name="fsmStateName">the state name</param>
    public void DeleteState(string fsmStateName)
    {
        if (fsmStateName == null || fsmStateName == "")
        {
            Debug.LogError("the fsmStateName can not be null or empty");

            return;
        }

        //convert the state name to hash value,to improve performance
        string fsmStateNameHash = HashTool.StringToHash(fsmStateName);

        if (m_FSMStateDic.ContainsKey(fsmStateNameHash) == true)
        {
            m_FSMStateDic.Remove(fsmStateNameHash);
        }
        else
        {
            Debug.LogErrorFormat("The finite state machine do not have the state:{0}", fsmStateName);
        }

    }

    /// <summary>
    /// Set the default state
    /// </summary>
    /// <param name="fsmStateName">The state name</param>
    public void SetDefaultState(string fsmStateName)
    {
        if (fsmStateName == null || fsmStateName == "")
        {
            Debug.LogError("the fsmStateName can not be null or Empty");

            return;
        }

        FSMState fsmState = null;

        //convert the state name to hash value,to improve performance
        string fsmStateNameHash = HashTool.StringToHash(fsmStateName);

        bool exist = m_FSMStateDic.TryGetValue(fsmStateNameHash, out fsmState);

        if (exist == true)
        {
            this.m_DefaultFSMState = fsmState;
        }
        else
        {
            Debug.LogErrorFormat("The {0} is not exist", fsmStateName);
        }


    }

    /// <summary>
    /// Set the end state
    /// </summary>
    /// <param name="fsmStateName">The state name</param>
    /// <param name="endStateToExitStateFSMTransitionConditionArray">Some conditions of endState switch to the exitState</param>
    public void SetEndState(string fsmStateName, IFSMTransitionCondition[] endStateToExitStateFSMTransitionConditionArray)
    {
        if (fsmStateName == null || fsmStateName == "")
        {
            Debug.LogError("the fsmStateName can not be null or Empty");

            return;
        }

        FSMState fsmState = null;

        //convert the state name to hash value,to improve performance
        string fsmStateNameHash = HashTool.StringToHash(fsmStateName);

        bool exist = m_FSMStateDic.TryGetValue(fsmStateNameHash, out fsmState);

        if (exist == true)
        {
            this.m_EndFSMState = fsmState;

            m_Exit = new FSMExit("FSMExit");

            AddState(m_Exit);

            this.m_EndFSMState.AddTransition(m_Exit, endStateToExitStateFSMTransitionConditionArray);
        }
        else
        {
            Debug.LogErrorFormat("The {0} is not exist", fsmStateName);
        }
    }

    /// <summary>
    /// output the name of current state which the finite state machine updates
    /// </summary>
    /// <returns></returns>
    public string OutputCurrentStateName()
    {
        return m_CurrentFSMState.FSMStateName;
    }

    /// <summary>
    /// Create a transition which represent the state switch to another state,and add it to the transition list.
    /// </summary>
    /// <param name="fsmStateName">The state name</param>
    /// <param name="nextFSMStateName">The name of the next state</param>
    /// <param name="fsmTransitionConditionArray">Some transition conditions</param>
    public FSMTransition CreateFSMStateToAnotherFSMStateTransition(string fsmStateName, string nextFSMStateName, IFSMTransitionCondition[] fsmTransitionConditionArray)
    { 
        //convert the state name to hash value,to improve performance
        string fsmStateNameHash = HashTool.StringToHash(fsmStateName);

        FSMState fsmState = null;

        bool fsmStateExist = m_FSMStateDic.TryGetValue(fsmStateNameHash, out fsmState);

        if (fsmStateExist == false)
        {
            Debug.LogErrorFormat("The fsmStateName:{0} is not exist", fsmStateName);

            return null;
        }

        //convert the state name to hash value,to improve performance
        string nextFSMStateNameHash = HashTool.StringToHash(nextFSMStateName);

        FSMState nextFSMState = null;

        bool nextFSMStateExist = m_FSMStateDic.TryGetValue(nextFSMStateNameHash, out nextFSMState);

        if (nextFSMStateExist == false)
        {
            Debug.LogErrorFormat("The nextFSMStateName:{0} is not exist", nextFSMStateName);

            return null;
        }

        return fsmState.AddTransition(nextFSMState, fsmTransitionConditionArray);
    }

    /// <summary>
    /// Delete the specific transition in the state
    /// </summary>
    /// <param name="fsmStateName">The state name</param>
    /// <param name="fsmTransition">The specific transition</param>
    public void DeleteFSMStateToAnotherFSMStateTransition(string fsmStateName, FSMTransition fsmTransition)
    {
        //convert the state name to hash value,to improve performance
        string fsmStateNameHash = HashTool.StringToHash(fsmStateName);

        FSMState fsmState = null;

        bool fsmStateExist = m_FSMStateDic.TryGetValue(fsmStateNameHash, out fsmState);

        if (fsmStateExist == true)
        {
            fsmState.DeleteTransition(fsmTransition);
        }
        else
        {
            Debug.LogErrorFormat("The fsmStateName:{0} is not exist", fsmStateName);
        }

    }

    /// <summary>
    ///  Create each state switch to the given state's transition ,and add them to the transition list.
    /// </summary>
    /// <param name="nextFsmStateName">the state which them will switch to</param>
    /// <param name="fsmTransitionConditionArray">Some transition conditions</param>
    public void CreateAnyFSMStateToFSMStateTransition(string nextFsmStateName, IFSMTransitionCondition[] fsmTransitionConditionArray)
    {
        string nextFSMStateNameHash = HashTool.StringToHash(nextFsmStateName);

        FSMState nextFSMState = null;

        bool nextFSMStateExist = m_FSMStateDic.TryGetValue(nextFSMStateNameHash, out nextFSMState);

        if (nextFSMStateExist == false)
        {
            Debug.LogErrorFormat("The nextFSMStateName:{0} is not exist", nextFsmStateName);

            return;
        }


        foreach (KeyValuePair<string, FSMState> kv in m_FSMStateDic)
        {
            FSMState fsmState = kv.Value;

            if (fsmState != m_Entry
                && fsmState != m_Exit
                && fsmState != m_EndFSMState
                && fsmState != nextFSMState)
            {
                fsmState.AddTransition(nextFSMState, fsmTransitionConditionArray);
            }
        }

    }

    /// <summary>
    /// check all the conditions of current state,if match the condition,then exit current state and switch to the next state. 
    /// </summary>
    public void ExecuteCurrentFSMStateOnCheckTransition()
    {
        if (m_CurrentFSMState.OnCheckTransition() == true)
        {
            //execute current state OnExit function
            m_CurrentFSMState.OnExit();

            //get the next state
            FSMState fsmState = m_CurrentFSMState.GetToNextFSMState();

            //switch to the next state
            m_CurrentFSMState = fsmState;

            //execute the next state OnEnter function
            m_CurrentFSMState.OnEnter();
        }
    }

    /// <summary>
    /// Update the current state's update function
    /// </summary>
    public void ExecuteCurrentFSMStateOnUpdate()
    {
        m_CurrentFSMState.OnUpdate();
    }


}
