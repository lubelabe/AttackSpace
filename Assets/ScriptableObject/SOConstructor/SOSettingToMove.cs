using UnityEngine;

namespace ScriptableObject.SOConstructor
{
    [CreateAssetMenu(menuName = "Settings Move", fileName = "SOValuesToMove")]
    public class SOSettingToMove : UnityEngine.ScriptableObject
    {
        public float ValueMinXToMove;
        public float ValueMaxXToMove;
        public float ValueMinYToMove;
        public float ValueMaxYToMove;
    }
}
