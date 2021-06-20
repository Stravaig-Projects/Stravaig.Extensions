using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace Stravaig.Extensions.Core.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class IEnumerableOfTExtensions_ForEachTests
    {
        [Test]
        public void ForEach_CallsActionForEachElement()
        {
            string[] elements = { "abc", "def", "ghi" };

            List<string> result = new List<string>();
            elements.ForEach(s => result.Add(s));
            
            result[0].ShouldBe(elements[0]);
            result[1].ShouldBe(elements[1]);
            result[2].ShouldBe(elements[2]);
        }

        [Test]
        public void ForEach_SequenceParameterNullCheck()
        {
            string[] sequence = null;
            Should.Throw<ArgumentNullException>(() =>
                // ReSharper disable once ExpressionIsAlwaysNull
                sequence.ForEach(s => throw new InvalidOperationException("Should not reach here!")));
        }

        [Test]
        public void ForEach_ActionParameterNullCheck()
        {
            string[] sequence = Array.Empty<string>();
            Should.Throw<ArgumentNullException>(() =>
                sequence.ForEach((Action<string>)null));
        }
        
        [Test]
        public void ForEachWithIndex_CallsActionForEachElement()
        {
            string[] elements = {"abc", "def", "ghi" };
            string[] result = new string[elements.Length];
            elements.ForEach((s, i) => result[i] = s);
            
            result[0].ShouldBe(elements[0]);
            result[1].ShouldBe(elements[1]);
            result[2].ShouldBe(elements[2]);
        }
        
        [Test]
        public void ForEachWithIndex_SequenceParameterNullCheck()
        {
            string[] sequence = null;
            Should.Throw<ArgumentNullException>(() =>
                // ReSharper disable once ExpressionIsAlwaysNull
                sequence.ForEach((s,i) => throw new InvalidOperationException("Should not reach here!")));
        }

        [Test]
        public void ForEachWithIndex_ActionParameterNullCheck()
        {
            string[] sequence = Array.Empty<string>();
            Should.Throw<ArgumentNullException>(() =>
                sequence.ForEach((Action<string, int>)null));
        }
        
        [Test]
        public async Task ForEachAsync_CallsActionForEachElementAsync()
        {
            string[] elements = { "abc", "def", "ghi" };

            List<string> result = new List<string>();
            await elements.ForEachAsync(async s =>
            {
                result.Add(s);
                await Task.CompletedTask;
            });
            
            result[0].ShouldBe(elements[0]);
            result[1].ShouldBe(elements[1]);
            result[2].ShouldBe(elements[2]);
        }
        
        [Test]
        public async Task ForEachAsync_SequenceParameterNullCheck()
        {
            string[] sequence = null;
            await Should.ThrowAsync<ArgumentNullException>(async () =>
                // ReSharper disable once ExpressionIsAlwaysNull
                await sequence.ForEachAsync(async s =>
                {
                    await Task.CompletedTask;
                    throw new InvalidOperationException("Should not reach here!");
                }));
        }

        [Test]
        public async Task ForEachAsync_ActionParameterNullCheckAsync()
        {
            string[] sequence = Array.Empty<string>();
            await Should.ThrowAsync<ArgumentNullException>(async () =>
                await sequence.ForEachAsync((Func<string, Task>)null));
        }
        
        [Test]
        public async Task ForEachAsyncWithIndex_CallsActionForEachElementAsync()
        {
            string[] elements = {"abc", "def", "ghi" };
            string[] result = new string[elements.Length];
            await elements.ForEachAsync(async (s, i) =>
            {
                result[i] = s;
                await Task.CompletedTask;
            });
            
            result[0].ShouldBe(elements[0]);
            result[1].ShouldBe(elements[1]);
            result[2].ShouldBe(elements[2]);
        }

        [Test]
        public async Task ForEachAsyncWithIndex_SequenceParameterNullCheck()
        {
            string[] sequence = null;
            await Should.ThrowAsync<ArgumentNullException>(async () =>
                // ReSharper disable once ExpressionIsAlwaysNull
                await sequence.ForEachAsync(async (s,i) =>
                {
                    await Task.CompletedTask;
                    throw new InvalidOperationException("Should not reach here!");
                }));
        }

        [Test]
        public async Task ForEachAsyncWithIndex_ActionParameterNullCheckAsync()
        {
            string[] sequence = Array.Empty<string>();
            await Should.ThrowAsync<ArgumentNullException>(async () =>
                await sequence.ForEachAsync((Func<string, int, Task>)null));
        }
    }
}