namespace Evesoft.Dialogue.YarnSpinner
{
    public static class YarnValueAdapter
    {
        public static object[] ToObjects(this Yarn.Value[] data)
        {
            if(data.IsNullOrEmpty())
                return null;

            var result = new object[data.Length];
            for (int i = 0; i < result.Length; i++)
            {
                var value = data[i];
                switch(value.type)
                {
                    case Yarn.Value.Type.Bool:
                    {
                        result[i] = value.AsBool;
                        break;
                    }
                    
                    case Yarn.Value.Type.Number:
                    {
                        result[i] = value.AsNumber;
                        break;
                    }

                    case Yarn.Value.Type.String:
                    {
                        result[i] = value.AsString;
                        break;
                    }

                    default:
                    {
                        result[i] = null;
                        break;
                    }
                }
            }

            return result;
        }        
    }
}