public interface IInputProvider
{
    bool isJumpPressed { get; }
    bool wasJumpPressedThisFrame { get; }
    bool wasJumpReleasedThisFrame { get; }
}
