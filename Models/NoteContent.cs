using System;
using System.Collections.Generic;
using System.Text;

namespace Notatnik3Service.Model
{
    public class NoteContent
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int NoteID { get; set; }
        public Note Note { get; set; }
    }
}
