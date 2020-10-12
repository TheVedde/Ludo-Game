namespace Game_Ludo.Interfaces
{
    public interface IToken
    {
         GameColor Color { get; }
         TokenState PlTknState { get; }
         int TokenId { get; }
         int tokenPosition { get; }
         int startPos { get; }
         int EndZonePos { get; }
         bool AccessToSafety { get; }
    }
}