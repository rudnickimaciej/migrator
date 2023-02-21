# MigratoR
Database migration tool for C# types

## What it does
MigratoR is a code-first migration tool that analyses entities defined in C# and generates SQL Scripts (at this moment only SQL Server is supported) that create or updates tables' definitions on database. 
The tool compares version of schemas and updates existing tables based on changes applied to the types. 



## How to install and test 


## How it works 


The sequence of operation conducted by the tool is as follows: 
1. Loading Entity types to program memory
2. Converting Entity types to common format (TModel) 	
3. Loading current database schemas and converting them to common format (TModel) 
4. Comparizon of current schemas with newest versions of types 
5. SQL scripts generation and applying it to database 
![image](https://user-images.githubusercontent.com/42208564/220399234-46651570-4d43-494f-984f-acdec96dae0d.png)
