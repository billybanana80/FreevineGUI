# FreevineGUI
Windows front end GUI for Freevine (created by stabbedbybrick available at GitHub here https://github.com/stabbedbybrick/freevine

This application uses all the underlying Python code created above and will initiate a new Command Prompt window each time your perform an action within Freevine GUI.

Pre-requisites
1. Freevine v1.1.4 and all it's requirements as per https://github.com/stabbedbybrick/freevine
2. Windows .NET 6.0 Desktop Runtime

Installation
The FreevineGUI is a portable executable and does not require to be installed. You can place the Freevine GUI folder in any location on your machine, there is no specific folder location requirement.
When running FreevineGUI for the first tme please set your Freevine folder location ie: that is the Freevine folder from https://github.com/stabbedbybrick/freevine (this is a Mandatory step)
You may also choose to set your Downloads folder location in the Options (this is an optional step)

FreevineGUI v1.1.0 changes
1. added support for TVNZ
2. added proxy configuration in Options (set Hola, Windscribe or Custom proxy). Proxy server locations listed in the Country Name dropdown will populate according to your set proxy option.
3. added set Windscribe proxy credentials
4. added Save File Name command
5. added number of Threads option
6. added additional N-m3u8Dl-RE command option
7. added No Mux, No Mux Subtitles and Shaka Packager options
8. Redesigned UI to move optional items under the 3 main program functions (service, action, process)
9. added Test CDM function
   
Operation
1. Search
   Choose the streaming service in Option 1 and the Search button in Option 2
   Type what your looking for in Option 3 and click Go or hit Enter
2. List Available Titles
   Get the series URL from your search or direct from the service website
   Type/paste into Option 3 and click Go or hit Enter
3. Download Complete Season/Complete Series/Episode/Movie/Subtitle
   Get the series, episode or movie URL
   Type/paste into Option 3 and click Go or hit Enter
4. Complete Command (info only)
   This field will show you the complete string that is passed on to the Command Prompt window. It is for info only so you can see exactly what you have requested.
5. The Freevine button will open a Command Prompt at your specified Freevine folder location
6. The Help button will open a Command Prompt at your specified Freevine folder location and run the Help option
7. The Clear button will remove all selected options and related search/complete command info all at once.
8. The Reset button will perform the same as Clear AND will look for any running Windows Terminal processes on your machie and KILL them (ie: close any Command Prompt windows currently open)
9. Set Service Profile to allow freevine to create a profile with your own Username and Password for a particular service (only required for All4 at this time).
10. Favorites button will display list of your series that you can save as a Favorite. Highlight a row and click Copy to send the series URL to the main window
11. Queue function allows you to queue multiple tasks and run them all at once when you choose.
12. Sites menu will open the chosen streaming service in your default web browser.

Disclaimer

This project is made as a part time hobby. There will be some bugs along the way.
It is designed for educational purposes only and does not condone piracy.

