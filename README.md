# movie-challenge
Sample application to solve a simple mock business task. See below for setup, usage instructions, and design overview.

## Getting Started
movie-challenge requires only .Net Core and a text editor for you to get started (and a valid config and input file to run successfully).

### .Net Core
Download the .Net Core sdk here https://dotnet.microsoft.com/download

movie-challenge was developed with .Net Core 2.1

### Config
movie-challenge uses ```appsettings.json``` for configuration settings. It must match the same JSON format as the sample provided.

Open/Close times must be provided for every day of the week in HH:MM 24-hour format.

SetupTimes provides configuration for time required to setup venue after opening and to clean individual theaters between showings.

InputFileHeaders relates to the first line of the input file. This configuration serves to match the data columns with data fields. The order here need not match the input file header order (but the input file header order must match the input file data order, obviously).

### Input file
movie-challenge relies on a txt file for data input. It should be passed on the command line as the first argument. 
A sample input file is provided as ```input.txt```, copied below.

```
Movie Title, Release Year, MPAA Rating, Run Time
There's Something About Mary, 1998, R, 2:14
How to Lose a Guy in 10 Days, 2003, PG-13, 1:56
Knocked Up, 2007, R, 2:08
The Wedding Singer, 1998, PG-13, 1:36
High Fidelity, 2000, R, 1:54
Sleepless in Seattle, 1993, PG, 1:46
The Proposal, 2009, PG-13, 1:48
```

## Tests
Unit tests found in ```MoviesChallenge.Test``` can be run using the command ```dotnet test``` from that directory.

## Usage
movie-challenge can be run via the command ```dotnet run input.txt``` where ```input.txt``` is the input file.

The output will be printed to the console, as requested. 

## Design 
