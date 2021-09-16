using System;
using System.Collections.Generic;
using System.Text;

namespace Notatnik3Service.Model
{
    public interface INoteItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Description { get; set; }
    }
}
