namespace Utilities.PathFinding
{
    public interface INode<T> where T : INode<T>
    {
        public int GCost { get; set; }
        public int HCost { get; set; }
        public T Parent { get; set; }
    }
}