# CoreTiles Server

This is a simple proof-of-concept to show a working cross-platform map tile-server written in .Net Core.
It presents a simple OpenStreetMap map with an hexagon overlay.

The most interesting part of this repo is that it includes a cross-platform drawing logic 
(particularly as `System.Drawing`doesn't include any drawing methods). I've implemented:
- squares
- lines
-- with thickness
-- with antialiasing

Please see my blog post at http://build-failed.blogspot.com for some technical details.

### Running

You'll need to install .NET Core 1.0.

After clonning this git repo the server is executed as:
```sh
$ dotnet restore
$ dotnet run
```

Then just open the browser at: `http://localhost:5000`. You should see something similar to this:

![Alt text](/assets/blog/hexagon1.png?raw=true "Hexagon Tile Server")

### Next steps

Well, most likely none. This was mostly an experiment, although I might reuse some bits and pieces from this.