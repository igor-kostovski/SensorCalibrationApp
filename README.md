# SensorCalibrationApp
This project forms practical part of my bachelor thesis ‘Automated car sensor calibration’. I created a desktop app that enables its users to connect to the arbitrary point in LIN network and simulates LIN node behavior, whether it’s a master or a slave node. It’s created using .NET framework, WPF, Entity Framework and SQLite. Peak PLIN-USB device is used for sending LIN frames to the LIN bus, alongside PLIN-API that connects the app to the device. App is created following MVVM architectural pattern and eases testing and configuration of complex LIN systems. 