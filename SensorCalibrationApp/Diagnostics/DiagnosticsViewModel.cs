﻿using System.Collections.ObjectModel;
using System.Linq;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.Diagnostics
{
    class DiagnosticsViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly EventManager _eventManager;
        private readonly IFrameService _frameService;

        private readonly object[] _initialRes = { -1, -1, -1, -1, -1, -1, -1, -1 };

        public DiagnosticsViewModel(ICommandService commandService, EventManager eventManager, IFrameService frameService)
        {
            _commandService = commandService;
            _eventManager = eventManager;
            _frameService = frameService;

            InitializeCommands();

            ResBytes = _initialRes;
        }

        private void InitializeCommands()
        {
            Select = new RelayCommand<string>(OnSelect);
        }

        private FrameModel _frame;
        public FrameModel Frame
        {
            get { return _frame;}
            set
            {
                _frame = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Command> _commands;
        public ObservableCollection<Command> Commands
        {
            get { return _commands; }
            set
            {
                _commands = value;
                OnPropertyChanged();
            }
        }

        private Command _selectedCommand;
        public Command SelectedCommand
        {
            get { return _selectedCommand;}
            set
            {
                _selectedCommand = value;
                OnPropertyChanged();
            }
        }

        private object[] _resBytes;
        public object[] ResBytes
        {
            get { return _resBytes; }
            set
            {
                _resBytes = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand<string> Select { get; set; }

        public void Load()
        {
            _eventManager.PushData += OnNewData;
            Commands = CommandsFactory.CreateCommands(Frame.FrameId);

            SelectedCommand = Commands.First();
            Commands.First().IsSelected = true;
        }

        public override void Unload()
        {
            _eventManager.PushData -= OnNewData;
        }

        public void Set(FrameModel frame)
        {
            Frame = frame;
        }

        private void OnNewData(object sender, string e)
        {
            throw new System.NotImplementedException();
        }

        private void OnSelect(string name)
        {
            var selectedCommands = Commands.Where(x => x.Name != name && x.IsSelected).ToList();
            if (selectedCommands.Any())
                selectedCommands.ForEach(x => x.IsSelected = false);

            SelectedCommand = Commands.SingleOrDefault(x => x.IsSelected);
            ResBytes = _initialRes;
        }
    }
}
