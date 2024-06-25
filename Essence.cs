using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{


	internal interface Essence
	{
		public static Essence1 ess1 = new Essence1();
		public static Essence[] essences = { ess1 };
		//Create array of essences, essence takes hexagon and neighbors?(tree may be in my future)
		internal void performEffect((Grid, (int, int)) hex, int strength);
	}

}
