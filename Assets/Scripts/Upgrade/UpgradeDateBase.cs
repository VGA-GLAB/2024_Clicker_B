using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/CreateUpgradeDB")]
public class UpgradeDateBase : ScriptableObject
{
    public List<UpgradeContext> _cookie;
    public List<UpgradeContext> _cursor;
    public List<UpgradeContext> _grandma;

    /// <summary>
    /// ���X�g���擾���܂�
    /// </summary>
    /// <param name="id">���X�g�̔ԍ�</param>
    public List<UpgradeContext> GetList(int id)
    {
        List<UpgradeContext> list = id switch
        {
            0 => _cookie,
            1 => _cursor,
            2 => _grandma,
            _ => null,
        };
        return list;
    }
}
