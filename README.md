# ime-state

A tiny Windows CLI tool to get or set the IME state of the foreground window.

## Usage

| Command                   | Description                                   |
| :------------------------ | :-------------------------------------------- |
| `ime-state.exe`           | Prints the current IME state (`ON` or `OFF`). |
| `ime-state.exe on` / `1`  | Turns the IME on.                             |
| `ime-state.exe off` / `0` | Turns the IME off.                            |

## Sequence Diagrams

- Get IME State: `ime-state.exe`

```mermaid
sequenceDiagram
    participant User as User
    participant Program as Program.Main
    participant User32 as user32.dll
    participant Imm32 as imm32.dll
    participant IME as IME Window

    User->>Program: ime-state.exe (no arguments)

    Program->>User32: GetForegroundWindow()
    User32-->>Program: foregroundWnd

    alt foregroundWnd == IntPtr.Zero
        Program-->>User: Exit
    end

    Program->>Imm32: ImmGetDefaultIMEWnd(foregroundWnd)
    Imm32-->>Program: imeWnd

    alt imeWnd == IntPtr.Zero
        Program-->>User: Exit
    end

    Program->>IME: SendMessage(imeWnd,<br/>WM_IME_CONTROL,<br/>IMC_GETOPENSTATUS, 0)
    IME-->>Program: status (0 or ≠0)

    Program->>User: Console.WriteLine("ON" or "OFF")
```

- Set IME State: `ime-state.exe on/1 or off/0`

```mermaid
sequenceDiagram
    participant User as User
    participant Program as Program.Main
    participant User32 as user32.dll
    participant Imm32 as imm32.dll
    participant IME as IME Window

    User->>Program: ime-state.exe on/1 or off/0

    Program->>User32: GetForegroundWindow()
    User32-->>Program: foregroundWnd

    alt foregroundWnd == IntPtr.Zero
        Program-->>User: Exit
    end

    Program->>Imm32: ImmGetDefaultIMEWnd(foregroundWnd)
    Imm32-->>Program: imeWnd

    alt imeWnd == IntPtr.Zero
        Program-->>User: Exit
    end

    alt Argument is "on" or "1"
        Program->>IME: SendMessage(imeWnd,<br/>WM_IME_CONTROL,<br/>IMC_SETOPENSTATUS, 1)
    else Argument is "off" or "0"
        Program->>IME: SendMessage(imeWnd,<br/>WM_IME_CONTROL,<br/>IMC_SETOPENSTATUS, 0)
    end

    Note over Program,IME: Exit without using the return value
```

## Build & Release

- **Build:** Run `build.bat` to output `ime-state.exe` to the `bin/` directory.
- **Release:** Run `release.bat` to package the executable and `LICENSE` into `release/ime-state.zip`.

## Requirements

- Windows
- .NET Framework 4.0 or later

## License

See the [LICENSE](LICENSE) file.
