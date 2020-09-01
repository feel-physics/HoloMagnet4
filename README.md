[日本語](README-JP.md) / [English](README.md)

# HoloMagnet4

## Overview

Based on the experience of [HoloMagnet3](https://github.com/feel-physics/HoloMagnet3), this application specializes in sharing the experience for HoloLens with multiple users.  
Using the sharing function of the old HoloToolkit, you can easily share the experience with multiple users by preparing a PC for the server.  

![App_Cap](https://user-images.githubusercontent.com/14026964/90363866-5c869580-e09e-11ea-9d55-b729ef38f568.png)
![App_Cap](https://user-images.githubusercontent.com/14026964/90363870-5e505900-e09e-11ea-9359-f9fe283a3c90.png)

\* HoloLens2 is not supported.  
\* Using Unity version 2017.4.x.  

## Environment

In addition to the HoloLens for running the app, you need a Windows 10 PC as a server and connect it to the same network.  
\* Depending on your LAN's firewall settings, you may not be able to communicate normally.

## Starting the Sharing server

#### The case of launching via Unity

After launching UnityEditor, in the top menu, select [Mixed Reality Toolkit], and then [Sharing Service] -> [Launch Sharing Service] to launch the program for the Sharing server.  
![Launch_Sharing_Service](https://user-images.githubusercontent.com/14026964/90336462-04f11700-e017-11ea-8b19-33a81efb7740.png)

#### The case of launching via the command line

Launch a command prompt in HoloMagnet4/External/HoloToolkit/Sharing/Server.  
Execute the following command after launching.  
```SharingService.exe -local```

### Check the IP address

When you start the Sharing server, the IP address will be displayed below the description below.  
```Local IP addresses are:```
![IP_Address](https://user-images.githubusercontent.com/14026964/90336465-06224400-e017-11ea-8777-6c56318bda34.png)

## Setup

Currently, we package a specific IP address in UnityEditor, install it in HoloLens, and make it work.  

### HoloMagnet4

#### The setting of IP address

Open the project in the Unity editor and open Scene2D of the following path on the Project tab.  
```HoloMagnet4/Assets/HoloMagnet36/Scenes```
\* Using Unity version 2017.4.x.  
![Open_Scene](https://user-images.githubusercontent.com/14026964/90336466-06224400-e017-11ea-96d2-b7479efea62c.png)

From the Hierarchy tab, select "Sharing" under "Managers",  
after that from the Inspector tab, set the IP address that you confirmed in checking the IP address procedure to "Server Address" in the SharingStage component.  
![Setting_IP_Address](https://user-images.githubusercontent.com/14026964/90336467-06bada80-e017-11ea-944f-0da1bb2e19aa.png)

### Build

In the top menu, click the \[File\] menu and select \[Build Settings ...\] to display the Build Settings window.  
In the Platform item, make sure that the Universal Windows Platform is selected and the Unity icon is displayed.  
If you don't see the Unity icon in the Universal Windows Platform item, select the Universal Windows Platform and press the Switch Platform button to switch platforms.  
![Build_Settings](https://user-images.githubusercontent.com/14026964/90336468-07537100-e017-11ea-84f2-f908b442bc3a.png)

Once you see the Unity icon in the Universal Windows Platform item, press the Build button at the bottom of the window and specify the output destination to build on Unity.  
For the output destination, create a new folder in the folder that you specified when you opened the project and specify that folder.  
(It is recommended to specify a new, dedicated folder in the project folder as the output destination, as it may not be possible to build properly due to the influence of the destination folder or existing files in the folder.)  
![Build_Settings_Window](https://user-images.githubusercontent.com/14026964/90336469-07537100-e017-11ea-9337-29ddcc5abf70.png)

Once the build is complete on Unity, Explorer will be launched with the folder you specified as the output destination folder open.  
As a .sln file will be generated in the output folder, which you can open in Visual Studio 2017.  
(It currently generates a .sln file by default with the name "181026-HoloMagnet41rt".)  

### Deploying to each HoloLens

#### The case of installing to HoloLens directly from Visual Studio

Connect the HoloLens to your PC with a data transferable USB cable.  
Change the Visual Studio deployment target to x86, Device.  
![VisualStudio_x86](https://user-images.githubusercontent.com/14026964/91731726-2b4fae80-ebe2-11ea-9b69-0a8b12e551a5.png)
![VisualStudio_Device](https://user-images.githubusercontent.com/14026964/91731732-2db20880-ebe2-11ea-9cfe-ae371e31b015.png)

Click the button with play mark as "Device" written and install it to the device and debug.  

If this is your transfer it with a first time PC and HoloLens combination, you will need to enter a PIN.  
![VisualStudio_Pin](https://user-images.githubusercontent.com/14026964/91731758-386c9d80-ebe2-11ea-8f58-7ae75e7ad7f6.png)

On HoloLens, launch the Settings app and select the "Updates & Security" item.  
![HoloLens_Setting](https://qiita-user-contents.imgix.net/https%3A%2F%2Fqiita-image-store.s3.amazonaws.com%2F0%2F20418%2F5123d5aa-217b-5059-86fa-8c664d3b17b7.png?ixlib=rb-1.2.2&auto=format&gif-q=60&q=75&s=584eb66fa7fa9a5acf40f48f8dcdc6bb)
Select "For developers" from the menu on the left, and then AirTap the "Pair" button in the device detection item.  
![HoloLens_Pairing](https://qiita-user-contents.imgix.net/https%3A%2F%2Fqiita-image-store.s3.amazonaws.com%2F0%2F20418%2F0106c18b-2962-0603-30e3-790a4a180b7e.png?ixlib=rb-1.2.2&auto=format&gif-q=60&q=75&s=389d72607225a4305460cdbac8d5d796)

When you see "Pair device" and a 6-digit number appears at the bottom, input the same number in the PIN entry window on the Visual Studio side, then click OK.  
![HoloLens_Pin](https://qiita-user-contents.imgix.net/https%3A%2F%2Fqiita-image-store.s3.amazonaws.com%2F0%2F20418%2F134534d1-2cf5-a088-1166-2b4a6da614dd.png?ixlib=rb-1.2.2&auto=format&gif-q=60&q=75&s=5661fc64204db5e95e238290e2480ff0)

#### The case of creating package data and install to HoloLens via Device Portal

In the top menu of Visual Studio, select "Project", "Store" -> "Create App Packages...".  
![App_Package](https://user-images.githubusercontent.com/14026964/91731737-2f7bcc00-ebe2-11ea-970a-02c3abcdc52a.png)

In the Create App Package window, select "I want to create packages for sideloading" and click the Next button.  
![App_Package_Window](https://user-images.githubusercontent.com/14026964/91731762-3acef780-ebe2-11ea-99e0-298e2c81a6e5.png)

In the "Select the packages to create and the solution configuration mappings" item, check only the item with the architecture of "x86" and click the Create button.  
![App_Package_Target](https://user-images.githubusercontent.com/14026964/91731765-3d315180-ebe2-11ea-9ad5-2fbbec4ecd65.png)

When the package creation is complete, the output destination for the package will be displayed.  
".appxbundle" file, etc will be created in the output destination.  

Once the package has been created, install it to HoloLens via the Device Portal.  
Connect the HoloLens to your PC with a USB cable by wire and type "localhost:10080" into your browser's URL.  
See the [official documentation](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-the-windows-device-portal) for an overview of the Device Portal and setup instructions.  

Once you've accessed the Device Portal in HoloLens from your browser, select "Apps" from the menu on the left.  
![Device_Portal_Apps](https://user-images.githubusercontent.com/14026964/90345540-64bde100-e05c-11ea-9175-ee0d141f99ad.png)

In the Deploy Apps item, click the "Select File" button and select the ".appxbundle" file that was generated earlier.  

Check the "Allow me to select framework packages" checkbox and click the "Next" button, if necessary, for example, when installing for the first time.
If you have checked "Allow me to select framework packages", click the ”Select” button in "Choose any necessary dependencies:" and open "Dependencies" -> "x86" folder and select all the ".appx" files in the folder containing the ".appxbundle" file.  
![Device_Portal_Apps_Dependencies](https://user-images.githubusercontent.com/14026964/90345542-65ef0e00-e05c-11ea-97cb-872c457e6d64.png)

Finally, click the "Install" button and install it to HoloLens.  

\* The screen layout of the Device Portal will vary slightly depending on the HoloLens OS version.  

[Reference URL for deployment (official documentation)](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-visual-studio)

## How to operate

### How to transition Scene

After launching the app in HoloLens, keep facing front and raise gradually your point of view to the top, you will see buttons.  
By aligning the round cursor in front of the eye direction with this button and performing AirTap operation, you can transition scenes in order and then switch the contents you can experience.

### Scene Description

#### 2D

By moving the bar magnet up and down and left and right against the compasses spread out vertically and horizontally, you can see the magnetic force working at what position and in what direction on a flat space with no depth.  
By holding your hand in the front direction with your index finger upright, pinch with your thumb and index finger and move your hand, you can pinch the bar magnet to move it in a parallel direction.  
The only bar magnet that can be moved is the one with a pink line connecting it to your position.

#### 3D

In addition to the vertical and horizontal movements, the bar magnets move in the compasses spread in the depth direction, making it possible to see how the magnetic force observed in the 2D Scene works in 3D.  
In this Scene, the bar magnets cannot be moved, but you can watch from various directions that the bar magnets moving from side to side.

#### Lines of magnetic force

You can display lines of magnetic force against a bar magnet and see the lines of the magnetic force.  
Since the one bar magnet is displayed for one person who experiences it, Multiple magnets are displayed through multiple users' experiences, therefore you can watch how each bar magnet affects the lines of magnetic force.
