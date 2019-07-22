# LiteByteCapsule
<p align="center">
<img src="https://github.com/skywarth/LiteByteCapsule/blob/LibraryVersion/temp-logo-small.png">
</p>



![Nuget](https://img.shields.io/nuget/v/LiteByteCapsule.svg) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/04a5655bde6a43b0a37c73d12db5fad5)](https://www.codacy.com/app/skywarth/LiteByteCapsule?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=skywarth/LiteByteCapsule&amp;utm_campaign=Badge_Grade) [![Maintainability](https://api.codeclimate.com/v1/badges/c413df8917e037ec8847/maintainability)](https://codeclimate.com/github/skywarth/LiteByteCapsule/maintainability) [![Build Status](https://dev.azure.com/skywarth/LiteByteCapsule/_apis/build/status/skywarth.LiteByteCapsule?branchName=Lib-including-test)](https://dev.azure.com/skywarth/LiteByteCapsule/_build/latest?definitionId=10&branchName=Lib-including-test) [![Codecov](https://img.shields.io/codecov/c/github/skywarth/LiteByteCapsule.svg)](https://codecov.io/gh/skywarth/LiteByteCapsule) [![CII Best Practices](https://bestpractices.coreinfrastructure.org/projects/2992/badge)](https://bestpractices.coreinfrastructure.org/projects/2992)

  

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

### Or create constant stack by random constants using:
```
Stack<CapsuleConstant> constants=CapsuleConstant.GenerateCapsulationConstants(amount);
```
This will create you a stack of constants which has random values. Since the positioning is dynamic, you won't have to worry about positions of constants in the capsule.


### Create LiteByteCapsule instance, constants variable is a stack of CapsuleConstant instances
```
LiteByteCapsule lite=new LiteByteCapsule(constants);
```

### Convert to syntax (capsulation)
```
byte[] capsule=lite.ConvertToSyntax(innerPackage);
```
Converts the given byte array to capsule using capsule constant stack passed to constructor of LiteByteCapsule.

### Check syntax (decapsulation)
```
byte[] innerPackage=lite.CheckSyntax(capsule);
```
CheckSyntax returns null incase the capsule syntax doesn't match designated capsule constant sequence. Basically null return means imposter package.

### Convert byte array to string
```
string arrayContent=lite.ConvertToString(capsule);
```
