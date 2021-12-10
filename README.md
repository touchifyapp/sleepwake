# sleepwake

`sleepwake` is a tool that allows to automatically wake your system from sleep (S3) or hibernate (S4) mode. It leverages Windows [System Wake-up Events](https://docs.microsoft.com/en-us/windows/win32/power/system-wake-up-events) to create a `Waitable Timer` that will wake up the system after a given period of time.

## Installation

This program is available to download on Github: [Download here](https://github.com/touchifyapp/sleepwake/releases/latest).

## Usage

```bash
# Put system in sleep mode and wake in 10 minutes
sleepwake.exe -w 00:10 -s

# Put system in hibernate mode and wake in 1 hour
sleepwake.exe -w 01:00 -h
```

## Options

| Option | Description |
|--------|-------------|
| `--wake`, `-w` | **Required.** Set the time to wait before waking up the system. |
| `--sleep`, `-s` | Put the system in sleep (S3) mode. |
| `--hibernate`, `-h` | Put the system in hibernate (S4) mode. |


## Compatibility

| Operating System | Status |
|------------------|--------|
| Windows 7        | ❓     |
| Windows 8        | ❓     |
| Windows 10       | ✔️     |
| Windows 11       | ✔️     |

```
❓ Compatible (not tested)
✔️ Compatible (tested)
```

## Contributing

### Prerequistes

- .NET 6.0

### Prepare the project

```bash
# Clone the repository
git clone https://github.com/touchifyapp/sleepwake

# Initialize the project
cd sleepwake
dotnet restore
```

### Common tasks

```bash
# Build the project
dotnet build

# Create a release
dotnet publish -r win-x64 -c Release --self-contained
```

## License

[MIT](LICENSE)