# 🇺🇦 Salo Stocks

**Salo Stocks Tycoon** is a chaotic, patriotic stock market simulation game where salty pork fat meets volatile market events.

Dive into a fictional Eastern European economy filled with exploding pickled tomatoes, viral folk dances, and tractor part shortages. Make bold investments, ride the wave of meme manias, or suffer the wrath of a horilka hangover crash.

## 🧄 Features

- 📈 **Authentic Ukrainian-style Stocks**: Invest in iconic assets like Salo, Vyshyvanka, Horilka, and BorschtCoin.
- 🎉 **Dynamic Events**: Every event — from brine explosions to ghost tours in Chornobyl — affects your portfolio.
- 🧠 **Strategic Gameplay**: Choose patriotism, intuition, or chaos as your trading guide.
- 🛒 **Pixel Art Icons**: Custom 32x32 icons give each stock a distinct identity in the in-game shop.
- 🎲 **Weighted Random Events**: Events have different probabilities, making every playthrough unpredictable.
- 🔧 **Modular Architecture**: Easily add new stocks and events through `stocks.txt` and `events.txt`. Scripted generation respects code regions!

## Stock and Event Code Generator  🐍 Python Automation

A Python script automates the generation of the C# source files `Stocks.cs` and `Events.cs` for the game. It reads stock and event data from simple text files (`stocks.txt` and `events.txt`) and creates corresponding C# classes with predefined properties and initialization methods.

This approach simplifies maintaining and expanding the list of stocks and events — any changes made to the text files are automatically reflected in the game code the next time the script is run, saving time and reducing errors.

### Location of Text Files

The input files are located in the `StockData` directory at the root of the project:

- `stocks.txt` — contains the list of stocks and their base prices.
- `events.txt` — contains the list of events, their descriptions, affected stock, impact percentage, and probability.

### File Format

- `stocks.txt`  
  Each line represents one stock, formatted as:

  StockName,Price

  Example:  
  Salo,100\
  Vyshyvanka,500

- `events.txt`  
    Each line represents one event, formatted as:

    EventName,Description,AffectedStock,ImpactPercent,Probability

    Example:
    Salo Spoilage,Salo was stored wrong and now smells terrible!,Salo,-25,150\
    Vyshyvanka Fashion Trend,Vyshyvanka becomes the new hipster must-have!,Vyshyvanka,40,300

    
### Benefits

This approach simplifies maintaining and expanding the list of stocks and events — any changes made to the text files are automatically reflected in the game code the next time the script is run, saving time and reducing errors.

---

The project is open source — contributions and improvements to the scripts, assets, stock and event data are very welcome!

## Screenshots
### *Early dev concept*

![App Screenshot](https://i.imgur.com/00OfHiy.jpeg)


## Roadmap

- An Actual Roadmap

- Reworked UI

- Interactions with set of NPCs to influence Market

- Unlocking new Stocks like Black Market


## Compatability

- **🟢 Project is being made in Unity 2022.3.48f1**

| Unity Version Range           | Compatibility Level    | Notes                                                                                        |
| ----------------------------- | ---------------------- | -------------------------------------------------------------------------------------------- |
| **2022.3.x (LTS)**            | Guaranteed Compatible  | LTS versions are stable and long-term supported. Your project will run smoothly here.        |
| **2022.x (Non-LTS releases)** | Very Likely Compatible | Minor updates and patches within 2022 should work without issues.                            |
| **2021.3.x (LTS)**            | Possibly Compatible    | Older LTS version, usually compatible but some 2022 features may be missing or cause issues. |
| **2023.x (Tech Releases)**    | Possibly Compatible    | New features or API changes might cause incompatibilities. Testing recommended.              |
| **2020.x or earlier**         | Not Compatible         | Significant API and engine changes since then. Backwards compatibility is limited.           |
