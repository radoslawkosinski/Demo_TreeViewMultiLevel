using Notatnik3.Service.Model;
using Notatnik3Service.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using TreeViewExperimentsIntegration;

namespace WPFEmptyProject
{
    /// <summary>
    /// only keep what is needed to currently show on UI. take the rest from db
    /// </summary>
    public class NoteTreeViewItem : ReactiveObject, INoteItem
    {
        public enum NodeType
        {
            Folder,
            Note
        }


        
        public NodeType TreeNodeType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public NoteTreeViewItem()
        {
            SubItems = new ObservableCollection<NoteTreeViewItem>();
        }

 


        //public TreeViewItem ParentFolder { get; set; }

        ObservableCollection<NoteTreeViewItem> subItems; 
        public ObservableCollection<NoteTreeViewItem> SubItems
        {
            get { return subItems; }
            set { this.RaiseAndSetIfChanged(ref subItems, value); }
        }

        bool isSelected = false; 
        public bool IsSelected
        {
            get { return isSelected; }
            set { this.RaiseAndSetIfChanged(ref isSelected, value); }
        }

        bool isExpanded = false; 
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { this.RaiseAndSetIfChanged(ref isExpanded, value); }
        }

        //public Visibility IsNodeVisible { get; set; }

        Visibility nodeVisibility = Visibility.Visible;
        public Visibility NodeVisibility
        {
            get { return nodeVisibility; }
            set { this.RaiseAndSetIfChanged(ref nodeVisibility, value); }
        }


        bool isNodeVisible;
        public bool IsNodeVisible
        {
            get { return isNodeVisible; }
            set { this.RaiseAndSetIfChanged(ref isNodeVisible, value); }
        }

        //this can change if note modified thus INotifyPropertyChanged
        DateTime? modifyDate;
        public DateTime? ModifyDate
        {
            get { return modifyDate; }
            set { this.RaiseAndSetIfChanged(ref modifyDate, value); }
        }


        //this can change if note modified
        DateTime? lastOpenDate;
        public DateTime? LastOpenDate
        {
            get { return lastOpenDate; }
            set { this.RaiseAndSetIfChanged(ref lastOpenDate, value); }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { this.RaiseAndSetIfChanged(ref name, value); }
        }

        string fileName;
        public string FileName
        {
            get { return fileName; }
            set { this.RaiseAndSetIfChanged(ref fileName, value); }
        }

        NoteTreeViewItem parentTreeViewItem;
        public NoteTreeViewItem ParentTreeViewItem
        {
            get { return parentTreeViewItem; }
            set { this.RaiseAndSetIfChanged(ref parentTreeViewItem, value); }
        }

        string description;
        public string Description {
            get { return description; }
            set { this.RaiseAndSetIfChanged(ref description, value); }
        }

        public NoteTreeViewItem FromNoteItem(INoteItem noteItem)
        {
            var tvItem = new NoteTreeViewItem();

            //this.NodeVisibility = Visibility.Hidden;

            tvItem.Name = noteItem.Name;
            tvItem.CreatedDate = noteItem.CreatedDate;
            tvItem.ModifyDate = noteItem.ModifyDate;
            tvItem.Id = noteItem.Id;

            tvItem.SubItems = new ObservableCollection<NoteTreeViewItem>();

            tvItem.TreeNodeType = (noteItem is Note) ? NodeType.Note : NodeType.Folder;

            return tvItem;
        }




    }
}
