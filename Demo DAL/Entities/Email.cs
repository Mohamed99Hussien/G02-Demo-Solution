﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_DAL.Entities
{
	public class Email
	{
		public int Id { get; set; }

		public string Subject { get; set; }
		public string To { get; set; } // Receipant
		public string Body { get; set; }

			

	}
}
