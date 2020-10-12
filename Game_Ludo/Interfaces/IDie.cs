namespace Game_Ludo.Interfaces
{
    public interface IDie
    { 
        int DieValue { get; }
        int GetValue();
        int ThrowDice();
        
    }
}