public interface IEffect
{
    void Apply();           // activa el efecto

    void Revert();          // desactiva el efecto (restaura valores originales)
    void Update(float deltaTime); // actualiza temporizador, revierte si expira
    bool IsExpired { get; } // true si el efecto ha terminado
}