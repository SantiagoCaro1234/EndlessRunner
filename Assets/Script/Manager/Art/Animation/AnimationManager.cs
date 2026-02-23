using UnityEngine;

// componente generico para controlar un animator desde cualquier entidad
public class AnimationManager : MonoBehaviour, IAnimatorController
{
    private Animator animator;

    private void Awake()
    {
        // busca el animator en el mismo objeto o en hijos
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            Debug.LogError("no se encontro animator en " + gameObject.name);
    }

    public void SetBool(string parameter, bool value)
    {
        if (animator != null)
            animator.SetBool(parameter, value);
    }

    public void SetFloat(string parameter, float value)
    {
        if (animator != null)
            animator.SetFloat(parameter, value);
    }

    public void SetTrigger(string parameter)
    {
        if (animator != null)
            animator.SetTrigger(parameter);
    }

    public void SetInteger(string parameter, int value)
    {
        if (animator != null)
            animator.SetInteger(parameter, value);
    }
}