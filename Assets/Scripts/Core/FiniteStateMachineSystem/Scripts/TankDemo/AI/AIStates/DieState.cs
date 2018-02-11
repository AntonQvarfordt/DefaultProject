using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when the life value of ai be reduce to zero,we create some explosion effect
/// and change the color of ai tank
/// </summary>
public class DieState:AIFSMState
{
    //the explosion effect
    private GameObject m_explosionEffect;

    private List<Renderer> m_RenderList = new List<Renderer>();

    public DieState(string fsmStateName, AIController aiController)
        : base(fsmStateName, aiController)
    {
        this.m_AIController = aiController;
    }

    public override void OnEnter()
    {
        //the ai tank is died,so disable the function of draw view
        this.m_AIController.drawView = false;
        UIManager.GetInstance().showAIViewEvent -= this.m_AIController.DrawViewEvent;

        m_explosionEffect = this.m_AIController.explosionEffect.gameObject;

        m_RenderList.Add(this.m_AIController.MainBodyRender);

        m_RenderList.Add(this.m_AIController.NozzleRender);

        m_RenderList.Add(this.m_AIController.TrackRender);

        //play the explosion effect
        m_explosionEffect.SetActive(true);    

        //change the color of the ai tank
        for (int i = 0; i < m_RenderList.Count;i++ )
        {
            Renderer render = m_RenderList[i];

            for (int j = 0; j < render.materials.Length;j++ )
            {
                Material material = render.materials[j];

                material.color = new Color(0.1f, 0.1f, 0.1f, 0.0f);
            }           
        }
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        m_RenderList.Clear();

        m_explosionEffect = null;

        UIManager.GetInstance().ShowRestartPanel();
    }
}
