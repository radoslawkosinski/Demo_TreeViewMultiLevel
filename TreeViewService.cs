using Notatnik3Service.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFEmptyProject;

namespace TreeViewExperimentsIntegration
{
    class TreeViewService : ReactiveObject
    {

        private List<Folder> folderList = new List<Folder>();


        ObservableCollection<NoteTreeViewItem> treeViewItems;
        public ObservableCollection<NoteTreeViewItem> TreeViewItems
        {
            get { return treeViewItems; }
            set { this.RaiseAndSetIfChanged(ref treeViewItems, value); }
        }

        public TreeViewService()
        {
            TreeViewItems = new ObservableCollection<NoteTreeViewItem>();
        }




        public void DeleteNoteOrFolderInTree(NoteTreeViewItem TreeViewItemToDelete)
        {
            var currentFolder = TreeViewItemToDelete.ParentTreeViewItem;

            currentFolder.SubItems.Remove(TreeViewItemToDelete);
        }

        public void MoveNoteInTree(int DestFolderId, NoteTreeViewItem noteToMove)
        {
            var desFolder = FindInTreeView(NoteTreeViewItem.NodeType.Folder, DestFolderId, false);
            var currentFolder = noteToMove.ParentTreeViewItem;

            currentFolder.SubItems.Remove(noteToMove);
            desFolder.SubItems.Add(noteToMove);


            noteToMove.IsSelected = true;
            noteToMove.IsExpanded = true;
        }


        public void AddFolder(NoteTreeViewItem treeVewContainer, Folder folder)
        {
            var newTvFolder = new NoteTreeViewItem().FromNoteItem(folder);
            treeVewContainer.SubItems.Add(newTvFolder);
        }

        public void AddNote(NoteTreeViewItem treeVewContainer, Note note)
        {
            var newTvFolder = new NoteTreeViewItem().FromNoteItem(note);
            treeVewContainer.SubItems.Add(newTvFolder);
        }


       

        public NoteTreeViewItem FindInTreeView(NoteTreeViewItem.NodeType nodeType, int nodeId, bool selectAndExpand )
        {
            expandOrColapseAll(false);
            var tvitem =
                TreeViewItems
                .SelectMany(z => z.SubItems)
                .Where(x => x.Id == nodeId)
                .Where(x => x.TreeNodeType == nodeType)
                .FirstOrDefault();

            if (tvitem != null)
            {
                
                tvitem.IsSelected = selectAndExpand;
                //new Action(async () =>
                //{
                //    await Task.Delay(500);
                //}).Invoke();
                tvitem.ParentTreeViewItem.IsExpanded = selectAndExpand;
                return tvitem;
            }

            foreach (NoteTreeViewItem fol in TreeViewItems)
            {

                tvitem = FindInThisTreeViewNode(fol, nodeId, nodeType);
                if (tvitem != null)
                {
                    
                    tvitem.IsSelected = selectAndExpand;

                    //new Action(async () =>
                    //{
                    //    await Task.Delay(500);
                    //}).Invoke();
                    tvitem.ParentTreeViewItem.IsExpanded = selectAndExpand;

                    return tvitem;
                }
            }
            return tvitem;
        }

        public NoteTreeViewItem FindInThisTreeViewNode(NoteTreeViewItem thisNode, int treeViewItemIdToFind, NoteTreeViewItem.NodeType nodeType)
        {
            if (thisNode.Id == treeViewItemIdToFind)
                return thisNode;

            var tvitem =
            thisNode.SubItems
            .Where(x => x.Id == treeViewItemIdToFind)
            .Where(x => x.TreeNodeType == nodeType)
            .FirstOrDefault();

            if (tvitem != null)
                return tvitem;
            foreach (NoteTreeViewItem tvi in thisNode.SubItems)
            {
                NoteTreeViewItem ftvi = null;
                if (tvi.Id == treeViewItemIdToFind && tvi.TreeNodeType == nodeType)
                    return tvi;
                if (tvi.TreeNodeType == NoteTreeViewItem.NodeType.Folder)
                {
                    ftvi = FindInThisTreeViewNode(tvi, treeViewItemIdToFind, nodeType);
                }                
                if (ftvi != null)
                    return ftvi;
            }
            return tvitem;
        }


        private void updateParentItems() {
            foreach (NoteTreeViewItem tvi in TreeViewItems)
            {
                tvi.ParentTreeViewItem = tvi;
                tvi.ParentTreeViewItem.IsExpanded = true;

                updateItems(tvi);
            }
            }

        private void updateItems(NoteTreeViewItem ti) {
            foreach (NoteTreeViewItem si in ti.SubItems)
            {
                si.ParentTreeViewItem = ti;
                ti.ParentTreeViewItem.IsExpanded = true;

                updateItems(si);
            }
        }



        private void expandOrColapseAll(bool expand)
        {
            foreach (NoteTreeViewItem tvi in TreeViewItems)
            {
                tvi.ParentTreeViewItem = tvi;
                tvi.ParentTreeViewItem.IsExpanded = true;

                expandOrColapseAll(tvi,expand);
            }
        }

        private void expandOrColapseAll(NoteTreeViewItem ti, bool expand)
        {
            foreach (NoteTreeViewItem si in ti.SubItems)
            {
                si.ParentTreeViewItem = ti;
                ti.ParentTreeViewItem.IsExpanded = true;

                expandOrColapseAll(si, expand);
            }
        }


        public void LoadFoldersToTreeViewItems(List<Folder> folders)
        {
            this.folderList = folders;


            foreach (Folder fol in folderList)
            {
                var grp = new NoteTreeViewItem().FromNoteItem(fol);
                if (fol.Notes != null)
                    foreach (Note notatka in fol.Notes)
                    {
                        
                        
                        var mainNote = new NoteTreeViewItem().FromNoteItem(notatka);

                        grp.SubItems.Add(
                            mainNote
                            );
                    }

                if (fol.Folders != null)
                    foreach (Folder sfol in fol.Folders)
                    {
                        var sfolNote = enumerateSubFolders(sfol);

                        grp.SubItems.Add(sfolNote);
                    }
                expandOrColapseAll(true);

                TreeViewItems.Add(grp);
            }

            updateParentItems();
        }


        private NoteTreeViewItem enumerateSubFolders(Folder folder)
        {
            var grp = new NoteTreeViewItem().FromNoteItem(folder);

            foreach (Note notatka in folder.Notes)
            {
                var nt = new NoteTreeViewItem().FromNoteItem(notatka);

                //if (notatka.Id == 2) nt.NodeVisibility = Visibility.Collapsed;
                grp.SubItems.Add(nt);
            }

            if (folder.Folders != null)
                foreach (Folder sfol in folder.Folders)
                {
                    var sgrp = enumerateSubFolders(sfol);

                    grp.SubItems.Add(sgrp);
                }
            return grp;
        }



    }
}
