using Notatnik3Service.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Windows;
using TreeViewExperimentsIntegration;

namespace WPFEmptyProject
{

        public class MainWindowViewModel : ReactiveObject
        {
        //public ObservableCollection<MyItemSecondTreeView> Data2ndTree { get; set; }

        //public List<Folder> Folders;
        TreeViewService treeViewDataLoader;



        ObservableCollection<NoteTreeViewItem> noteTreeViewItems; 
        public ObservableCollection<NoteTreeViewItem> NoteTreeViewItems
        {
            get { return noteTreeViewItems; }
            set { this.RaiseAndSetIfChanged(ref noteTreeViewItems, value); }
        }


        /// <summary>
        /// flat version of NoteTreeViewItems with pointers to notes only
        /// we need it for searching so no need to use recurency 
        /// </summary>
        ObservableCollection<NoteTreeViewItem> noteTreeViewItemsFlat;
        public ObservableCollection<NoteTreeViewItem> NoteTreeViewItemsFlat
        {
            get { return noteTreeViewItemsFlat; }
            set { this.RaiseAndSetIfChanged(ref noteTreeViewItemsFlat, value); }
        }


        public ReactiveCommand<Unit, Unit> ShowNoteDetailsCommand { get; }

        public ReactiveCommand<Unit, Unit> SelectFirstNodeCommand { get; }



        /// <summary>
        /// search string entered in textbox to demonstrate dynamic treeview filtering by setting visible=false property on tree view nodes 
        /// that does not match search criteria
        /// </summary>
            string searchString;
            public string SearchString
            {
                get { return searchString; }
                set { 
                this.RaiseAndSetIfChanged(ref searchString, value);
                FilterNodes(value);
            }
            }

        NoteTreeViewItem currentClickedNote;
        public NoteTreeViewItem CurrentClickedNote
        {
            get { return currentClickedNote; }
            set {

                this.RaiseAndSetIfChanged(ref currentClickedNote, value); 
            }
        }


        public MainWindowViewModel()
            {
            
            NoteTreeViewItems = new ObservableCollection<NoteTreeViewItem>();

            NoteTreeViewItemsFlat = new ObservableCollection<NoteTreeViewItem>();


                treeViewDataLoader = new TreeViewService();

               

                populateTree();


                ShowNoteDetailsCommand = ReactiveCommand.Create<Unit>(_ => ShowDetails());

                SelectFirstNodeCommand = ReactiveCommand.Create<Unit>(_ => SelectFirstNode());
        }




        /// <summary>
        /// select first node
        /// </summary>
        /// <returns></returns>
        private void SelectFirstNode()
        {
            CurrentClickedNote = NoteTreeViewItemsFlat
                .Where(x => x.Id == 3)
                .FirstOrDefault();
        }


        /// <summary>
        /// select first node
        /// </summary>
        /// <returns></returns>
        private void FilterNodes(string searchText)
        {
            //show all nodes if nothing found
            if (string.IsNullOrEmpty(searchText))
            {
                foreach (var treeNote in NoteTreeViewItemsFlat)
                {
                    treeNote.NodeVisibility = System.Windows.Visibility.Visible;
                }
                return;
            }

            var notesFound = NoteTreeViewItemsFlat
                .Where(y => y.TreeNodeType == NoteTreeViewItem.NodeType.Note)
                .Where(x => x.Name.ToLower().Contains(searchText.ToLower()))
                .ToList();

            //get only notes
            var flatList = NoteTreeViewItemsFlat
                .Where(x => x.TreeNodeType == NoteTreeViewItem.NodeType.Note)
                .ToList();


            foreach (var treeNote in flatList)
            {
                if (notesFound.Contains(treeNote))
                    treeNote.NodeVisibility = System.Windows.Visibility.Visible;
                else treeNote.NodeVisibility = System.Windows.Visibility.Collapsed;
            }



        }



        /// <summary>
        /// double click
        /// </summary>
        /// <returns></returns>
        private void ShowDetails()
        {

                MessageBox.Show(CurrentClickedNote?.ParentTreeViewItem?.Name);
        }



    



