import os
import re

# === Pfade ===
base_dir = os.path.abspath(os.path.join(os.path.dirname(__file__), ".."))
stockdata_dir = os.path.join(base_dir, "StockData")
scripts_dir = os.path.join(base_dir, "Scripts")

stocks_txt = os.path.join(stockdata_dir, "stocks.txt")
events_txt = os.path.join(stockdata_dir, "events.txt")

stocks_cs = os.path.join(scripts_dir, "Stocks.cs")
events_cs = os.path.join(scripts_dir, "Events.cs")

# Hilfsfunktion: CamelCase-Name in lesbaren Namen mit Leerzeichen wandeln
def camel_case_to_spaces(name):
    result = []
    for i, c in enumerate(name):
        if i > 0 and c.isupper() and (name[i-1].islower() or (i+1 < len(name) and name[i+1].islower())):
            result.append(" ")
        result.append(c)
    return "".join(result)

# === Datei-Parser ===
def read_txt_entries(path):
    with open(path, "r", encoding="utf-8") as f:
        return [line.strip().split(",") for line in f if line.strip()]

stock_entries = read_txt_entries(stocks_txt)
event_entries = read_txt_entries(events_txt)

# === Bereichswechsler ===
def replace_region(content, region_name, generated_code):
    pattern = (
        rf"(#region {re.escape(region_name)}\n)(.*?)(\n[ \t]*#endregion)"
    )
    replacement = rf"\1{generated_code}\3"
    return re.sub(pattern, replacement, content, flags=re.DOTALL)


# === Stocks.cs generieren ===
with open(stocks_cs, "r", encoding="utf-8") as f:
    original_stocks_code = f.read()

stock_props = "\n".join([
    f'    public static Stock {name} => _registry["{name}"];'
    for name, _ in stock_entries
])

stock_init = "\n".join([
    f'        _registry["{name}"] = new Stock("{camel_case_to_spaces(name)}", {price}f);'
    for name, price in stock_entries
])

new_stocks_code = replace_region(original_stocks_code, "CREATING_STATIC_STOCKS", stock_props)
new_stocks_code = replace_region(new_stocks_code, "STOCKREGISTRY", stock_init)

with open(stocks_cs, "w", encoding="utf-8") as f:
    f.write(new_stocks_code)

# === Events.cs generieren ===
with open(events_cs, "r", encoding="utf-8") as f:
    original_events_code = f.read()

event_props = "\n".join([
    f'    public static Event {entry[0].replace(" ", "")} {{ get; private set; }}'
    for entry in event_entries
])

event_inits = ""
for entry in event_entries:
    name, desc, stock, impact, prob = entry
    var_name = name.replace(" ", "")
    event_inits += f'        {var_name} = new Event("{name}", "{desc}", Stocks.{stock}, {impact}f, {prob});\n'
    event_inits += f'        eventList.Add({var_name});\n\n'

new_events_code = replace_region(original_events_code, "CREATING_EVENTS", event_props)
new_events_code = replace_region(new_events_code, "INIT_EVENTS", event_inits.strip())

with open(events_cs, "w", encoding="utf-8") as f:
    f.write(new_events_code)

print("✅ Dateien aktualisiert, nur in den #region-Bereichen.")
