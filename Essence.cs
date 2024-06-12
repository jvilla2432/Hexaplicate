﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{


	internal interface Essence
	{
		internal void performEffect(Grid grid, (int,int) hexCoordinate, float deltaT, float strength);
	}

}
