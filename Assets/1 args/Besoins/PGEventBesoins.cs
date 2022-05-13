using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core.PGEvents;
using PGSauce.Core.Strings;

namespace PGSauce.Games.DropDaBomb
{
	[CreateAssetMenu(fileName = "Game Event Besoins", menuName = MenuPaths.GamePath + "Game Events/1 args/Besoins")]
	public class PGEventBesoins : PGEvent1Args<Dictionary<string, Besoins.Besoin>>
	{
	}
}
