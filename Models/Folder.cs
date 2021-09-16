using Notatnik3.Service.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Notatnik3Service.Model
{
    public class Folder : IFolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Note> Notes { get; set; }
        public IList<Folder> Folders { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? FolderId { get; set; }
        public Folder ParentFolder { get; set; }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}
