# Data Converter

Generic Solution for Converting Data (Input ---convert/import--> Process ---convert/export--> Output)

Requires: specific data/object/class (to import to and to export from).

## Running Main Program (Console App)

Console app is built for temporary quick testing.
The app will read the content of `input-text-file.txt` (can be replaced by any other file by changing the file path and name in the argument used when running the app).
The app will then write the output to `output-text-file.txt` (or, if an additional argument is provided when running the app, the additional argument will be used as the output file path and name).

`dotnet run -p text-line-importer-from-text-file/text-line-importer-from-text-file.csproj text-line-importer-from-text-file/input-text-file.txt`

## Travis:

Click: [`Link`](https://travis-ci.org/peter-aryanto/data-converter)
