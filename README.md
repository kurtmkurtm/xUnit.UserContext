# xUnit.UserContext
xUnit.UserContext is an attribute based extension for [xUnit](https://github.com/xunit/xunit) that allows tests to run under the context of a given Windows user. Impersonation can be useful for running integration tests that use resources that require windows authentication, such as connecting to a database.

The aim of this library is to allow easy usage in xUnit tests with minimal code. This library utilises the wrapper library [SimpleImpersonation](https://github.com/mj1856/SimpleImpersonation) to use the Win32 LogonUser API for Impersonation. Additionally, because storing credentials in code is not ideal, this library also supports retrieval of credentials stored using ASP.NET [UserSecrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2&tabs=windows).

## Installation

Using the dotnet cli:

```powershell
dotnet add package xunit.usercontext
```

## Usage

To use this library use a UserFact or UserTheory in place of Fact or Theory

**UserFactAttribute** 

```C#
using Xunit.UserContext;
...
[UserFact("UserName", "Password")]
public void TestedMethodName_StateUnderTest_ExpectedBehavior()
{
    // Test case that uses resource restricted to a windows user
    // e.g. database connection using windows auth
}
...
```

**UserTheoryAttribute**

```C#
using Xunit.UserContext;
...
[UserTheory("TestUserSecretsId")]
[InlineData("TestValue")]
public void TestedMethodName_StateUnderTest_ExpectedBehavior(string value)
{
    // Test case that uses resource restricted to a windows user
    // e.g. database connection using windows auth
}
...
```

### Attribute Constructors

Both **UserFact** and **UserTheory** follow the same constructor patterns, so the all the following overloads are applicable to both. Additionally, all constructors have the following optional parameters with default values.

- **logonType** - default value of Network. The type of logon operation to perform (as per the underlying API), this should be set according to your usage, more information on the different logon types is available [here](https://docs.microsoft.com/en-au/windows/desktop/api/winbase/nf-winbase-logonusera). 
- **displayNameOnTest**  - default value of true. If true, this appends the username to the test name in the following format 
  `(user: username)`

#### **Static Username and Password**

```C#
[UserFact("UserName", "Password")]   
[UserTheory("UserName","Password")]
```

- **username** - windows username
- **password** - windows password

#### **Static Username, Password and Domain**

```C#
[UserFact("UserName", "Password","Domain")] 
[UserTheory("UserName","Password","Domain")]
```

- **username** - windows username
- **password** - windows password
- **domain** - windows domain

#### **User Secrets Id** 

```C#
[UserFact("TestUserSecretsId")]
[UserTheory("TestUserSecretsId")]
```

* **userSecretsId** - user secrets id, used when setting secrets

To set the credentials for this option, use the dotnet cli to set the values. The library will read values for the following keys: **Username**,**Password**, and optionally **Domain**. Below is an example of setting these values:

```cmd
dotnet user-secrets set "Username" MyUsername --id TestUserSecretsId
dotnet user-secrets set "Password" MyPassword --id TestUserSecretsId
dotnet user-secrets set "Domain" Domain --id TestUserSecretsId
```

*Note: the secrets id used to set these values must be the same as what is provided to the attribute*

## Issues & Contributions

If you find a bug or have a feature request, please report them in this repository's issues section. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

**Note: Impersonation does not work with async**

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Acknowledgments

-  [xUnit](https://github.com/xunit/xunit)
- [SimpleImpersonation](https://github.com/mj1856/SimpleImpersonation) 
- [Ionicons](https://github.com/ionic-team/ionicons)
