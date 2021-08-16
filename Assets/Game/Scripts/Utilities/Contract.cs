#region

using System;

#endregion

namespace Utilities.Contract
{
    public static class Contract
    {
    #region Public Variables

        public static bool CHECK_CLASS_INVARIANT = true;
        public static bool CHECK_POST            = true;
        public static bool CHECK_PRE             = true;

    #endregion

    #region Public Methods

        public static void ClassInvariant(bool value , string annotation = "")
        {
            if (CHECK_CLASS_INVARIANT == false) return;
            if (value == false) throw new ClassInvariantViolationException(annotation);
        }

        public static void ClassInvariantNotNull(object obj , string annotation = "")
        {
            if (CHECK_CLASS_INVARIANT == false) return;
            ClassInvariant(obj != null , $"{annotation} can not be null");
        }

        public static void ClassInvariantString(string str , string annotation = "")
        {
            if (CHECK_CLASS_INVARIANT == false) return;
            ClassInvariant(string.IsNullOrEmpty(str) == false , $"{annotation} can not be empty or null");
        }

        public static void Ensure(bool value , string annotation = "")
        {
            if (CHECK_POST == false) return;
            if (value == false) throw new PostConditionViolationException(annotation);
        }

        public static void EnsureNotNull(object obj , string annotation = "")
        {
            if (CHECK_POST == false) return;
            Ensure(obj != null , $"{annotation} can not be null");
        }

        public static void EnsureString(string str , string annotation = "")
        {
            if (CHECK_POST == false) return;
            Ensure(string.IsNullOrEmpty(str) == false , $"{annotation} can not be empty or null");
        }

        public static void Require(bool value , string annotation = "")
        {
            if (CHECK_PRE == false)
                return;
            if (value == false)
                throw new PreconditionViolationException(annotation);
        }

        public static void RequireNotNull(object obj , string annotation = "")
        {
            if (CHECK_PRE == false) return;
            Require(obj != null , $"{annotation} can not be null");
        }

        public static void RequireString(string str , string annotation = "")
        {
            if (CHECK_PRE == false) return;
            Require(string.IsNullOrEmpty(str) == false , $"{annotation} can not be empty or null");
        }

    #endregion
    }

    public class PreconditionViolationException : Exception
    {
    #region Constructor

        public PreconditionViolationException(string annotation) : base(annotation) { }

    #endregion
    }

    public class PostConditionViolationException : Exception
    {
    #region Constructor

        public PostConditionViolationException(string annotation) : base(annotation) { }

    #endregion
    }

    public class ClassInvariantViolationException : Exception
    {
    #region Constructor

        public ClassInvariantViolationException(string annotation) : base(annotation) { }

    #endregion
    }
}