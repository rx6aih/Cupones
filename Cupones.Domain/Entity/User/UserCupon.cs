using Cupones.Domain.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Domain.Entity.User
{
	public class UserCupon
	{
		public User User { get; set; }
		public ICupon Cupon { get; set; }
		public int Reaction { get; set; }
	}
}
