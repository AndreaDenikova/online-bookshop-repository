using OnlineBookshop.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookshop.Data.Models
{
    public class ReportedBooks : BaseDeletableModel<string>
    {
        public ReportedBooks()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual Book Book { get; set; }

        public string BookId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
