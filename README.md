# **To be added later. For now, basic usage:**

## First create a stack of capsulation constants using CapsuleConstant class
```
Stack<CapsuleConstant> constants = new Stack<CapsuleConstant>();
```

## Then push your capsulation constants as you desire

```
constants.Push(new CapsuleConstant(5, 1, true));
constants.Push(new CapsuleConstant(111, 0, false));
constants.Push(new CapsuleConstant(222, 2, true));
constants.Push(new CapsuleConstant(172, 0, true));
constants.Push(new CapsuleConstant(121, 1, false));
constants.Push(new CapsuleConstant(31, 2, false));
constants.Push(new CapsuleConstant(54, 3, false));
```

## CapsuleConstant instances has these properties by order
```
new CapsuleConstant(byteValue,position,startFromTheHead);
```

## Create LiteByteCapsule instance, constants variable is a stack of CapsuleConstant instances
```
LiteByteCapsule lite=new LiteByteCapsule(constants);
```

## Convert to syntax (capsulation)
```
byte[] capsule=lite.ConvertToSyntax(innerPackage);
```

## Check syntax (decapsulation)
```
byte[] innerPackage=lite.CheckSyntax(capsule);
```

## Convert byte array to string
```
string arrayContent=lite.ConvertToString(capsule);
```
