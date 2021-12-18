﻿using System.Collections.Generic;
using System.Resources;
using EasySave.Command;
using EasySave.Model;
using EasySave.View.Ressources;

namespace EasySave.ViewModel.Save
{
    class ExecuteBackupViewVM
    {
        // Used for navigation between views
        private readonly MainWindowsVM nav = MainWindowsVM.GetThis();

        // Used for traduction 
        public ResourceManager manager = new ResourceManager(typeof(Resource1));

        // Declaration of commands that will be binded in the view (in buttons)
        public RelayCommands Return { get; set; }
        public RelayCommands ExecuteBackup { get; set; }

        // List of backups 
        public List<SaveWork> nameList { get; set; }

        // String for traduction, that will be binded in the view
        public string title { get; set; }
        public string title2 { get; set; }
        public string execute { get; set; }
        public string ReturnButton { get; set; }

        public ExecuteBackupViewVM()
        {
            nameList = new List<SaveWork>();

            // Assignment of values for traduction
            title = Resource1.EBV1;
            title2 = Resource1.EBV2;
            execute = Resource1.EBV3;
            ReturnButton = Resource1.ReturnButton;

            // Command for return button
            Return = new RelayCommands(o =>
            {
                MenuBackupViewVM menu = new MenuBackupViewVM();
                nav.CurrentView = menu;
            });

            // Get all backups
            nameList = CommandsBackup.GetAllBackups();

            // Command for execute button
            ExecuteBackup = new RelayCommands(o =>
            {
                List<SaveWork> selectedWork = new List<SaveWork>();
                foreach (SaveWork save in nameList)
                {
                    if (save.selected)
                    {
                        selectedWork.Add(save);
                    }
                }
                // Execute backup function
                CommandsBackup.ExecuteBackup(selectedWork);

                // Change view when done
                StateBackupViewVM state = new StateBackupViewVM();
                nav.CurrentView = state;
            });
        }
    }
}