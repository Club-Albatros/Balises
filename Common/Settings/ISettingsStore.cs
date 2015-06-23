
using System.Runtime.CompilerServices;

namespace Albatros.DNN.Modules.Balises.Common.Settings
{
    public interface ISettingsStore
    {
        T Get<T>(T @default = default(T), [CallerMemberName] string name = null);
        void Set<T>(T value, [CallerMemberName] string name = null);
        void Save();
    }
}