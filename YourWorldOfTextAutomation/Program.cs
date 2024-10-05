using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").Equals("complete"));

            // Hover over the 'Menu' button to open the dropdown
            IWebElement menuButton = driver.FindElement(By.XPath("//span[@id='menu']"));
            Actions action = new Actions(driver);
            action.MoveToElement(menuButton).Perform();  // Simulate hover over the menu button

            // Wait briefly for the dropdown menu to appear
            System.Threading.Thread.Sleep(1000);

            // Use JavaScript to click the checkbox for 'Show coordinates'
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.querySelector('input[type=\"checkbox\"]').click();");

            // Wait for the coordinate input modal to become visible
            System.Threading.Thread.Sleep(100);  // Give time for the modal to load

            // Inject custom CSS and HTML for black arrow buttons, pause button, and GIF handling
            string customScript = @"
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
            <div id='upArrow' class='triangle up' style='transform: translateY(5px); margin-bottom: 3px;'></div>  <!-- Moved down 5px -->
            <div style='display: flex; justify-content: space-between; align-items: center; padding: 5px; margin: 5px 0;'>
                <div id='leftArrow' class='triangle left' style='transform: translateX(-18px);'></div>  <!-- Move 5px to the left -->
                <div id='rightArrow' class='triangle right' style='transform: translateX(19px);'></div>  <!-- Move 5px to the right -->
            </div>
            <div id='downArrow' class='triangle down' style='transform: translateY(-5px); margin-top: 3px;'></div>  <!-- Moved up 5px -->
            <br>
            <button id='pauseBtn' style='background-color:black;color:white;border:none;padding:5px 10px;cursor:pointer;'>Pause</button>
            <div style='text-align:center;color:white;font-family:monospace;margin-top:10px;margin-bottom:5px;'>
                <p>discord: xqyet</p>
                <p>website: xqyet.dev</p>
            </div>
        </div>
    `;
    document.body.appendChild(overlay);

    // Add CSS for the smaller black arrow buttons and layout
    var style = document.createElement('style');
    style.innerHTML = `
        .triangle {
            width: 0;
            height: 0;
            border-left: 15px solid transparent;
            border-right: 15px solid transparent;
            cursor: pointer;
            transition: transform 0.2s ease, filter 0.2s ease;
        }
        .up {
            border-bottom: 30px solid black;
        }
        .down {
            border-top: 30px solid black;
        }
        .left {
            border-top: 15px solid transparent;
            border-bottom: 15px solid transparent;
            border-right: 30px solid black;
        }
        .right {
            border-top: 15px solid transparent;
            border-bottom: 15px solid transparent;
            border-left: 30px solid black;
        }
        .triangle:hover {
            transform: scale(1.1);  // Scale up on hover
            filter: brightness(1.3);  // Highlight effect
        }
        .triangle:active {
            transform: scale(0.95);  // 3D clicking effect
        }
        button:hover {
            filter: brightness(1.3);  // Pause button hover highlight
        }
        button:active {
            transform: translateY(2px);  // Pause button clicking effect
        }
        #gifOverlay {
            position: fixed;
            bottom: 0px;  // Flush with the bottom
            left: 0px;    // Flush with the left
            z-index: 99999;
            width: 150px;
            height: 150px;
        }
    `;
    document.head.appendChild(style);

    // Add event listeners for the triangle buttons
    document.getElementById('upArrow').addEventListener('click', function() {
        window.scrollUpStart = true;
        window.scrollDownStart = false;
        window.scrollLeftStart = false;
        window.scrollRightStart = false;
    });
    document.getElementById('downArrow').addEventListener('click', function() {
        window.scrollDownStart = true;
        window.scrollUpStart = false;
        window.scrollLeftStart = false;
        window.scrollRightStart = false;
    });
    document.getElementById('leftArrow').addEventListener('click', function() {
        window.scrollRightStart = true;  // Inverted: Left arrow now scrolls right
        window.scrollDownStart = false;
        window.scrollUpStart = false;
        window.scrollLeftStart = false;
    });
    document.getElementById('rightArrow').addEventListener('click', function() {
        window.scrollLeftStart = true;  // Inverted: Right arrow now scrolls left
        window.scrollRightStart = false;
        window.scrollDownStart = false;
        window.scrollUpStart = false;
    });

    // Add event listener for the pause button
    document.getElementById('pauseBtn').addEventListener('click', function() {
        for (let i = 0; i < 3; i++) {
            window.scrollDownStart = false;
            window.scrollUpStart = false;
            window.scrollLeftStart = false;
            window.scrollRightStart = false;
        }
    });

    window.scrollDownStart = false;
    window.scrollUpStart = false;
    window.scrollLeftStart = false;
    window.scrollRightStart = false;

    // Inject the GIF using the Imgur URL
    var gif = document.createElement('img');
    gif.src = 'https://i.imgur.com/o6Awr2B.gif';  // Use the correct direct GIF URL
    gif.id = 'gifOverlay';
    document.body.appendChild(gif);
";

            // Execute the script to add the arrows, pause button, and GIF
            js.ExecuteScript(customScript);


            // Locate the canvas element (id="yourworld")
            IWebElement canvasElement = driver.FindElement(By.Id("yourworld"));

            // Move steps for all directions
            int moveStepDown = -390;
            int moveStepUp = 390;
            int moveStepRight = 390;  // Corrected: positive moves right
            int moveStepLeft = -390;  // Corrected: negative moves left

            // Infinite loop to handle all directions
            while (true)
            {
                try
                {
                    bool isScrollingDown = (bool)js.ExecuteScript("return window.scrollDownStart;");
                    bool isScrollingUp = (bool)js.ExecuteScript("return window.scrollUpStart;");
                    bool isScrollingRight = (bool)js.ExecuteScript("return window.scrollRightStart;");
                    bool isScrollingLeft = (bool)js.ExecuteScript("return window.scrollLeftStart;");

                    if (isScrollingDown)
                    {
                        action = new Actions(driver);
                        action.ClickAndHold(canvasElement)
                              .MoveByOffset(0, moveStepDown)
                              .Release()
                              .Perform();
                        Thread.Sleep(50);
                    }

                    if (isScrollingUp)
                    {
                        action = new Actions(driver);
                        action.ClickAndHold(canvasElement)
                              .MoveByOffset(0, moveStepUp)
                              .Release()
                              .Perform();
                        Thread.Sleep(50);
                    }

                    if (isScrollingRight)
                    {
                        action = new Actions(driver);
                        action.ClickAndHold(canvasElement)
                              .MoveByOffset(moveStepRight, 0)  // Move right
                              .Release()
                              .Perform();
                        Thread.Sleep(50);
                    }

                    if (isScrollingLeft)
                    {
                        action = new Actions(driver);
                        action.ClickAndHold(canvasElement)
                              .MoveByOffset(moveStepLeft, 0)  // Move left
                              .Release()
                              .Perform();
                        Thread.Sleep(50);
                    }
                }
                catch (MoveTargetOutOfBoundsException)
                {
                    Console.WriteLine("Out of bounds, stopping movement.");
                    break;
                }

                // Break loop with 'Q' key
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    break;
                }
            }

            Console.WriteLine("Scrolling operation completed. Press Enter to close the program and browser.");
            Console.ReadLine();
            driver.Quit();
        }
    }
}
