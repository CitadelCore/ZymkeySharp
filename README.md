# ZymkeySharp

ZymkeySharp is a library you can use to perform operations with your Zymkey hardware security module.

Example usage:

```csharp
using (var zymkey = new Zymkey()) {
    Console.WriteLine(zymkey.GetTime());
    Console.WriteLine(BitConverter.ToString(zymkey.GetRandomBytes(32)));
        
    var locked = zymkey.LockData(Encoding.UTF8.GetBytes("Hello World!"));
        
    Console.WriteLine($"Locked bytes: {BitConverter.ToString(locked)}");
    var unlocked = zymkey.UnlockData(locked);

    Console.WriteLine($"Unlocked bytes: {BitConverter.ToString(unlocked)}");
    Console.WriteLine($"Unlocked string: {Encoding.UTF8.GetString(unlocked)}");

    Console.WriteLine(zymkey.WaitForTap(10000));
}
```