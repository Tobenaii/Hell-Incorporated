using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation/Bob")]
public class BobAnim : AnimState
{

    [SerializeField]
    private float m_bobHeight;
    [SerializeField]
    private float m_bobSpeed;
    private float m_velocity;
    private float m_initHeight;
    private float m_sin;

    public override void Init(Transform transform)
    {
        m_initHeight = transform.position.y;
    }

    public override bool UpdateAnim(Transform transform)
    {
        //bob up and down based on sin wave
        m_sin += m_bobSpeed * Time.deltaTime;
        float sin = Mathf.Sin(m_sin);
        transform.position = new Vector3(transform.position.x, m_initHeight + sin * m_bobHeight, transform.position.z);
        return (sin == 0);
    }
}
