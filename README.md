# YourWorldOfText Automation Project

This project automates the scrolling behavior on [Your World of Text](https://www.yourworldoftext.com/) using Selenium WebDriver in C#.

## Feature

- **Directional Arrows (Up, Down, Left, Right)**: Control the scrolling direction with on-screen arrows.

## Prerequisites

To run this project, ensure that you have the following installed on your system:

1. **Visual Studio** (2022 or later) with .NET Core
2. **Chrome** (latest version) and **ChromeDriver** (version matching your Chrome)
3. **NuGet Packages**:
    - Selenium WebDriver
    - Selenium WebDriver.ChromeDriver
    - WebDriver.Support
   
[![Video Thumbnail](https://img.youtube.com/vi/lNyk812-kkM/0.jpg)](https://www.youtube.com/watch?v=lNyk812-kkM)

### Installing the Prerequisites

1. **Chrome and ChromeDriver**:
    - Install the latest version of Chrome from [Google Chrome](https://www.google.com/chrome/).
    - Download the matching version of ChromeDriver from [ChromeDriver Downloads](https://sites.google.com/chromium.org/driver/).

2. **Install Selenium NuGet Packages**:
    - Open Visual Studio and navigate to your project.
    - Go to **Tools > NuGet Package Manager > Manage NuGet Packages for Solution**.
    - Search for and install the following packages:
        - **Selenium.WebDriver**
        - **Selenium.WebDriver.ChromeDriver**
        - **Selenium.Support**

  
### Steps to Run

1. **Clone the repository** or copy the project files to your local machine.

2. **Set up Visual Studio**:
    - Open Visual Studio, and load the project.
    - Restore the NuGet packages by going to **Tools > NuGet Package Manager > Restore NuGet Packages**.

3. **Ensure ChromeDriver is installed**:
    - Verify that the ChromeDriver version matches your installed Chrome version.
    - ChromeDriver can be placed in the project’s `bin/Debug/` directory or added to your system's PATH.

4. **Add the GIF file**:
    - Place your GIF (`anime-cora.gif`) in the project folder.
    - Ensure that its file path is correctly referenced in the C# code or use a URL as appropriate.

5. **Run the Project**:
    - Press **F5** in Visual Studio or click on **Run** to start the project.

6. **Control the Scroll**:
    - Use the on-screen arrow buttons to scroll in different directions.
    - Use the pause button to stop the scrolling.

### Key Components in `Program.cs`

- **WebDriver Initialization**: Sets up ChromeDriver to automate the Chrome browser.
- **JavaScript Injection**: Dynamically injects the custom CSS/HTML for the scrolling buttons and GIF overlay.
- **Scroll Logic**: Handles up, down, left, right scrolling, and implements smooth scrolling with `Actions`.

### Program.cs Example

```csharp
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace YourWorldOfTextAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up ChromeDriver
            IWebDriver driver = new ChromeDriver();

            // Set window size to force scrolling
            driver.Manage().Window.Size = new System.Drawing.Size(1200, 800);

            // Navigate to Your World of Text
            driver.Navigate().GoToUrl("https://www.yourworldoftext.com/");

            // Wait for the page to fully load
            Thread.Sleep(2000);  // Adjust for page load

            // Inject JavaScript for directional buttons and GIF overlay
            string script = @"
                var overlay = document.createElement('div');
                overlay.style.position = 'fixed';
                overlay.style.top = '10px';
                overlay.style.right = '10px';
                overlay.style.backgroundColor = 'rgba(0, 0, 0, 0.5)';
                overlay.style.padding = '10px';
                overlay.style.zIndex = '9999';
                overlay.style.width = '120px';
                overlay.style.height = '250px';
                overlay.style.textAlign = 'center';
                overlay.innerHTML = `
                    <div style='display:flex; justify-content:center; align-items:center; flex-direction:column;'>
                        <div id='upArrow' class='triangle up'></div>
                        <div style='display: flex; justify-content: space-between; align-items: center; padding: 5px; margin: 5px 0;'>
                            <div id='leftArrow' class='triangle left'></div>
                            <div id='rightArrow' class='triangle right'></div>
                        </div>
                        <div id='downArrow' class='triangle down'></div>
                        <button id='pauseBtn'>Pause</button>
                    </div>`;
                document.body.appendChild(overlay);

                // CSS for triangles and pause button
                var style = document.createElement('style');
                style.innerHTML = `
                    .triangle {
                        width: 0;
                        height: 0;
                        border-left: 15px solid transparent;
                        border-right: 15px solid transparent;
                        cursor: pointer;
                    }
                    .up {
                        border-bottom: 30px solid black;
                    }
                    .down {
                        border-top: 30px solid black;
                    }
                    .left {
                        border-right: 30px solid black;
                    }
                    .right {
                        border-left: 30px solid black;
                    }
                    .triangle:hover {
                        filter: brightness(1.2);
                    }
                    button:hover {
                        background-color: grey;
                    }`;
                document.head.appendChild(style);
            ";
            ((IJavaScriptExecutor)driver).ExecuteScript(script);

            // Scroll logic with directional controls
            Actions action = new Actions(driver);
            while (true)
            {
                // Logic for moving based on scroll flags set in JS
            }
        }
    }
}



