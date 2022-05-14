# Topdown Arena Shooter

This is the source for a topdown arena shooter a couple of friends and I worked on for a bit. We wanted to experiment with the lower level 
components of an engine, so we used MonoGame, and built a lot of feature from the ground up.

## Prerequisites
To build and run the project, you'll need to install the following prequisites:
* [dotnet SDK](https://github.com/dotnet/sdk)
* [MonoGame Content Builder](https://docs.monogame.net/articles/tools/mgcb.html) - to install run `dotnet tool install -g dotnet-mgcb` 

## Building
To build the content file, run the following command:
```
mgcb .\TopDownShooter\Content\Content.mgcb
```
The built .\Content folder will need to be copied to the output folder after building in Visual Studio.

## Contributing

Feel free to create issues, or submit a PR.

## License

MIT © Jason Shands
