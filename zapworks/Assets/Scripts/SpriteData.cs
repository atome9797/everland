using System.Collections.Generic;
using UnityEngine;

// �����Ϳ��� ���� ����� �� �ֵ��� �޴��� ����
[CreateAssetMenu(fileName = "SpriteData", menuName = "Scriptable Object Asset/SpriteData")]
public class SpriteData : ScriptableObject
{
	public Queue<Texture2D> _Texture = new Queue<Texture2D>();
}
