using NecatiAkpinar.Enums;

namespace NecatiAkpinar.Interfaces
{
    public interface IMonoContainer<T>
    {
        public T GetMono(GameElementType elementType);
    }
}