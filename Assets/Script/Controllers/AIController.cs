using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return true;
    }
    public override float RetrieveMoveInput()
    {
        return 1f;
    }
    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }
    public override bool RetrieveAttackInput() 
    {   
        return false;
    }
    public override bool RetrieveDashInput()
    {
        return false;
    }
}
