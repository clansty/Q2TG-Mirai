using LevelDB;

namespace Clansty.tianlang
{
    static class Db
    {
        internal static DB ldb;

        internal static void Init(bool reload = false)
        {
            if (!reload)
            {
                var options = new Options {CreateIfMissing = true};
                //leveldb 保存位置
                ldb = new DB(options, "ldb/qtime2tgmsgid");//TODO
            }
        }
    }
}