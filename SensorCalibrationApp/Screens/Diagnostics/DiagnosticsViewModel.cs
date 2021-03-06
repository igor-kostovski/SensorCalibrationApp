﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SensorCalibrationApp.Common;
using SensorCalibrationApp.Common.Enums;
using SensorCalibrationApp.Common.Extensions;
using SensorCalibrationApp.Domain;
using SensorCalibrationApp.Domain.Factories;
using SensorCalibrationApp.Domain.Models;
using SensorCalibrationApp.Domain.Services;
using SensorCalibrationApp.Domain.Services.CommandService;

namespace SensorCalibrationApp.Screens.Diagnostics
{
    class DiagnosticsViewModel : ViewModelBase
    {
        private readonly ICommandService _commandService;
        private readonly IFrameService _frameService;
        private readonly EventManager _eventManager;

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
                Run.RaiseCanExecuteChanged();
                ResBytes = null;
            }
        }

        private byte[] _resBytes;
        public byte[] ResBytes
        {
            get { return _resBytes; }
            set
            {
                _resBytes = value;
                OnPropertyChanged();
            }
        }

        public ValidationNotifier RaiseAfterValidation { get; set; }

        public RelayCommand<string> Select { get; set; }
        public RelayCommand Run { get; set; }

        public DiagnosticsViewModel(ICommandService commandService, EventManager eventManager, IFrameService frameService)
        {
            _commandService = commandService;
            _eventManager = eventManager;
            _frameService = frameService;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            Select = new RelayCommand<string>(OnSelect);
            Run = new RelayCommand(OnRun, CanRun);
            RaiseAfterValidation = Run.RaiseCanExecuteChanged;
        }

        public void Load()
        {
            Task.Run(() =>
            {
                //prevents capturing of response signals sent from sensor that are caused by requests from previous screens
                Thread.Sleep(500);
                _eventManager.PushData += OnNewData;
            });

            Commands = CommandFactory.CreateCommands(Frame);
            SelectFirst();
        }

        private void SelectFirst()
        {
            SelectedCommand = Commands.First();
            SelectedCommand.IsSelected = true;
        }

        public override void Unload()
        {
            _eventManager.PushData -= OnNewData;
            ResBytes = null;
        }

        public void Set(FrameModel frame)
        {
            Frame = frame;
        }

        private void OnNewData(object sender, object resBytes)
        {
            var resBytesVal = resBytes as byte[];
            ResBytes = resBytesVal;
        }

        private void OnSelect(string name)
        {
            SelectedCommand = Commands.SelectByName(name);
        }

        private async void OnRun()
        {
            AddLegacyBytesAndFrameId();

            if (SelectedCommand.Type == CommandType.AssignId)
            {
                await _commandService.UpdateFrameId(Frame);
                await _frameService.Update(Frame);
            }
            else
            {
                await _commandService.ReadById(Frame);
            }
        }

        private void AddLegacyBytesAndFrameId()
        {
            Frame.LegacyCommandBytes = SelectedCommand.Signals.Select(x => x.Value).ToArray();

            if (SelectedCommand.GetFrameId() is byte frameId)
                Frame.FrameId = frameId;
        }

        private bool CanRun()
        {
            if (SelectedCommand == null)
                return false;

            return SelectedCommand.AreSignalsInRange();
        }
    }
}
