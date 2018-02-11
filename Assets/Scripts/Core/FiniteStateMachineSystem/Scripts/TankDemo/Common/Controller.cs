using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the unit base class,and it can draw sight view in runtime. 
/// </summary>
public class Controller : MonoBehaviour
{
    //the sight distance of the unit
    public float sightRange = 15.0f;
    //the attack distance of th unit
    public float attackRange = 7.5f;

    //the field of view of the unit
    public float fieldOfView = 60.0f;

    //this is the position where we start draw unit view
    public Transform drawViewRoot;

    //the sight view color show in runtime
    public Color sightViewColor;

    //the attack view color show in runtime
    public Color attackViewColor;

    public Material sightViewMaterial;
    public Material attackViewMaterial;

    //if draw the sight view and attack view when game is running
    public bool drawView;

    // These lists hold the mesh as it is constructed in the LateUpdate
    private List<Vector3> m_SightViewVerticeList = new List<Vector3>();
    private List<Vector2> m_SightViewUVList = new List<Vector2>();
    private List<int> m_SightViewTriangleList = new List<int>();

    // Cached mesh components
    private Mesh m_SightViewMesh = null;
    private MeshRenderer m_SightViewMeshRenderer = null;

    // These lists hold the mesh as it is constructed in the LateUpdate
    private List<Vector3> m_AttackViewVerticeList = new List<Vector3>();
    private List<Vector2> m_AttackViewUVList = new List<Vector2>();
    private List<int> m_AttackViewTriangleList = new List<int>();

    // Cached mesh components
    private Mesh m_AttackViewMesh = null;
    private MeshRenderer m_AttackViewMeshRenderer = null;



    
    // Use this for initialization
    protected virtual void Awake()
    {
        GameObject sightView = new GameObject("sightView");

        sightView.transform.SetParent(this.transform,false);

        sightView.transform.localPosition = drawViewRoot.localPosition;

        m_SightViewMesh = sightView.AddComponent<MeshFilter>().mesh;

        m_SightViewMeshRenderer = sightView.AddComponent<MeshRenderer>();

        m_SightViewMeshRenderer.material = this.sightViewMaterial;

        m_SightViewMeshRenderer.material.color = sightViewColor;


        GameObject attackView = new GameObject("attackView");

        attackView.transform.SetParent(this.transform,false);

        attackView.transform.localPosition = new Vector3(drawViewRoot.localPosition.x, drawViewRoot.localPosition.y + 0.1f, drawViewRoot.localPosition.z);

        m_AttackViewMesh = attackView.AddComponent<MeshFilter>().mesh;

        m_AttackViewMeshRenderer = attackView.AddComponent<MeshRenderer>();

        m_AttackViewMeshRenderer.material = this.attackViewMaterial;

        m_AttackViewMeshRenderer.material.color = attackViewColor;
    }

    protected virtual void LateUpdate()
    {
        if(drawView == false)
        {
            m_SightViewMesh.Clear();
            m_SightViewVerticeList.Clear();
            m_SightViewUVList.Clear();
            m_SightViewTriangleList.Clear();

            m_AttackViewMesh.Clear();
            m_AttackViewVerticeList.Clear();
            m_AttackViewUVList.Clear();
            m_AttackViewTriangleList.Clear();

            return;
        }

        DrawViewInRunTime(m_AttackViewMesh, m_AttackViewVerticeList, m_AttackViewUVList, m_AttackViewTriangleList,attackRange);

        DrawViewInRunTime(m_SightViewMesh, m_SightViewVerticeList, m_SightViewUVList, m_SightViewTriangleList, sightRange);


    }

    /// <summary>
    /// draw view when game is running
    /// </summary>
    private void DrawViewInRunTime(Mesh viewMesh, List<Vector3> viewVerticeList, List<Vector2> viewUVList, List<int> viewTriangleList,float viewRange)
    { 
        float halfFieldOfView = fieldOfView / 2.0f;
        // Clear out the old cone
        viewMesh.Clear();
        viewVerticeList.Clear();
        viewUVList.Clear();
        viewTriangleList.Clear();

        // Always start with the point of the cone
        viewVerticeList.Add(Vector3.zero);
        viewUVList.Add(Vector2.zero);

        for (float angle = -halfFieldOfView; angle <= halfFieldOfView; angle++)
        {
            Quaternion currRot = Quaternion.identity;

            currRot.eulerAngles = new Vector3(0.0f, angle, 0.0f);

            // Calculate where the cone should reach to at this angle
            Vector3 checkVector = currRot * (viewRange * Vector3.forward);

            // No obstacle, use the full visibleDistance of the cone
            viewVerticeList.Add(checkVector);

            viewUVList.Add(new Vector2((halfFieldOfView + angle) / halfFieldOfView, 1.0f));

        }

        // Create the mesh triangles from the vertices
        for (int i = 2; i < viewVerticeList.Count; i++)
        {
            viewTriangleList.Add(0);
            viewTriangleList.Add(i - 1);
            viewTriangleList.Add(i);
        }

        // Finally, create the actual mesh from vertices, UVs, and triangles
        viewMesh.vertices = viewVerticeList.ToArray();
        viewMesh.uv = viewUVList.ToArray();
        viewMesh.triangles = viewTriangleList.ToArray();

        viewMesh.RecalculateBounds();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            return;
        }


        DrawViewInEditor(sightRange, sightViewColor);

        DrawViewInEditor(attackRange,attackViewColor);
    }

    /// <summary>
    /// draw view in editor mode
    /// </summary>
    private void DrawViewInEditor(float range,Color viewColor)
    {
        float halfFieldOfView = fieldOfView / 2.0f;

        Gizmos.color = viewColor;

        Transform root = drawViewRoot;

        if (null == root)
        {
            root = gameObject.transform;
        }


        // Forward direction
        Vector3 direction = root.TransformDirection(Vector3.forward) * range;

        Gizmos.DrawRay(root.position, direction);

        // Left and right side of the arc extents
        Gizmos.DrawRay(root.position, range * (Quaternion.AngleAxis(-halfFieldOfView, Vector3.up) * transform.forward));

        Gizmos.DrawRay(root.position, range * (Quaternion.AngleAxis(halfFieldOfView, Vector3.up) * transform.forward));

        int innerArcAngles = (int)(halfFieldOfView * 2.0f);

        for (float delta = 0.25f; delta < innerArcAngles; delta += 0.25f)
        {
            Gizmos.DrawRay(root.position, range * (Quaternion.AngleAxis(-halfFieldOfView + delta, Vector3.up) * transform.forward));
        }
    }

}