        /// <summary>
        /// demo - populate list
        /// </summary>
        public void populateTree()
        {

            var notesAndFoldersList = new List<Folder>();




            notesAndFoldersList.Add(new Folder
            {
                Id = 1,
                Name = "Folder GL 1",
                Notes = new List<Note>
                {
                    new Note { Name = "notatka bla", Id = 1, Description = "zwykla notatka", FileName = @"C:\ZZZZZ\blabla", FolderId = 1  }
                }
                ,
                Folders = new List<Folder>
                {
                    new Folder
                    {
                        Name = "1st SubFolder GL 1", Id=2,
                                    Notes = new List<Note>
                                    {
                                        new Note {Name="notatka podfol 1 DO PRZENIESIENIA", Id=2, Description="w podfol", FileName=@"C:\ZZZZZ\kuku", FolderId = 2 },
                                        new Note { Name = "notatka1 podfol 1 A", Id = 3, Description = "w podfol", FileName = @"C:\ZZZZZ\kuku" },
                                        new Note { Name = "notatka2 podfol 1 B", Id = 4, Description = "w podfol", FileName = @"C:\ZZZZZ\kuku" },
                                        new Note { Name = "piata notatka notatka3 podfol 1 C", Id = 5, Description = "w podfol", FileName = @"C:\ZZZZZ\kuku" }
                                    },
                                    Folders = new List<Folder>
                                    {
                                            new Folder
                                            {
                                                Name = "2nd SubFolder GL 1", Id=3, Folders=null,
                                                            Notes = new List<Note>
                                                            {
                                                                new Note {Name="notatka podfol ZZ", Id=6, Description="w zagn", FileName=@"C:\ZZZZZ\kuku" },
                                                                new Note { Name = "zagniezfdzona notatka A", Id = 7, Description = "w zagn", FileName = @"C:\ZZZZZ\kuku", FolderId = 2 },
                                                                new Note { Name = "zagniezfdzona notatka B", Id = 8, Description = "w zagn", FileName = @"C:\ZZZZZ\kuku", FolderId = 2 },
                                                                new Note { Name = "zagniezfdzona notatka C", Id = 9, Description = "w zagn", FileName = @"C:\ZZZZZ\kuku", FolderId = 2 },
                                                                new Note { Name = "ZAG zagniezfdzona notatka D", Id = 1500, Description = "w zagn", FileName = @"C:\ZZZZZ\kuku", FolderId = 2 }
                                                            }
                                            }
                                    }

                }
               }
            });

            notesAndFoldersList.Add(
                    new Folder
                    {
                        Name = "Folder GL 2",
                        Id = 4,
                        Notes = new List<Note>
                                    {
                                        new Note {Name="test note 1 in GL2", Id=9, Description="w fol 2", FileName=@"C:\ffff\adaff", FolderId = 4 },
                                        new Note { Name = "notatka blabla  in GL2", Id = 10, Description = "w fol 2", FileName = @"C:\safasdf\gsdfg", FolderId = 4 }
                                    },
                        Folders = null
                    });


            notesAndFoldersList.Add(new Folder
            {
                Id = 5,
                Name = "Folder GL 3",
                Notes = new List<Note>
                {
                    new Note { Name = "KUKU NOTE", Id = 11, Description = "KRA KRA IN GL 3", FileName = @"C:\ZZZZZ\blabla", FolderId = 5 },
                    new Note { Name = "KUKU NOTE", Id = 11, Description = "NOTATKA XYZ IN GL 3", FileName = @"C:\ZZZZZ\blabla", FolderId = 5 }
                }
                ,
                Folders = new List<Folder>
                {
                    new Folder
                    {
                        FolderId = 6,
                        Name = "SubfolderZZ 1 w IN GL 3", Id=6,
                                    Notes = new List<Note>
                                    {
                                        new Note {Name="notatka IN SubfolderZZ", Id=12, Description="w KOLFOL", FileName=@"C:\ZZZZZ\kuku" },
                                        new Note { Name = "NNNNN IN SubfolderZZ", Id = 13, Description = "w KOLFOL", FileName = @"C:\ZZZZZ\kuku", FolderId = 6 },
                                        new Note { Name = "NNNNN IN SubfolderZZ", Id = 14, Description = "w KOLFOL", FileName = @"C:\ZZZZZ\kuku", FolderId = 6 },
                                        new Note { Name = "NNNNN IN SubfolderZZ", Id = 15, Description = "w KOLFOL", FileName = @"C:\ZZZZZ\kuku", FolderId = 6 }
                                    },
                                    Folders = new List<Folder>
                                    {
                                            new Folder
                                            {
                                                Name = "SubfolderYY 1 w IN ZZ", Id=7, Folders=null,
                                                            Notes = new List<Note>
                                                            {
                                                                new Note {Name="YYYYYY IN SubfolderYY A", Id=16, Description="w SubfolderYY", FileName=@"C:\ZZZZZ\kuku" },
                                                                new Note { Name = "YYYYYY IN SubfolderYY A", Id = 17, Description = "w SubfolderYY", FolderId = 7,  FileName = @"C:\ZZZZZ\kuku" },
                                                                new Note { Name = "YYYYYY IN SubfolderYY B", Id = 18, Description = "w SubfolderYY", FolderId = 7,  FileName = @"C:\ZZZZZ\kuku" },
                                                                new Note { Name = "YYYYYY IN SubfolderYY C", Id = 19, Description = "w SubfolderYY", FolderId = 7,  FileName = @"C:\ZZZZZ\kuku" },
                                                                new Note { Name = "YYYYYY IN SubfolderYY D", Id = 20, Description = "w SubfolderYY", FolderId = 7,  FileName = @"C:\ZZZZZ\kuku" }
                                                            }
                                            }
                                    }

                }
               }
            });


            treeViewDataLoader.LoadFoldersToTreeViewItems(notesAndFoldersList);


            NoteTreeViewItems = treeViewDataLoader.TreeViewItems;


            
            var folders = NoteTreeViewItems.Where(x => x.TreeNodeType == NoteTreeViewItem.NodeType.Folder).ToList();

            //Here load notes and folders into NoteTreeViewItemsFlat
            foreach (NoteTreeViewItem fol in folders)
            PopulateFlatList(fol);

        }

        /// <summary>
        /// Recurently load all notes into NoteTreeViewItemsFlat observable collection
        /// so we can search and use notes found to bind to CurrentClickedNote or filter nodes in tree view(by using visible observable property)
        /// </summary>
        /// <param name="folder"></param>
        void PopulateFlatList(NoteTreeViewItem folder)
        {
            //then get notes (subitems with TreeNodeType == NoteTreeViewItem.NodeType.Folder)
            var notesFound = folder.SubItems
                //.Where(x => x.TreeNodeType == NoteTreeViewItem.NodeType.Note)
                .ToList();

            if(notesFound != null)
            foreach (NoteTreeViewItem noteFound in notesFound)
                NoteTreeViewItemsFlat.Add(noteFound);

            //and then enumerate folders (subitems)
            foreach (NoteTreeViewItem fldr in folder.SubItems
                .Where (x => x.TreeNodeType == NoteTreeViewItem.NodeType.Folder))
                PopulateFlatList(fldr);
        }


    }
}
