using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
	public enum EssenceType
	{
		Essence1,
		Essence2
	}

	internal interface Essence
	{
		internal void performEffect(Hexagon hex);
	}

}
