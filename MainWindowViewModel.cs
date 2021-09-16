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


        public ReactiveCommand<Unit, Unit> ShowNoteDetailsCommand { get; }


        public bool canOperateOnNote = true; //dev only
            public bool CanOperateOnNote
            {
                get { return canOperateOnNote; }
                set { this.RaiseAndSetIfChanged(ref canOperateOnNote, value); }
            }


            string docContent;
            public string DocContent
            {
                get { return docContent; }
                set { this.RaiseAndSetIfChanged(ref docContent, value); }
            }

        NoteTreeViewItem currentClickedNote;
        public NoteTreeViewItem CurrentClickedNote
        {
            get { return currentClickedNote; }
            set {
                //value = value.TreeNodeType == TreeViewItem.NodeType.Note ? value : null;
                //this.RaiseAndSetIfChanged(ref currentClickedNote, value); ;
                this.RaiseAndSetIfChanged(ref currentClickedNote, value); 
            }
        }


        //TreeViewItem currentFolder;
        //public TreeViewItem CurrentFolder
        //{
        //    get { return currentClickedNote; }
        //    set { this.RaiseAndSetIfChanged(ref currentFolder, value); }
        //}

        //private ObservableCollection<Entry> entries;
        //public ObservableCollection<Entry> Entries
        //{
        //    get { return entries; }
        //    set { this.RaiseAndSetIfChanged(ref entries, value); }
        //}
        //public ObservableCollection<ITreeViewItem> Children { get; set; }




        //private Notatka currentNote;
        //public Notatka CurrentNote
        //{
        //    get
        //    {
        //        return currentNote;
        //    }
        //    set
        //    {
        //        this.RaiseAndSetIfChanged(ref currentNote, value);
        //    }
        //}

        //ctor..
        public MainWindowViewModel()
            {
                //Groups = new ObservableCollection<Group>();

                NoteTreeViewItems = new ObservableCollection<NoteTreeViewItem>();

                this.canOperateOnNote = true;

                treeViewDataLoader = new TreeViewService();

                //treeViewDataLoader.PopulateListOfNotes();

                

                populateTree();

            //Load();

            //Load2ndTree();
            //Load();
            //sample command            
            //ShowNoteDetailsCommand = ReactiveCommand.Create<bool>(_ => DeleteInFirstTv(), this.WhenAnyValue(x => x.CanOperateOnNote));

                ShowNoteDetailsCommand = ReactiveCommand.Create<Unit>(_ => ShowDetails());
        }




        //void DeleteInFirstTv()
        //    {
        //    //MessageBox.Show("ja pierdole");
        //    Groups[0].Entries.RemoveAt(0);
        //    var blabla = Groups[0].Items;
        //    }

        /// <summary>
        /// double click
        /// </summary>
        /// <returns></returns>
        private void ShowDetails()
        {
            //if (CurrentClickedNote != null)
            //{
                MessageBox.Show(CurrentClickedNote?.ParentTreeViewItem?.Name);
            //}
        }



    




        public void populateTree()
        {

            var foldersFromNoteBookService = new List<Folder>();


            //var MainFolder = new Folder { Name = "Folder GL 1", ParentFolder=null,  }


            foldersFromNoteBookService.Add(new Folder
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

            foldersFromNoteBookService.Add(
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


            foldersFromNoteBookService.Add(new Folder
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

            //Folders = Folders;

            treeViewDataLoader.LoadFoldersToTreeViewItems(foldersFromNoteBookService);


            NoteTreeViewItems = treeViewDataLoader.TreeViewItems;

        }


        //private void Load2ndTree()
        //{
        //    foreach (var num in Enumerable.Range(1, 1))
        //    {
        //        var item = new TreeViewItem();
        //        item.Name = string.Format("{0} {1}", "FOLDER", num);
        //        item.TreeNodeType = TreeViewItem.NodeType.Folder;

        //        var notatka = new TreeViewItem();
        //        notatka.Name = string.Format("{0} {1} : {2}'s {3}", "Notatka blabla", num, 333, 444);
        //        notatka.TreeNodeType = TreeViewItem.NodeType.Note;
        //        notatka.ParentFolder = item;
        //        item.SubItems.Add(notatka);

        //        var notatka1 = new TreeViewItem();
        //        notatka1.Name = string.Format("{0} {1} : {2}'s {3}", "SELNotatka kuku", num, 44, 55);
        //        notatka1.TreeNodeType = TreeViewItem.NodeType.Note;
        //        //notatka1.IsSelected = true;
        //        notatka1.ParentFolder = item;
        //        item.SubItems.Add(notatka1);
        //        //item.IsExpanded = true;


        //        var pustyFolder = new TreeViewItem();
        //        pustyFolder.Name = string.Format("{0} {1} : {2}'s {3}", "Folder", num, 44, 55);
        //        pustyFolder.TreeNodeType = TreeViewItem.NodeType.Folder;
        //        pustyFolder.ParentFolder = item;
        //        item.SubItems.Add(pustyFolder);

        //        var pustyFolder1 = new TreeViewItem();
        //        pustyFolder1.Name = string.Format("{0} {1} : {2}'s {3}", "Folder", num, 44, 55);
        //        pustyFolder1.TreeNodeType = TreeViewItem.NodeType.Folder;
        //        pustyFolder1.ParentFolder = item;
        //        item.SubItems.Add(pustyFolder);

        //        for (int i = 0; i < 5; i++)
        //        {
        //            var child = new TreeViewItem();
        //            child.Name = string.Format("{0} {1}'s {2}", "FOLDER", num, i);
        //            child.TreeNodeType = TreeViewItem.NodeType.Folder;
        //            child.ParentFolder = item;
        //            item.SubItems.Add(child);
        //            for (int j = 0; j < 3; j++)
        //            {
        //                var grandChild = new TreeViewItem();
        //                grandChild.Name = string.Format("{0} {1} : {2}'s {3}", "KURAItem", num, i, j);
        //                grandChild.ParentFolder = child;

        //                if (j == 1 && i == 1)
        //                {
        //                    grandChild.IsSelected = true;
        //                    CurrentClickedNote = grandChild;
        //                }
        //                child.SubItems.Add(grandChild);
        //            }
        //        }
        //        Data2ndTree.Add(item);               
        //    }
        //}

        //public void Load()
        //{
        //    Group grp1 = new Group() { Key = 1, Name = "Group 1", SubGroups = new ObservableCollection<Group>(), Entries = new ObservableCollection<Entry>() };
        //    Group grp2 = new Group() { Key = 2, Name = "Group 2", SubGroups = new ObservableCollection<Group>(), Entries = new ObservableCollection<Entry>() };
        //    Group grp3 = new Group() { Key = 3, Name = "Group 3", SubGroups = new ObservableCollection<Group>(), Entries = new ObservableCollection<Entry>() };
        //    Group grp4 = new Group() { Key = 4, Name = "Group 4", SubGroups = new ObservableCollection<Group>(), Entries = new ObservableCollection<Entry>() };

        //    //grp1
        //    grp1.Entries.Add(new Entry() { Key = 1, Name = "Entry number 1" });
        //    grp1.Entries.Add(new Entry() { Key = 2, Name = "Entry number 2" });
        //    grp1.Entries.Add(new Entry() { Key = 3, Name = "Entry number 3" });

        //    //grp2
        //    grp2.Entries.Add(new Entry() { Key = 4, Name = "Entry number 4" });
        //    grp2.Entries.Add(new Entry() { Key = 5, Name = "Entry number 5" });
        //    grp2.Entries.Add(new Entry() { Key = 6, Name = "Entry number 6" });

        //    //grp3
        //    grp3.Entries.Add(new Entry() { Key = 7, Name = "Entry number 7" });
        //    grp3.Entries.Add(new Entry() { Key = 8, Name = "Entry number 8" });
        //    grp3.Entries.Add(new Entry() { Key = 9, Name = "Entry number 9" });

        //    //grp4
        //    grp4.Entries.Add(new Entry() { Key = 10, Name = "Entry number 10" });
        //    grp4.Entries.Add(new Entry() { Key = 11, Name = "Entry number 11" });
        //    grp4.Entries.Add(new Entry() { Key = 12, Name = "Entry number 12" });

        //    grp4.SubGroups.Add(grp1);
        //    grp2.SubGroups.Add(grp4);


        //    Groups.Add(grp1);
        //    Groups.Add(grp2);
        //    Groups.Add(grp3);
        //}


    }
}
