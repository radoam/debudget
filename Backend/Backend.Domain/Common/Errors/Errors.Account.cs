using ErrorOr;

namespace Backend.Domain.Common.Errors;

public static partial class Errors
{
    public static class Account
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "Account.DuplicateEmail",
            description: "Account with given email already exist.");
    }
}