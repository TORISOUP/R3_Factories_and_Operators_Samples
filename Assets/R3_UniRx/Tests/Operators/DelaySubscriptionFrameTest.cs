using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using R3;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace R3_UniRx.Tests.Operators
{
    public sealed class DelaySubscriptionFrameTest
    {
        [Test]
        public void R3_DelaySubscriptionFrame_遅れてSubscribeする()
        {
            var fakeFrameProvider = new FakeFrameProvider();

            var isSubscribed = false;

            using var subject = new R3.Subject<int>();

            // Subscribeされたらフラグを立てる
            var observable = subject.Do(onSubscribe: () => isSubscribed = true);

            // 3F遅らせてSubscribeする
            observable.DelaySubscriptionFrame(3, fakeFrameProvider).Subscribe();

            // まだSubscribeされていない
            Assert.IsFalse(isSubscribed);

            fakeFrameProvider.Advance(); // 1
            fakeFrameProvider.Advance(); // 2
            fakeFrameProvider.Advance(); // 3

            // Subscribeされている
            Assert.IsTrue(isSubscribed);
        }

        [Test]
        public async Task UniRx_DelayFrameSubscription()
        {
            var isSubscribed = false;

            UniRx.Observable.Return(1)
                .DoOnSubscribe(() => isSubscribed = true)
                .DelayFrameSubscription(1)
                .Subscribe();

            Assert.IsFalse(isSubscribed);

            await UniTask.WaitUntilValueChanged(this, _ => Time.frameCount);

            Assert.IsTrue(isSubscribed);
        }
    }
}