namespace Moba
{
    public static class StringExtensions
    {
        /// <summary>
        /// 这个是消息通信的基础 需要版本不同编程语言 不同平台下 对同一个字符串的hash值都是一样的
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetStableHash(this string str)
        {
            unchecked
            {
                int hash = 23;
                foreach (char c in str)
                    hash = hash * 31 + c;
                return hash;
            }
        }
        // golang版本
        // func GetStableHash(str string) int {
        //     hash := 23
        //     for _, c := range str {
        //         hash = hash * 31 + int(c)
        //     }
        //     return hash
        // }
        
        // python版本
        // def GetStableHash(str):
        //     hash = 23
        //     for c in str:
        //         hash = hash * 31 + ord(c)
        //     return hash
    }
}