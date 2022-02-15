using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("键位")]
    [SerializeField]
    protected string keyForward = "d";
    [SerializeField]
    protected string keyBackward = "a";
    [SerializeField]
    protected string keyJump = "space";

    [Header("轴值")]
    public float forwardAndBackward;
    public float target_ForwardAndBackward;
    private float temp_ForwardAndBackward;

    [Header("开关")]
    //控制器自身开关
    public bool inputEnable = true;
    //外接开关
    public bool jumpPress;
    public bool jumpHold;

    private void Update()
    {
        //角色操控轴值
        target_ForwardAndBackward = ((Input.GetKey(keyForward) && inputEnable) ? 1.0f : 0f) - ((Input.GetKey(keyBackward) && inputEnable) ? 1.0f : 0f);
        forwardAndBackward = Mathf.SmoothDamp(forwardAndBackward, target_ForwardAndBackward, ref temp_ForwardAndBackward, 0.1f);
        //角色操控外接开关
        jumpPress = Input.GetKeyDown(keyJump);
        jumpHold = Input.GetKey(keyJump);
    }
}
