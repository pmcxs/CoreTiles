# CoreTiles Server

This is a simple proof-of-concept to show a working cross-platform map tile-server written in .Net Core.
It presents a simple OpenStreetMap map with an hexagon overlay.

Initially, as there wasn't any lib on .NET Core that supported drawing, I built my custom logic (still included on the repo and explained at http://build-failed.blogspot.com/2016/08/creating-simple-tileserver-with-net.html). Current, as https://github.com/SixLabors/ImageSharp now includes native drawing support, I'm using it for my drawing needs.

### Running

You'll need to install .NET Core 2.1

After clonning this git repo the server is executed as:
```sh
$ cd src
$ cd CoreTiles.Server
$ dotnet restore
$ dotnet run
```

Then just open the browser at: `http://localhost:5000`. You should see something similar to this:

![Alt text](/assets/blog/hexagon1.png?raw=true "Hexagon Tile Server")

### Next steps

Well, probably none. This was mostly an experiment, although I might reuse some bits and pieces from this in the future.
