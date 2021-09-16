using Notatnik3Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notatnik3.Service.Model
{
    public interface IFolder : INoteItem
    {
        public IList<Note> Notes { get; set; }
        public IList<Folder> Folders { get; set; }
        public int? FolderId { get; set; }
    }
}
