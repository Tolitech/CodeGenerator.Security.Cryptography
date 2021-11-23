# Tolitech.CodeGenerator.Security.Cryptography
Cryptography library used in projects created by the Code Generator tool.

This project implements and makes available public methods (wrap) for cryptography with Hash algorithm and Advanced Encryption Standard (AES), facilitating its use within projects. 

Tolitech Code Generator Tool: [http://www.tolitech.com.br](https://www.tolitech.com.br/)

Examples:

```
string key = AesCryptography.GenerateKey();
string iv = AesCryptography.GenerateIV();
```

```
string encrypted = AesCryptography.Encrypt("plainText", key, iv);
string decrypted = AesCryptography.Decrypt(textEncrypted, key, iv);
```

```
var bytes = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "image.jpeg"));
var encrypted = AesCryptography.Encrypt(bytes, key, iv);
var decrypted = AesCryptography.Decrypt(encrypted, key, iv);
```