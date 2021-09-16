using Notatnik3Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notatnik3.Service.Model
{
    public interface INote : INoteItem
    {

        public DateTime? LastOpenDate { get; set; }
        public string FileName { get; set; }
        public int FolderId { get; set; }
        public bool IsSelected { get; set; }
    }
}
