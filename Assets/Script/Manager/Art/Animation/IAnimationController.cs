public interface IAnimatorController
{
    void SetBool(string parameter, bool value);
    void SetFloat(string parameter, float value);
    void SetTrigger(string parameter);
    void SetInteger(string parameter, int value);
}