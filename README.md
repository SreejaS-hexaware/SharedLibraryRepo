
# RedisCacheLibrary

This is a dotnet library for building a redis connection and dealing with the basic Redis operations like GET, SET and REMOVE.




## Installation

Use the command  [ dotnet add package](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-add-package) to install RedisCacheLibrary

```bash
  dotnet add package RedisCacheLibrary
```
    
## Usage

```javascript
using RedisCacheLibrary

# Make Redis Connection
<redisCacheServiceName>.EnsureConnection(<connectionString>);

# Set a cache value
await <redisCacheServiceName>.SetAsync(<key>, <value>, <timespan>);

# Get a cache value
var <variableName> = await <redisCacheServiceName>.GetAsync<string>(<key>);

# Remove a cache value
<redisCacheServiceName>.Remove(<key>);
```


## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License

[MIT](https://choosealicense.com/licenses/mit/)

