using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Domain.Entity.Interfaces
{
	public abstract class ICupon
	{
		protected string Description { get; set; }
		protected string Image {  get; set; }
	}
}
