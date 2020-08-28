[日本語](README-JP.md) / [English](README.md)

# HoloMagnet4

## Overview

Based on our experience with [HoloMagnet3](https://github.com/feel-physics/HoloMagnet3), this application specializes in sharing the HoloLens experience with multiple users.
Using the sharing function of the older version HoloToolkit, you can easily share the experience with multiple users by using a PC as a server.

![App_Cap](https://user-images.githubusercontent.com/14026964/90363866-5c869580-e09e-11ea-9d55-b729ef38f568.png)
![App_Cap](https://user-images.githubusercontent.com/14026964/90363870-5e505900-e09e-11ea-9359-f9fe283a3c90.png)

\* HoloLens2 is not supported.  
\* Using Unity version 2017.4.x.  

## System Requirements

In addition to the HoloLens to run the app, you need a Windows 10 PC as a server to connect to the same network.
\*  Depending on your LAN's firewall settings, you may not be able to connect properly.

## Launching the Sharing Server

#### Launching via Unity

After launching the UnityEditor, in the top menu select “Mixed Reality Toolkit”, and then select “Sharing Service” > ”Launch Sharing Service” to launch the program for the Sharing Server.

![Launch_Sharing_Service](https://user-images.githubusercontent.com/14026964/90336462-04f11700-e017-11ea-8b19-33a81efb7740.png)

#### Launching via the command line

Launch a command prompt with the below directory: HoloMagnet4/External/HoloToolkit/Sharing/Server.
Execute the following command after launching:
```SharingService.exe -local```

### Check your IP address

When you launch the Sharing Server, the IP address will be displayed below 
```Local IP addresses are:```
![IP_Address](https://user-images.githubusercontent.com/14026964/90336465-06224400-e017-11ea-8777-6c56318bda34.png)

## Setup

Currently, we package with a specific IP address in UnityEditor, install it to HoloLens to make it run.  

### HoloMagnet4

#### Setting up your IP address

Open the project in the Unity editor and open Scene2D in the Project tab in the following directory:
```HoloMagnet4/Assets/HoloMagnet36/Scenes```
\* Using Unity version 2017.4.x.  
![Open_Scene](https://user-images.githubusercontent.com/14026964/90336466-06224400-e017-11ea-96d2-b7479efea62c.png)

In the Hierarchy tab, under "Managers" select "Sharing",then in the Inspector tab under the SharingStage component menu, for the “Server Address” fill in the IP address that you confirmed earlier.
![Setting_IP_Address](https://user-images.githubusercontent.com/14026964/90336467-06bada80-e017-11ea-944f-0da1bb2e19aa.png)

### Build

From the top menu bar, click the “File” menu and select “Build Settings ...” to display the Build Settings window.
 In the “Platform” list, make sure that the Universal Windows Platform is selected and the Unity icon is displayed.
 If you do not see the Unity icon on Universal Windows Platform, select the Universal Windows Platform and press the “Switch Platform” button to switch platforms.

![Build_Settings](https://user-images.githubusercontent.com/14026964/90336468-07537100-e017-11ea-84f2-f908b442bc3a.png)

Once you see the Unity icon on the Universal Windows Platform, press the “Build” button at the bottom of the window and specify the output destination to build.
 For the output destination, create a new folder in the folder that you specified when you opened the project and assign that folder.
(It is recommended to specify a new, dedicated folder in the project folder as the output destination, as it may not be possible to build properly due to the influence of the destination folder or existing files in the folder.)
![Build_Settings_Window](https://user-images.githubusercontent.com/14026964/90336469-07537100-e017-11ea-9337-29ddcc5abf70.png)

Once the build is complete, a file explorer will launch opening the folder you specified as the output destination folder.
 In the output folder, a .sln file will be generated, which you can open in Visual Studio 2017.
(It currently generates a .sln file by default named "181026-HoloMagnet41rt".)

### Deploying to HoloLens

#### Installing to HoloLens directly from Visual Studio

Connect the HoloLens to your PC with a data transferable USB cable.
Change the Visual Studio deployment target to x86, Device.

![VisualStudio_x86](https://user-images.githubusercontent.com/14026964/90338515-17724d00-e025-11ea-8121-4a322dbcd14b.png)
![VisualStudio_Device](https://user-images.githubusercontent.com/14026964/90338516-17724d00-e025-11ea-9b3f-1832a34251e8.png)

Click the play button and select "Device" to install to the device and run debugging. 

If this is your first time transferring using a PC and HoloLens, you will need to enter a PIN code.
![VisualStudio_Pin](https://user-images.githubusercontent.com/14026964/90338519-180ae380-e025-11ea-9093-e95e8c8f9916.png)

In HoloLens, launch the Settings app and select "Update & Security".

![HoloLens_Setting](https://user-images.githubusercontent.com/14026964/90338510-15a88980-e025-11ea-8370-d952b300c2c9.jpg)
Select "For Developers" from the menu on the left, and then AirTap the "Pairing" button under device detection.
![HoloLens_Pairing](https://user-images.githubusercontent.com/14026964/90338513-16d9b680-e025-11ea-94bc-0219b353a819.jpg)

When you see the "Pairing Devices" screen with a 6-digit number, fill in this number to the PIN code entry window in Visual Studio and click the “OK” button.
![HoloLens_Pin](https://user-images.githubusercontent.com/14026964/90338623-e5151f80-e025-11ea-95fc-ec719aa0125d.jpg)

#### Creating package data and installing to HoloLens via Device Portal

On the top menu of Visual Studio, select "Project", then select "Store” > “Create App Package".
![App_Package](https://user-images.githubusercontent.com/14026964/90338520-18a37a00-e025-11ea-82a7-81679fff8d42.png)

In the Create App Packages window, select "I want to create packages for sideloading" and click the “Next” button.
![App_Package_Window](https://user-images.githubusercontent.com/14026964/90338521-193c1080-e025-11ea-927d-f88b20e8f30f.png)

In the "Select the package to create and the solution configuration mappings" table, check only the "x86" checkbox and then click the “Create” button.
![App_Package_Target](https://user-images.githubusercontent.com/14026964/90338522-193c1080-e025-11ea-8a20-c7fc311bb65c.png)

When the package creation is complete, the output destination for the package will be displayed.
 In the output destination folder a ".appxbundle" file, etc will be created. 

Once the package has been created, install to HoloLens via the Device Portal.
Connect the HoloLens to your PC with a USB cable and type in "localhost:10080" to the address bar of your browser. 
Please refer to the [official documentation](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-the-windows-device-portal) for an overview of the Device Portal and setup instructions.

Once you have accessed the Device Portal in HoloLens from your browser, select "Apps" from the menu on the left.
![Device_Portal_Apps](https://user-images.githubusercontent.com/14026964/90345540-64bde100-e05c-11ea-9175-ee0d141f99ad.png)

In the Deploy Apps page, click the "Select File" button and select the ".appxbundle" file that was generated earlier.

If necessary, and when you are installing for the first time, check the "Allow me to select framework packages" checkbox and click the "Next" button. If you checked "Allow me to select framework packages", click the ”Select” button under "Choose any necessary dependencies:" and open "Dependencies” > “x86" folder and select all the ".appx" files in the folder containing the ".appxbundle" file.
![Device_Portal_Apps_Dependencies](https://user-images.githubusercontent.com/14026964/90345542-65ef0e00-e05c-11ea-97cb-872c457e6d64.png)

Lastly, click the "Install" button and install to HoloLens.

\* The screen layout of the Device Portal will vary slightly depending on the HoloLens OS version.

[Reference URL for deployment (official documentation)](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-visual-studio)

## How to operate

### Transition between Scenes

After launching the app in HoloLens, keep facing straight and raise your eyes gradually upward and you will notice buttons.
By aligning the round cursor straight at the button, AirTap to transition between scenes to choose the scene you want to experience.

### Scene Description

#### 2D

By moving the bar magnet up and down and left and right against the compasses spread out vertically and horizontally, you will notice on a flat space with no depth, the position and direction the magnetic force is being affected.
By holding out your hand in front of you pointing your index finger, pinch your thumb and index finger, and move your hand, and you will be able to hold and move the bar magnet in a parallel direction.
The only bar magnet that can be moved is the one with a pink line that is connecting to your position.

#### 3D

In addition to the vertical and horizontal movements, the bar magnets move along the compasses spread in the depth direction, making it possible to see how the magnetic force we saw in the 2D Scene work in 3D.
In this Scene, the bar magnets cannot be moved, but you can watch from various directions how the bar magnet horizontally moves back and forth.

#### Lines of magnetic force

You can display the lines of magnetic force with the bar magnet and see how the magnetic lines behave.
 Since one bar magnet is displayed per person, with multiple people you can display multiple bar magnets and view how each bar magnet affects the lines of magnetic force.
