using Cupones.Domain.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Domain.Entity.Implementations
{
	public class MacCupon : ICupon
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string OriginLink { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
