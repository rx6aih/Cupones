﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Domain.Entity.Interfaces
{
	public abstract class ICupon
	{
		public int Id { get; set; }
		protected string Description { get; set; }
		protected string Image {  get; set; }
		public DateTime UpdatedDate { get; set; }


		public int LikesCount {  get; set; }
		public int DislikesCount {  get; set; }
		public int Reaction {  get; set; }
	}
}
