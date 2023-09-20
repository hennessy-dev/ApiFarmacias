namespace ApiJwt.Helpers;

public class Authorization
{
    public enum Roles
    {
        Admin,
        Manager,
        Employee
    }

    public const Roles rol_default = Roles.Employee;
}
