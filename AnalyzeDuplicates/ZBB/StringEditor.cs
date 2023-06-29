namespace ZBB
{
    public static class StringEditor
    {        
        public static List<string> RemovePhareFromList(List<string> listNames, string phraseToRemove)
        {
            List<string> newListNames = new List<string>();
            foreach(var n in listNames)
            {
                newListNames.Add(n.Replace(phraseToRemove, string.Empty));
            }
            return newListNames;
        }
    }
}