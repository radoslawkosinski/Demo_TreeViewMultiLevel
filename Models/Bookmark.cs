using System;
using System.Collections.Generic;
using System.Text;

namespace Notatnik3Service.Model
{
    public class Bookmark : INoteItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Note> Notatki { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
