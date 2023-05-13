namespace PowerQualityManageService.Core.Utils.Extensions;

public static class StringExtensions
{
    public static string Move(this string text, int fromIdx, int toIdx, int length)
    {
        var partToMove = text.Substring(fromIdx, length);
        if (toIdx > fromIdx)
        {
            return text.Insert(toIdx, partToMove).Remove(fromIdx, length);
        }
        else
        {
            return text.Remove(fromIdx, length).Insert(toIdx, partToMove);
        }
    }
    public static string Move(this string text, int fromIdx, int toIdx, int length, Func<string,string> modifyMovedPart )
    {
        var partToMove =  modifyMovedPart(text.Substring(fromIdx, length));
        if (toIdx > fromIdx) 
        {
            return text.Insert(toIdx, partToMove).Remove(fromIdx, length);
        }
        else
        {
            return text.Remove(fromIdx, length).Insert(toIdx, partToMove);
        }

    }
}