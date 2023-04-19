using KitchenData;
using System.Collections.Generic;

namespace CodelessModInterop.Customs
{
    public class UnlockEffectSet
    {
        public bool IsReadOnly { get; protected set; }

        protected List<UnlockEffect> _unlockEffects = new List<UnlockEffect>();

        public IEnumerable<UnlockEffect> UnlockEffects => IsReadOnly? new List<UnlockEffect>(_unlockEffects) : _unlockEffects;

        public UnlockEffectSet(IEnumerable<UnlockEffect> unlockEffects, bool readOnly = true)
        {
            _unlockEffects = new List<UnlockEffect>(unlockEffects);
            IsReadOnly = readOnly;
        }
    }
}
