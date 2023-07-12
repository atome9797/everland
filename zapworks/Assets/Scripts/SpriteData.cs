using System.Collections.Generic;
using UnityEngine;

// 에디터에서 쉽게 사용할 수 있도록 메뉴를 만듬
[CreateAssetMenu(fileName = "SpriteData", menuName = "Scriptable Object Asset/SpriteData")]
public class SpriteData : ScriptableObject
{
	public Queue<Texture2D> _Texture = new Queue<Texture2D>();
}
