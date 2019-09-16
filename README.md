# LiteByteCapsule
<p align="center">
<img src="https://github.com/skywarth/LiteByteCapsule/blob/LibraryVersion/temp-logo-small.png">
</p>



![Nuget](https://img.shields.io/nuget/v/LiteByteCapsule.svg) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/04a5655bde6a43b0a37c73d12db5fad5)](https://www.codacy.com/app/skywarth/LiteByteCapsule?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=skywarth/LiteByteCapsule&amp;utm_campaign=Badge_Grade) [![Maintainability](https://api.codeclimate.com/v1/badges/c413df8917e037ec8847/maintainability)](https://codeclimate.com/github/skywarth/LiteByteCapsule/maintainability) [![Build Status](https://dev.azure.com/skywarth/LiteByteCapsule/_apis/build/status/skywarth.LiteByteCapsule?branchName=Lib-including-test)](https://dev.azure.com/skywarth/LiteByteCapsule/_build/latest?definitionId=10&branchName=Lib-including-test) [![Codecov](https://img.shields.io/codecov/c/github/skywarth/LiteByteCapsule.svg)](https://codecov.io/gh/skywarth/LiteByteCapsule) [![CII Best Practices](https://bestpractices.coreinfrastructure.org/projects/2992/badge)](https://bestpractices.coreinfrastructure.org/projects/2992)


*NuGet Package: https://www.nuget.org/packages/LiteByteCapsule/*

LiteByteCapsule is a solution/library to encapsulate your byte array packages before sending them over certain protocols (TCP-IP, Socket, API, etc.)


## Table Of Contents
1. Introduction
    1. [But Why ?](#but-why)
    2. [How ?](#how)
2. Installation
3. Usage
    1. [About capsulation constants](#constants)
    2. [LiteByteCapsule instance](#instance)
    3. [Constructors](#constructors)
    4. [Encapsulating a package](#encapsulation)
    5. [Exporting encapsulation constants](#exporting)
    6. [De-encapsulate(validate) a package](#decapsulation)
    7. [Instance utilities/helpers](#instance)
    8. [Static utilities/helpers](#static)
4. [Tests](#tests)
5. [Licence](#licence)
6. [Old documentation](#old)
    



## Introduction
### <a name="but-why"></a> But Why ?



When you send your byte arrays on WebSockets (e.g) a connection between sender and receiver is created. This connection is quiet vulnerable to [MitM] [mitm] attacks if you don't use SSL/HTTPS to encrypt the communication. 
 
 Even if you use SSL/HTTPS encryption, you are still vulnerable to denial-of-service [(DDoS)] [ddos] attacks on listening port. For e.g: what if somebody floods your listening port on WebSocket with byte arrays with length of 100.000, non-stop. Even if your listening port is async, soon it'll be flooded with noise data. 
 
### <a name="how"></a>How ?

#### For sender(client) side
LiteByteCapsule encapsulates your package by adding constants(byte values) at the start and end of your original byte array. In other words, your actual package is contained in between series of byte values. That way, if malicious intended person tries to sniff your by packages they won't have an idea where does the actual package start and end. 
  
#### For receiver(server) side
Instead of reading all the incoming data/transmission to listening port, use LiteByteCapsule's CheckSyntax() method to drop packages rather fast. It has several measures to prevent reading all the data and stops reading package as soon as one of the measures in syntax doesn't fit. With that, even if somebody sends packages with millions of size, it will be discarded in milliseconds without going through all the package.

## Installation
Simply initiate this command from your package manager console:

`Install-Package LiteByteCapsule -Version 1.1.6`

Or grab it through NuGet manager by searching "LiteByteCapsule".

# Usage
![alt text][diagram]






### <a name="constants"></a> About encapsulation constants
Both encapsulation and decapsulation actions require encapsulation constants to be present. Encapsulation constants are a variable type that is defined in the package **(CapsuleConstants)**. 




#### CapsuleConstants Class
___
CapsuleConstants class consists:

| Name       |Type           |Cool  |
| ------------- :|:-------------:|:-----|
| val     | byte|Value property of an CapsuleConstant instance. Byte value for constant.|
| position     | int      |Position of the constant related to the start position parameter(head). E.g: if position is:0 and head=true, this constant will be the first element in the capsule.|
| head | bool     |Property to indicate counting for position from start(head) or counting from the end(tail) of the capsule. E.g:Head:false, position:0 will be the last element of the capsule.|

Incase you get lazy and don't want to define bunch of CapsuleConstants by hand, you may refer to :

`CapsuleConstants.GenerateCapsulationConstants(int amount)` is a static method to generate specified amount of CapsuleConstants instances. Returns `Stack<CapsuleConstant>` which could be used to create LiteByteCapsule instance.
___
### <a name="constructors"></a> Constructors

 For encapsulation or decapsulation, you have to create LiteByteCapsule class instance using diverse constructors.
 
 ####**`LiteByteCapsule()`**
 This constructor is used in order to encapsule/decapsulate byte array packages using SmartCapsule technique.
 
 **Smart encapsulation**
 
 | First element      | In between           | Last 4 elements  |
 | :------------- |:-------------:| -----:|
 | 0      | byte[] *| CRC32C **|
_*byte[] is your actual byte array._

_**CRC32C is CRC32C calculation of your actual byte array._  

 
  ####**`LiteByteCapsule(Stack<CapsuleConstant> constants)`**
  This constructor is used in order to encapsule/decapsulate byte array packages by placing CapsuleConstants into defined positions.
  
  **Encapsulation by CapsuleConstant Stack**
  
  | Head(start)| In between|Tail(end)|
  |:------------- |:-------------:| -----:|
  | Head constants *| byte[] **| Tail constants ***|
  
 _*Head constants: CapsuleConstants in the stack provided as parameter, which has **head=true** value_
 
 _**byte[] is your actual byte array._
 
 _***Head constants: CapsuleConstants in the stack provided as parameter, which has **head=false** value_
 
 
   ####**`LiteByteCapsule(byte[] constantFirstPart, byte[] constantLastPart)`**
   This constructor is used in order to encapsule/decapsulate byte array packages by putting constantFirstPart array to the start of the capsule and constantLastPart array to end of the capsule.
   
   **Encapsulation by two byte arrays**
   
   | Head(start)| In between|Tail(end)|
   |:-------------|:-------------:| -----:|
   | constantFirstPart[]| byte[] *| Tail constantFirstPart |

  
  _*byte[] is your actual byte array._
  
 
 
### <a name="encapsulation"></a> Encapsulation

Simply use `ConvertToSyntax(byte[] infactData)` method on your LiteByteCapsule instance.
```
LiteByteCapsule lite=new LiteByteCapsule();

//or LiteByteCapsule lite = new LiteByteCapsule(CapsuleConstant.GenerateCapsulationConstants(5));

/*
or 
Stack<CapsulationConstant> constants=new Stack<CapsulationConstant>();
LiteByteCapsule lite = new LiteByteCapsule(constants);
*/

/*
or
byte[] head=....
byte[] tail=...
LiteByteCapsule lite=new LiteByteCapsule(head, tail);
*/

//Then just pass your byte array to ConvertToSyntax method:
byte[] myPackage=[12,255,234,116];
byte[] capsule=lite.ConvertToSyntax(myPackage);
//capsule is the encapsulated byte array.

```

### <a name="exporting"></a> Exporting Encapsulation Constants
After encapsulation process, you might want to send your export your encapsulation constants to the receiver of the package/capsule so that the receiver can create LiteByteCapsule instance and do decapsulate your package according to your encapsulation constants.

```
//Use your previously created LiteByteCapsule instance:

string json=lite.CapsulationConstantsJSONString();

//Then send it to your listener/receiver through an api/websocket/endpoint etc. 
//Receiver will have to parse the json and use it to create LiteByteCapsule instance.
```

### <a name="decapsulation"></a> Decapsulation
```
//Create your LiteByteCapsule according to the Capsule Constants provided by the package sender.

Stack<CapsuleConstant> constants = JsonConvert.DeserializeObject<Stack<CapsuleConstant>>(a);

LiteByteCapsule lite=new LiteByteCapsule(constants);

byte[] incomingPackage=....

byte[] actualPackage=lite.CheckSyntax(incomingPackage);

//returns null if the package provided(incomingPackage) is an imposter/doesn't fit to the sequence expected.



```
### <a name="instance'"></a> Instance utilities/helpers
```
//Stack<CapsuleConstant> constants=...

LiteByteCapsule lite=new LiteByteCapsule(constants);

//From your instance you have access to these utilities:

//CapsulationConstantsJSONString: For converting the encapsulation constants to JSON string

string json=lite.CapsulationConstantsJSONString();

//GetCapsulationConstants: To get your stack of encapsulation constants that you provided earlier to create an instance of LiteByteCapsule

Stack<CapsuleConstant<CapsuleConstant> constants=lite.GetCapsulationConstants();


```

### <a name="static'"></a> Static utilities/helpers
```
//Generate a random byte array 
byte[] package=LiteByteCapsule.GetRandomPackage(20);

//Convert a byte array to string
string readableFormat=LiteByteCapsule.ConvertToString(package);

//Generate CRC32C Hash/Checksum of a byte array
string hash=LiteByteCapsule.GenerateCRC32C(package);

//Add CRC32C calculation to end of an array (4 elements)
byte[] capsule=LiteByteCapsule.AddCRC32CToEnd(package);


//Check CRC32C integrity of a package (warning, this will expect that the given array has last 4 elements as CRC32C
byte[] actualPackage=LiteByteCapsule.CheckCRC32CIntegrity(package);



```
## Tests
Please do check Azure Devops linked in the badges section or revise CodeCov code coverage reports to see unit test results.

##Licence
[(MIT Licence)] [licence]



## Old Documentation

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
 
 
 [diagram]: ./diagram.jpg "Flowchart diagram of pathways."
 
 
 [//]: #REFERENCES
 
 
 [licence]: https://github.com/skywarth/LiteByteCapsule/blob/Lib-including-test/LICENSE "MIT Licence"
 
 
 [mitm]: https://en.wikipedia.org/wiki/Man-in-the-middle_attack "Man in the Middle Attack in information security - Wikipedia"
 
 [ddos]: https://en.wikipedia.org/wiki/Denial-of-service_attack "Denial of service attack"
