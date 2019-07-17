# LiteByteCapsule
<p align="center">
<img src="https://github.com/skywarth/LiteByteCapsule/blob/LibraryVersion/temp-logo-small.png">
</p>



![Nuget](https://img.shields.io/nuget/v/LiteByteCapsule.svg) ![GitHub tag (latest by date)](https://img.shields.io/github/tag-date/skywarth/LiteByteCapsule.svg?label=Latest%20Release) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/04a5655bde6a43b0a37c73d12db5fad5)](https://www.codacy.com/app/skywarth/LiteByteCapsule?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=skywarth/LiteByteCapsule&amp;utm_campaign=Badge_Grade) [![Maintainability](https://api.codeclimate.com/v1/badges/c413df8917e037ec8847/maintainability)](https://codeclimate.com/github/skywarth/LiteByteCapsule/maintainability) [![Build Status](https://dev.azure.com/skywarth/LiteByteCapsule/_apis/build/status/skywarth.LiteByteCapsule?branchName=LibraryVersion)](https://dev.azure.com/skywarth/LiteByteCapsule/_build/latest?definitionId=1&branchName=LibraryVersion) [![Licence](https://img.shields.io/github/license/skywarth/Fenrir-wolfpack-simulator.svg)](https://github.com/skywarth/Fenrir-wolfpack-simulator/blob/master/LICENSE)

  

*NuGet Package: https://www.nuget.org/packages/LiteByteCapsule/*

## **To be added later. For now, basic usage:**

### First create a stack of capsulation constants using CapsuleConstant class
```
Stack<CapsuleConstant> constants = new Stack<CapsuleConstant>();
```

### Then push your capsulation constants as you desire

```
constants.Push(new CapsuleConstant(5, 1, true));
constants.Push(new CapsuleConstant(111, 0, false));
constants.Push(new CapsuleConstant(222, 2, true));
constants.Push(new CapsuleConstant(172, 0, true));
constants.Push(new CapsuleConstant(121, 1, false));
constants.Push(new CapsuleConstant(31, 2, false));
constants.Push(new CapsuleConstant(54, 3, false));
```

### CapsuleConstant instances has these properties by order
```
new CapsuleConstant(byteValue,position,startFromTheHead);
```

### Create LiteByteCapsule instance, constants variable is a stack of CapsuleConstant instances
```
LiteByteCapsule lite=new LiteByteCapsule(constants);
```

### Convert to syntax (capsulation)
```
byte[] capsule=lite.ConvertToSyntax(innerPackage);
```

### Check syntax (decapsulation)
```
byte[] innerPackage=lite.CheckSyntax(capsule);
```

### Convert byte array to string
```
string arrayContent=lite.ConvertToString(capsule);
```
