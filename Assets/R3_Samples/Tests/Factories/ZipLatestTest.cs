using NUnit.Framework;
using R3;

namespace R3_Samples.Tests.Factories
{
    public sealed class ZipLatestTest
    {
        [Test]
        public void R3_ZipLatest_複数のObservableの値が揃ったら1つメッセージを発行する()
        {
            using var first = new Subject<int>();
            using var second = new Subject<string>();

            using var list = Observable.ZipLatest(first, second, (x, y) => x + y).ToLiveList();

            // first側入力
            first.OnNext(1);

            // secondが入力されていないので何も出力されない
            CollectionAssert.IsEmpty(list);


            // second側入力
            second.OnNext("a");

            // [1]と[a]が揃ったので出力される
            CollectionAssert.AreEqual(new[] { "1a" }, list);

            // first側は出し切ったので空
            // secondに[b]が保持される
            second.OnNext("b");

            // second側の値が[c]に更新される
            second.OnNext("c");

            // first側に[2]が入力される
            first.OnNext(2);

            // [2]と[c]が揃ったので出力される
            CollectionAssert.AreEqual(new[] { "1a", "2c" }, list);

            // first側に[3]が入力される
            // second側は出し切ったので空
            first.OnNext(3);

            // 変化無し
            CollectionAssert.AreEqual(new[] { "1a", "2c" }, list);

            // second側に[d]が入力される
            second.OnNext("d");

            // [3]と[d]が揃ったので出力される
            CollectionAssert.AreEqual(new[] { "1a", "2c", "3d" }, list);
        }
    }
}