using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public bool sprint = false;
    public bool attack { get; private set; } = false;
    public bool skill { get; private set; } = false;
    public bool guard { get; private set; } = false;
    public bool stat { get; private set; } = false;
    public bool item { get; private set; } = false;

    void Update()
    {
        Debug.Log("isPressed = " + sprint);
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnSprint(InputValue value)
    {
        // if (value.isPressed) sprint = true;
        // else sprint = false;
        sprint = value.isPressed;
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            attack = true;
        }
        else if (ctx.canceled)
        {
            attack = false;
        }
    }

    public void OnSkill(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            skill = true;
        }
        else if (ctx.canceled)
        {
            skill = false;
        }
    }

    public void OnGuard(InputAction.CallbackContext ctx)
    {
        guard = ctx.ReadValue<float>() > 0f;
    }

    public void OnStat(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            stat = true;
        }
        else if (ctx.canceled)
        {
            stat = false;
        }
    }

    public void OnItem(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            item = true;
        }
        else if (ctx.canceled)
        {
            item = false;
        }
    }
}
