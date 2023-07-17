using System.Runtime.Caching;

namespace DocumentCabinetLibrary
{
    public interface ICache<Key, Value>
    {
        public Value? Get(Key key);
        public void Set(Key key, Value value);

        public void Remove(Key key);
    }
}
