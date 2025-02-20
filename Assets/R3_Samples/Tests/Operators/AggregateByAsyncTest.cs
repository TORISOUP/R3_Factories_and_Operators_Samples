using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using R3;

namespace R3_Samples.Tests.Operators
{
    public class AggregateByAsyncTest
    {
        [Test]
        public async Task R3_AggregateByAsync_グループ単位で畳み込み計算する()
        {
            // キャンセルすることはないが、CancellationTokenは準備しておく
            var ct = CancellationToken.None;

            var array = new[] { 1, 2, 3, 4, 5 };

            var result = await R3.Observable.ToObservable(array)
                .AggregateByAsync(
                    // 偶数グループと奇数グループに分けてそれぞれ合計値を計算
                    keySelector: key => key % 2,
                    seed: 0,
                    func: (total, cur) => total + cur,
                    cancellationToken: ct);

            var actual = result.ToArray();

            Assert.AreEqual(9, actual[0].Value); // 偶数グループの合計値
            Assert.AreEqual(6, actual[1].Value); // 奇数グループの合計値
        }

        [Test]
        public async Task R3_AggregateByAsync_グループごとに初期値を指定して畳み込み計算する()
        {
            // キャンセルすることはないが、CancellationTokenは準備しておく
            var ct = CancellationToken.None;

            var array = new[] { 1, 2, 3, 4, 5 };

            var result = await R3.Observable.ToObservable(array)
                .AggregateByAsync(
                    // 偶数グループと奇数グループに分けてそれぞれ合計値を計算
                    keySelector: key => key % 2,
                    seedSelector: value => value == 0 ? 200 : 100, // 偶数グループは200から、奇数グループは100から始める
                    func: (total, cur) => total + cur,
                    cancellationToken: ct);

            var actual = result.ToArray();

            Assert.AreEqual(109, actual[0].Value); // 偶数グループの合計値
            Assert.AreEqual(206, actual[1].Value); // 奇数グループの合計値
        }


        [Test]
        public void UniRx_AggregateByは存在しない()
        {
            Assert.Ignore();
        }
    }
}