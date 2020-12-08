public abstract class ActivationTrap : Trap
{
    public override void TrapReset()
    {
        Deactivate(true);
    }

    protected abstract void Activate(bool p_silent = false);
    protected abstract void Deactivate(bool p_silent = false);
}