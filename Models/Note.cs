using Notatnik3.Service.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Notatnik3Service.Model
{
    public class Note : INote
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? LastOpenDate { get; set; }
        public string FileName { get; set; }
        public int FolderId { get; set; }
        public bool IsSelected { get; set; }
        public Folder ParentFolder { get; set; }
    }
}
