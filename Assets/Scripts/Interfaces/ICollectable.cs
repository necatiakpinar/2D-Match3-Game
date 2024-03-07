using System.Collections;

namespace NecatiAkpinar.Interfaces
{
    public interface ICollectable
    {
        public IEnumerator Collect(bool isInGoals);
    }
}