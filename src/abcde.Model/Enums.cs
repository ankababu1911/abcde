using System.ComponentModel;

namespace abcde.Model
{
    public enum TenantStatus
    {
        Notset = 0,
        Onboarding = 1,
        Live = 2,
        Suspended = 3,
        Closed = 4,
    }

    public enum Roles
    {
        SystemAdmin,
        Admin,
        Teacher,
        Student
    }

    public enum AddressType
    {
        Correspondence = 0,
        Billing = 1
    }

    public enum ItemStatus
    {
        Notset = 0,
        Open = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
    }

    public enum Complexity
    {
        Notset = 0,
        Easy = 1,
        Ok = 2,       
        Difficult = 3        
    }

    public enum Importance
    {
        NotSpecified = 0,
        NotImportant = 1,
        Important = 2,
    }
    public enum Urgency
    {
        NoSpecificDeadline = 0,
        ThisWeek = 1,
        Later = 2,
        Immediate = 3
    }
    public enum YesNo
    {
        Notset = 0,
        Yes = 1,
        No = 2
    }

    public enum Priority
    {
        Notset = 0,
        High = 1,
        Medium = 2,
        Low = 3
    }

    public enum DayPlanStatus
    {
        Planned,
        InProgress,
        Defferd,
        Done
    }

    public enum WorkItemCategory
    {
        WellBeing = 0,
        Curriculum = 1,
        Family = 2,
        Hobby = 3,
        Passion = 4
    }
}