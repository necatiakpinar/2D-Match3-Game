using System.Collections;

namespace NecatiAkpinar.Interfaces
{
    public interface IActivatable
    {
        public IEnumerator Activate(bool isInGoals);
    }
}