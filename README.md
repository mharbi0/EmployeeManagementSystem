# Employee Attendance Management System

## How to run

### Initializing the Database:
The system requires a Microsoft SQL server in order to run properly. The DB has the following schema:
- Employee
    - `Admin`: is the employee an administrator in the system?
    - `Active` field indicates if the employee is still part of the organization or if they have left for any reason. After all, an organization needs to keep a record of its past employees for various reasons.
```c
class Employee : IdentityUser
{
    string Name;
 
    bool Admin;

    bool Active;
}
```
    
- Employee Attendance
```c
class EmployeeAttendance
{
    [ForeignKey("Employee")]
    string EmployeeId;
    
    [Key]
    DateTime CheckIn;

    DateTime CheckOut; 

    bool LateCheckIn;

    bool EarlyCheckOut;
}
```
- Justicications: used to justify late check-ins and early check-outs
```c
class Justification
{
    [Key]
    int JustificationId ;

    DateTime DateCreated ;

    [ForeignKey("Employee")]
    string EmployeeId ;

    [ForeignKey("EmployeeAttendance")]
    DateTime CheckIn;

    string Reason ;

    bool Accepted ;
}
```

One way to initialize the DB in order for the solution to run properly is through Microsoft Visual Studio's Package Manager Console: 
- Delete the `Migrations` folder 
- Configure the DB connection settings in the `appsettings.json` file to point to your SQL server, and excute the following in the Package Management Console:
```cli
Add-Migration CreateDB
Update-Database
```
### Running the Solution
- Simply open the solution in Visual Studio and press `F5`, or navigate inside the `EbttikarWeb` directory and excute the following in the CLI:
```json
dotnet run
```
- Naviagate to the following link: 
```url
https://localhost:7190/
```
- In order to have full access to the system, you need to register as a new user
    - Emails have to be in correct form (i.e `someone@example.com`) but they don't have to be valid
    - Names can be any string
    - Passwords must meet the following criteria:
        - at least 6 charcters in length
        - at least one non alphanumeric character.
        - at least one lowercase ('a'-'z').
        - at least one uppercase ('A'-'Z').

# Side Note
While the original goal was to develop a system that conforms to the Clean Architecture, well-known conventions, and best practices, the project ended up being of a monolothic structure. The main reason it ended up this way is that this type of architecture proved too complex for a project of this size with the given time-constraints. See below link for an attempt at developing a well-structured solution.

```url
https://github.com/mharbi0/EmployeeAttendance
```
### Thank you