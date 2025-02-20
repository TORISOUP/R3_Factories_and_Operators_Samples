using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using R3;
using UniRx;

namespace R3_Samples.Tests.Operators
{
    public sealed class SumAsyncTest
    {
        [Test]
        public async Task R3_SumAsync_値の合計を求める()
        {
            // キャンセルすることはないが、CancellationTokenは準備しておく
            var ct = CancellationToken.None;

            var array = new[] { 1, 2, 3, 4, 5 };

            var result = await R3.Observable.ToObservable(array)
                .SumAsync(cancellationToken: ct);

            Assert.AreEqual(15, result);
        }
        
        [Test]
        public async Task UniRx_SumをLINQで再現する()
        {
            // キャンセルすることはないが、CancellationTokenは準備しておく
            var ct = CancellationToken.None;

            var array = new[] { 1, 2, 3, 4, 5 };

            var result = await UniRx.Observable.ToObservable(array)
                .ToArray()
                .ToTask(ct);

            Assert.AreEqual(15, result.Sum());
        }
    }
}