# KWQuotesApp

General description:
- C#, WPF application
- Prism MVVM framework / Unity dependency injection used - MIT License
- Microsoft.AspNet.WebApi.Client as HTTP client used

Pulling the code of application:
- open Visual Studio
- click "Clone a repository" option in menu at right side of window
- type repository location from this site and choose desired location at you PC
- click “Clone” button

Compiling:
- open project in Visual Studio
- type ctrl+shift+B or click "Build solution" in Build section in menu bar

Running:
- open project in Visual Studio
- click "Start" in Visual Studio or click "F5"

How to use application:
- after opening application you will see a main window 
- type number of KW comments you wish to pull from API (from 5 to 20 available)
- press “Upload” button
- comments will be presented in ListBox
- Now you can:
- see summary - (Click "View summary") - app will count number of positive neutral and negative quotes and find most positive and negative one
- analyse single comment - After selecting one comment in listbox you can click "Analyse selected". Then you will be shown a type and polarity of this comment  

Additional configuration:
- API urls are typed in APP.config file,
- min and max number of quotes to pull are typed in in APP.config file,
- PRISM regions names available as a field of static class KWQuotesApp.Configuration.RegionsNames

