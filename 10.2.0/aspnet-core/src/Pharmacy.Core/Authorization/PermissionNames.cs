namespace Pharmacy.Authorization;

public static class PermissionNames
    {
        public const string Orders_StartReview = "Orders.StartReview";
        public const string Orders_FinalizeReview = "Orders.FinalizeReview";
        public const string Orders_Reject      = "Orders.Reject";
        public const string Orders_Cancel      = "Orders.Cancel";

        // ABP identity management defaults (used by authorization provider)
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Activation = "Pages.Users.Activation";
        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Tenants = "Pages.Tenants";
    }
