namespace Moba
{
    public interface ITransport
    {
        public void TickOutgoing();
        public void TickIncoming();
    }
}