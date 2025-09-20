using Migration.Console;

var exporter = new LegacyExporter();
exporter.ExportSocks();
exporter.ExportStock();

// This creates socks_catalog.csv and stock_inventory.xlsx in the bin folder
// Which can then be used by our migration code to inject in our new system.
