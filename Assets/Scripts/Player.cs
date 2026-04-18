using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移动速度")]
    public float basicMV = 5f;
    public float shiftMV = 10f;
    [Header("跳跃力度")]
    public float basicJV = 5f;
    public float shiftJV = 10f;
    [Header("旋转速度")]
    public float basicRV = 180f;
    public float shiftRV = 360f;
    private float gravity = -9.8f;
    public Vector3 vel, pos, twd, rgt;
    private bool isOnGround;
    private float dt;

    private void Start()
    {
        vel = Vector3.zero;
        pos = transform.position;
        twd = transform.forward;
        rgt = transform.right;
    }
    void Update()
    {
        dt = Time.deltaTime;
        Movement();
    }

    void Movement()
    {
        // 根据旧状态更新本帧状态
        transform.position = pos;
        transform.LookAt(pos + twd);
        isOnGround = (pos.y <= 0);
        // 获取本帧状态变化量保存da
        pos += vel * dt;
        if (pos.y <= 0)
        {
            pos.y = 0;
        }
        float ws = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);
        float ad = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
        bool jp = Input.GetKeyDown(KeyCode.Space);
        bool sfted = Input.GetKey(KeyCode.LeftShift);
        vel.x = 0;
        vel.z = 0;
        if (!isOnGround)
        {
            vel.y += gravity * dt;
        }
        else if (vel.y <= 0)
        {
            vel.y = 0;
        }
        // 水平面方向
        Vector3 dv = (ws * twd + ad * rgt).normalized * (sfted ? shiftMV : basicMV);
        // 竖直方向
        if (jp && isOnGround)
        {
            dv.y += (sfted ? shiftJV : basicJV);
        }
        vel += dv;
        float qe = (Input.GetKey(KeyCode.E) ? 1 : 0) + (Input.GetKey(KeyCode.Q) ? -1 : 0);
        float curEAY = transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, curEAY + qe * (sfted ? shiftRV : basicRV) * dt, 0);
        twd = transform.forward;
        rgt = transform.right;
    }
}