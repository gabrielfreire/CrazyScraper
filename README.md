# Crazy Scraper

Command line tool in .NET Core 3.0 C#.

Scrape any website using this tool (only instagram at the moment).

# Installation
1. Download installer [Here](https://stephencabral.com/wp-content/uploads/2011/08/coming-soon.jpg)
2. Install and add the installatin path to `PATH`
# Usage
1. Run this for help and available commands:
    ```
    crs --help
    crs [cmd] --help
    ```
2. Run this to scrape instagram profiles:
    ```
    crs instagram -p <profilename> -p <profilename2> -o <filename.json>
    ```

# Build from source
(The instructions below are a work in progress)
1. `git clone https://github.com/gabrielfreire/CrazyScraper.git`
2. `cd CrazyScraper`
3. `dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true & compile_setup.bat`
4. Or `dotnet run -- instagram -p <profilename1> -p <profilename2>`
# Features
 - Scrape instagram

# TODO
- Better instagram scraping using headless browser
- Scrape Facebook
- Scrape Twitter
- Etc