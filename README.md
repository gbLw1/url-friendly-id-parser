# URL friendly base64 encoded ID (Guid)

Two way conversion between a GUID and a URL friendly base64 encoded ID **using 0 memory allocation.**

## Use case

When you want to use a GUID as a URL parameter and don't want to expose the GUID in the URL.

By doing that, your API/DB can process the request without exposing the GUID.

## How to use

[`Program.cs`](src/Program.cs) contains the code to **Run** the conversion and **Benchmark** it.

Just uncomment the code you want to run and follow the instructions in the comments.
